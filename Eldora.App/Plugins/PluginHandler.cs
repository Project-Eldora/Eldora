using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Eldora.PluginApi.Attributes;
using Eldora.Utils;
using NLog;

namespace Eldora.App.Plugins;

public class PluginChangedEventArgs : EventArgs
{
	public PluginContainer PluginContainer { get; }
	public PluginChangedEventType Type { get; }

	public PluginChangedEventArgs(PluginContainer pluginContainer, PluginChangedEventType type)
	{
		this.PluginContainer = pluginContainer;
		Type = type;
	}

	public enum PluginChangedEventType
	{
		Added,
		Changed
	}
}

public static class PluginHandler
{
	private static readonly Logger Log = LogManager.GetCurrentClassLogger();

	private const string PluginInfoFileName = "plugininfo.json";
	private const string ContentPathPrefix = "content";
	public static List<PluginContainer> LoadedPlugins { get; } = new();
	public static event EventHandler<PluginChangedEventArgs> PluginsGotChanged;

	private class PluginData
	{
		public string FullName { get; set; }
		public string Name { get; set; }
		public AssemblyName AssemblyName { get; set; }
		public byte[] Data { get; set; }
	}

	public static PluginLoadResult LoadPlugin(string pluginPath)
	{
		// Check for plugin extension
		if (!pluginPath.EndsWith(".zip"))
		{
			Log.Warn("Could not load plugin from path {path}. (It is not a zip)", pluginPath);
			return new PluginLoadResult(PluginLoadResult.ErrorCode.InvalidPath);
		}
		
		if (new FileInfo(pluginPath).Length == 0)
		{
			Log.Info("Deleting corrupt file: {path}", pluginPath);
			File.Delete(pluginPath);
			return new PluginLoadResult(PluginLoadResult.ErrorCode.CorruptFile);
		}


		List<PluginData> contentData = new();

		using var zip = ZipFile.OpenRead(pluginPath);
		PluginInfoModel infoModel = null;
		foreach (var zipArchiveEntry in zip.Entries)
		{
			if (zipArchiveEntry.FullName.Equals(PluginInfoFileName))
			{
				using var stream = zipArchiveEntry.Open();
				infoModel = ReadPluginFile(ReadStreamFully(stream));
				continue;
			}

			if (!zipArchiveEntry.FullName.StartsWith(ContentPathPrefix)) continue;
			if (!zipArchiveEntry.FullName.EndsWith(".dll")) continue;

			var data = new PluginData
			{
				AssemblyName = GetAssemblyName(zipArchiveEntry),
				Data = ReadStreamFully(zipArchiveEntry.Open()),
				FullName = zipArchiveEntry.FullName,
				Name = zipArchiveEntry.Name
			};

			contentData.Add(data);
		}

		// Check for plugin-info
		if (infoModel == null)
		{
			Log.Warn("Plugin Info not found in plugin {PluginName}", pluginPath);
			return new PluginLoadResult(PluginLoadResult.ErrorCode.PluginInfoNotFound);
		}

		// Check for main assembly
		var mainDllEntry = contentData.FirstOrDefault(tuple => tuple.Name.Equals(infoModel.MainDll));
		if (mainDllEntry == default)
		{
			Log.Warn("Plugin {PluginName} has no maindll entry", infoModel.PluginName);
			return new PluginLoadResult(PluginLoadResult.ErrorCode.MissingMainDllEntry);
		}
		
		AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
		{
			Log.Info("{Object} is searching for dependency {DependencyName}", sender, args.Name);
			var result = contentData.FirstOrDefault(p => p.AssemblyName.FullName.Equals(args.Name));
			return result == default ? null : LoadAssembly(result);
		};

		// Check for main class
		var mainAssembly = LoadAssembly(mainDllEntry);
		object pluginInstance = null;
		foreach (var type in mainAssembly.GetExportedTypes())
		{
			if (!Attribute.IsDefined(type, typeof(PluginEntryAttribute))) continue;
			pluginInstance = Activator.CreateInstance(type);
			Log.Info("Found Plugin Entry {name}", pluginInstance);
			break;
		}

		if (pluginInstance == null)
		{
			Log.Warn("Plugin {PluginName} has no main plugin entry", infoModel.PluginName);
			return new PluginLoadResult(PluginLoadResult.ErrorCode.MissingMainPluginEntry);
		}

		var plugin = new PluginContainer(pluginInstance, infoModel, mainAssembly, pluginPath);
		LoadedPlugins.Add(plugin);
		
		// Calls the on load method in the plugin
		plugin.CallLoad();
		
		return new PluginLoadResult(plugin);
	}

	public static void RaisePluginsChangedEvent(object sender, PluginChangedEventArgs args)
	{
		PluginsGotChanged?.Invoke(sender, args);
	}

	private static Assembly LoadAssembly(PluginData entry)
	{
		Log.Info("Loading Assembly {EntryName}", entry.FullName);
		return Assembly.Load(entry.Data);
	}

	private static PluginInfoModel ReadPluginFile(byte[] data)
	{
		return JsonSerializer.Deserialize<PluginInfoModel>(ConvertBytesToString(data), EldoraApp.DefaultSerializerOptions);
	}

	private static string ConvertBytesToString(byte[] bytes)
	{
		using var stream = new MemoryStream(bytes);
		using var streamReader = new StreamReader(stream);
		return streamReader.ReadToEnd();
	}

	private static byte[] ReadStreamFully(Stream input)
	{
		var buffer = new byte[16 * 1024];
		using var ms = new MemoryStream();

		int read;
		while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
		{
			ms.Write(buffer, 0, read);
		}

		return ms.ToArray();
	}

	private static AssemblyName GetAssemblyName(ZipArchiveEntry entry)
	{
		using var tmpFile = new TempFile();
		using (var fs = new FileStream(tmpFile.Path, FileMode.Open))
		{
			entry.Open().CopyTo(fs);
		}

		// Use temp file 
		return AssemblyName.GetAssemblyName(tmpFile.Path);
	}

	public static PluginInfoModel GetPluginInfo(string path)
	{
		using var zip = ZipFile.OpenRead(path);
		PluginInfoModel infoModel = null;
		foreach (var zipArchiveEntry in zip.Entries)
		{
			if (!zipArchiveEntry.FullName.Equals(PluginInfoFileName)) continue;
			using var stream = zipArchiveEntry.Open();
			infoModel = ReadPluginFile(ReadStreamFully(stream));
		}

		return infoModel;
	}

	public static bool IsNewerOrAlreadyInstalled(PluginInfoModel infoModel)
	{
		return IsNewerOrAlreadyInstalled(infoModel.PluginName, infoModel.PluginVersion);
	}

	/// <summary>
	/// Returns true if there are any plugins, that match the name, that have a version greater or equal to the provided version
	/// </summary>
	/// <param name="name"></param>
	/// <param name="version"></param>
	/// <returns></returns>
	public static bool IsNewerOrAlreadyInstalled(string name, Version version)
	{
		return LoadedPlugins.Any(p => p.PluginInfoModel.PluginName == name && p.PluginInfoModel.PluginVersion >= version);
	}

	public static bool ShouldBeUpdated(string name, Version version, out PluginContainer oldPluginContainer)
	{
		var plugin = LoadedPlugins.FirstOrDefault(p => p.PluginInfoModel.PluginName == name);
		oldPluginContainer = null;
		if (plugin == default) return true;
		if (plugin.PluginInfoModel.PluginVersion >= version) return false;
		oldPluginContainer = plugin;
		return true;
	}
	
	public static bool ShouldBeUpdated(PluginInfoModel infoModel, out PluginContainer oldPluginContainer)
	{
		return ShouldBeUpdated(infoModel.PluginName, infoModel.PluginVersion, out oldPluginContainer);
	}
}

public class PluginLoadResult
{
	public ErrorCode Code { get; }
	public PluginContainer PluginContainer { get; }

	public PluginLoadResult(ErrorCode code, PluginContainer pluginContainer)
	{
		Code = code;
		PluginContainer = pluginContainer;
	}

	public PluginLoadResult(ErrorCode code)
	{
		Code = code;
		PluginContainer = null;
	}

	public PluginLoadResult(PluginContainer pluginContainer)
	{
		PluginContainer = pluginContainer;
		Code = ErrorCode.None;
	}

	public enum ErrorCode
	{
		None,
		Uninstalled,
		InvalidPath,
		PluginInfoNotFound,
		MissingMainDllEntry,
		MissingMainPluginEntry,
		NewerVersionIsInstalled,
		NewVersionInstalled,
		CorruptFile
	}
}
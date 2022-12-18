using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text.Json;
using Eldora.PluginApi.Attributes;
using Eldora.Utils;
using NLog;

namespace Eldora.App.Plugins;

[Serializable]
public class PluginHandler
{
	private static readonly Logger Log = LogManager.GetCurrentClassLogger();

	private static readonly List<EldoraPlugin> Plugins = new();

	private const string PluginInfoFileName = "plugininfo.json";
	private const string ContentPathPrefix = "content";
	public static IEnumerable<EldoraPlugin> LoadedPlugins => Plugins;
	
	public static EldoraPlugin LoadPlugin(string pluginPath)
	{
		if (!pluginPath.EndsWith(".zip"))
		{
			Log.Warn("Could not load plugin from path {path}. (It is not a zip)", pluginPath);
			return null;
		}
		
		List<(ZipArchiveEntry entry, AssemblyName assemblyName)> zipContent = new();

		var zip = ZipFile.OpenRead(pluginPath);
		PluginInfo info = null;
		foreach (var zipArchiveEntry in zip.Entries)
		{
			if (zipArchiveEntry.FullName.Equals(PluginInfoFileName))
			{
				info = ReadPluginFile(zipArchiveEntry);
				continue;
			}
			
			if(!zipArchiveEntry.FullName.StartsWith(ContentPathPrefix)) continue;
			if(!zipArchiveEntry.FullName.EndsWith(".dll")) continue;
			
			zipContent.Add((zipArchiveEntry, GetAssemblyName(zipArchiveEntry)));
		}

		if (info == null)
		{
			Log.Warn("Plugin Info not found in plugin {PluginName}", pluginPath);
			return null;
		}

		var mainDllEntry = zipContent.FirstOrDefault(tuple => tuple.entry.Name.Equals(info.MainDll)).entry;
		if (mainDllEntry == default)
		{
			Log.Warn("Plugin {PluginName} has no maindll entry", info.PluginName);
			return null;
		}

		AppDomain.CurrentDomain.AssemblyResolve += (_, args) =>
		{
			Log.Info("Searching for dependency {DependencyName}", args.Name);
			var result = zipContent.FirstOrDefault(p => p.assemblyName.FullName.Equals(args.Name));
			return result == default ? null : LoadAssembly(result.entry);
		};
		
		var mainAssembly = LoadAssembly(mainDllEntry);
		object pluginInstance = null;
		foreach (var type in mainAssembly.GetExportedTypes())
		{
			if(!Attribute.IsDefined(type, typeof(PluginEntryAttribute))) continue;
			pluginInstance = Activator.CreateInstance(type);
			Log.Info("Found Plugin Entry {name}", pluginInstance);
			break;
		}
		
		var plugin = new EldoraPlugin(pluginInstance, info, mainAssembly);
		Plugins.Add(plugin);
		return plugin;
	}

	private static Assembly LoadAssembly(ZipArchiveEntry entry)
	{
		Log.Info("Loading Assembly {EntryName}", entry.FullName);
		return Assembly.Load(ReadFully(entry.Open()));
	}
	
	private static PluginInfo ReadPluginFile(ZipArchiveEntry entry)
	{
		using var reader = new StreamReader(entry.Open());
		return JsonSerializer.Deserialize<PluginInfo>(reader.ReadToEnd());
	}
	
	private static byte[] ReadFully(Stream input)
	{
		var buffer = new byte[16*1024];
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
}
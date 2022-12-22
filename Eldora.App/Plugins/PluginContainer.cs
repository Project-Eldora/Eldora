using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text.Json.Serialization;
using Eldora.PluginApi.Attributes;
using NLog.Filters;

namespace Eldora.App.Plugins;

public sealed class PluginContainer
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	private readonly object _mainPluginEntry;

	public string LocalFilePath { get; }
	public PluginInfoModel PluginInfoModel { get; }
	public Assembly PluginAssembly { get; }

	public PluginContainer(object mainPluginEntry, PluginInfoModel pluginInfoModel, Assembly pluginAssembly, string localFilePath)
	{
		_mainPluginEntry = mainPluginEntry;
		PluginInfoModel = pluginInfoModel;
		PluginAssembly = pluginAssembly;
		LocalFilePath = localFilePath;
	}

	public void CallLoad()
	{
		foreach (var methodInfo in _mainPluginEntry.GetType().GetMethods())
		{
			if (!Attribute.IsDefined(methodInfo, typeof(PluginLoadAttribute))) continue;
			methodInfo.Invoke(_mainPluginEntry, Array.Empty<object>());
		}
	}

	public void CallUnload()
	{
		foreach (var methodInfo in _mainPluginEntry.GetType().GetMethods())
		{
			if (!Attribute.IsDefined(methodInfo, typeof(PluginUnloadAttribute))) continue;
			methodInfo.Invoke(_mainPluginEntry, Array.Empty<object>());
		}
	}
}

public sealed class PluginInfoModel
{
	[JsonPropertyName("main_dll")]
	[JsonRequired]
	public string MainDll { get; set; }

	[JsonPropertyName("author")]
	[JsonRequired]
	public string Author { get; set; }

	[JsonPropertyName("plugin_name")]
	[JsonRequired]
	public string PluginName { get; set; }

	[JsonPropertyName("version")]
	[JsonRequired]
	public Version PluginVersion { get; set; }

	[JsonPropertyName("description")] public string Description { get; set; }

	[JsonPropertyName("tags")] public string Tags { get; set; }

	public override string ToString()
	{
		return $"{nameof(MainDll)}: {MainDll}, {nameof(Author)}: {Author}, {nameof(PluginName)}: {PluginName}, {nameof(PluginVersion)}: {PluginVersion}, {nameof(Description)}: {Description}, {nameof(Tags)}: {Tags}";
	}
}
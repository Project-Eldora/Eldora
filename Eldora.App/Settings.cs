using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Eldora.App;

public class Settings
{
	[JsonPropertyName("plugin_repos")] public List<PluginRepository> PluginRepositories { get; set; } = new();

	[JsonPropertyName("installed_plugins")]
	public List<InstalledPlugin> InstalledPlugins { get; set; } = new();

	public class InstalledPlugin
	{
		[JsonPropertyName("name")] public string Name { get; set; }
		[JsonPropertyName("version")] public Version Version { get; set; }
	}

	public class PluginRepository
	{
		/// <summary>
		/// The local name
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// The online url
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }
	}
}
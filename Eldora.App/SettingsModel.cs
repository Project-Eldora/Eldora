using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Eldora.App;

public class SettingsModel
{
	[JsonPropertyName("plugin_repos")] public List<PluginRepositoryModel> PluginRepositories { get; set; } = new();

	[JsonPropertyName("installed_plugins")]
	public List<InstalledPluginModel> InstalledPlugins { get; set; } = new();

	public class InstalledPluginModel
	{
		[JsonPropertyName("name")] public string Name { get; set; }
		[JsonPropertyName("version")] public Version Version { get; set; }
	}

	public class PluginRepositoryModel
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using Eldora.App.Plugins;
using Eldora.Utils;

namespace Eldora.App;

public static class EldoraApp
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
	public static Settings Settings { get; private set; }

	private static void LoadPlugins()
	{
		var pluginFolderContent = Directory.GetFiles(Paths.PluginPath, "*.zip").ToList();

		var toDelete = new List<Settings.InstalledPlugin>();

		foreach (var settingsInstalledPlugin in Settings.InstalledPlugins)
		{
			var found = false;

			foreach (var file in pluginFolderContent)
			{
				if (!File.Exists(file)) continue;

				var info = PluginHandler.GetPluginInfo(file);
				if (info == null) continue;

				if (info.PluginName != settingsInstalledPlugin.Name) continue;
				if (info.PluginVersion != settingsInstalledPlugin.Version) continue;

				var loadResult = PluginHandler.LoadPlugin(file);
				if (loadResult.Code != PluginLoadResult.ErrorCode.None)
				{
					Log.Info("Could not load plugin {path}. SKIPPING", file);
					continue;
				}

				found = true;

				pluginFolderContent.Remove(file);
				break;
			}

			if (!found)
			{
				Log.Warn("Plugin {name} with version {version} not found", settingsInstalledPlugin.Name, settingsInstalledPlugin.Version);
				toDelete.Add(settingsInstalledPlugin);
			}
		}

		Settings.InstalledPlugins = Settings.InstalledPlugins.Except(toDelete).ToList();
		SaveSettings();
	}

	private static void LoadSettings()
	{
		try
		{
			if (!File.Exists(Paths.SettingsPath))
			{
				LoadDefaultSettings();
			}

			Settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(Paths.SettingsPath), new JsonSerializerOptions
			{
				Converters =
				{
					new JsonVersionConverter()
				},
				Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
			});
		}
		catch (JsonException e)
		{
			Log.Error(e);
			LoadDefaultSettings();
		}
	}

	private static void LoadDefaultSettings()
	{
		Settings = new Settings
		{
			PluginRepositories =
			{
				new Settings.PluginRepository
				{
					Name = "Default",
					Url = "https://project-eldora.github.io/EldoraPlugins/plugins.json"
				}
			},
		};

		SaveSettings();
	}

	public static void SaveSettings()
	{
		if (File.Exists(Paths.SettingsPath)) File.Delete(Paths.SettingsPath);
		File.WriteAllText(Paths.SettingsPath, JsonSerializer.Serialize(Settings, new JsonSerializerOptions
		{
			WriteIndented = true,
			Converters =
			{
				new JsonVersionConverter()
			},
			Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
		}));
	}

	public static void Restart()
	{
		Application.Restart();
		Environment.Exit(0);
	}

	public static void RequestRestart(string message)
	{
		if (MessageBox.Show(message, @"Info", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			Restart();
		}
	}

	public static void Initalize()
	{
		LoadSettings();
		LoadPlugins();
	}
}
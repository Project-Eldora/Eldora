using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Windows.Forms;
using Eldora.App.Plugins;
using Eldora.Utils;

namespace Eldora.App;

public static class EldoraApp
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
	public static SettingsModel SettingsModel { get; private set; }

	public static readonly JsonSerializerOptions DefaultSerializerOptions = new()
	{
		Converters =
		{
			new JsonVersionConverter()
		},
		WriteIndented = true,
		Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
	};

	/// <summary>
	/// Loads the installed plugins
	/// </summary>
	private static void LoadPlugins()
	{
		var pluginFolderContent = Directory.GetFiles(InternalPaths.PluginPath, "*.zip").ToList();

		var pluginsToDelete = new List<SettingsModel.InstalledPluginModel>();

		foreach (var settingsInstalledPlugin in SettingsModel.InstalledPlugins)
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

			if (found) continue;

			Log.Warn("Plugin {name} with version {version} not found", settingsInstalledPlugin.Name, settingsInstalledPlugin.Version);
			pluginsToDelete.Add(settingsInstalledPlugin);
		}

		SettingsModel.InstalledPlugins = SettingsModel.InstalledPlugins.Except(pluginsToDelete).ToList();
		SaveSettings();
	}

	/// <summary>
	/// Loads the settings from settings.json
	/// </summary>
	private static void LoadSettings()
	{
		try
		{
			if (!File.Exists(InternalPaths.SettingsPath))
			{
				LoadDefaultSettings();
			}

			SettingsModel = JsonSerializer.Deserialize<SettingsModel>(File.ReadAllText(InternalPaths.SettingsPath), DefaultSerializerOptions);
		}
		catch (JsonException e)
		{
			Log.Error(e);
			LoadDefaultSettings();
		}
	}

	private static void LoadDefaultSettings()
	{
		SettingsModel = new SettingsModel
		{
			PluginRepositories =
			{
				new SettingsModel.PluginRepositoryModel
				{
					Name = "Default",
					Url = "https://project-eldora.github.io/EldoraPlugins/plugins.json"
				}
			},
		};

		SaveSettings();
	}

	/// <summary>
	/// Saves the settings to settings.json
	/// </summary>
	public static void SaveSettings()
	{
		if (File.Exists(InternalPaths.SettingsPath)) File.Delete(InternalPaths.SettingsPath);
		File.WriteAllText(InternalPaths.SettingsPath, JsonSerializer.Serialize(SettingsModel, DefaultSerializerOptions));
	}

	/// <summary>
	/// Restarts the app
	/// </summary>
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

	public static void Initialize()
	{
		LoadSettings();
		LoadPlugins();
	}

	public static void Terminate()
	{
		foreach (var pluginContainer in PluginHandler.LoadedPlugins)
		{
			pluginContainer.CallUnload();
		}

		SaveSettings();
	}
}
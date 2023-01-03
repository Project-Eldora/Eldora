using System.Text.Json;
using Eldora.Extensions;

namespace Eldora.App;
internal class EldoraApp
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
	//public static SettingsModel SettingsModel { get; private set; }

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
		//var pluginFolderContent = Directory.GetFiles(InternalPaths.PackagesPath, "*.zip").ToList();

		//var pluginsToDelete = new List<SettingsModel.InstalledPluginModel>();

		//foreach (var settingsInstalledPlugin in SettingsModel.InstalledPlugins)
		//{
		//	var found = false;

		//	foreach (var file in pluginFolderContent)
		//	{
		//		if (!File.Exists(file)) continue;
		//		found = true;

		//		pluginFolderContent.Remove(file);
		//		break;
		//	}

		//	if (found) continue;

		//	Log.Warn("Plugin {name} with version {version} not found", settingsInstalledPlugin.Name, settingsInstalledPlugin.Version);
		//	pluginsToDelete.Add(settingsInstalledPlugin);
		//}

		//SettingsModel.InstalledPlugins = SettingsModel.InstalledPlugins.Except(pluginsToDelete).ToList();
		//SaveSettings();
	}

	/// <summary>
	/// Loads the settings from settings.json
	/// </summary>
	private static void LoadSettings()
	{
		//try
		//{
		//	if (!File.Exists(InternalPaths.SettingsFilePath))
		//	{
		//		LoadDefaultSettings();
		//	}

		//	SettingsModel = JsonSerializer.Deserialize<SettingsModel>(File.ReadAllText(InternalPaths.SettingsFilePath), DefaultSerializerOptions);
		//}
		//catch (JsonException e)
		//{
		//	Log.Error(e);
		//	LoadDefaultSettings();
		//}
	}

	private static void LoadDefaultSettings()
	{
		//SettingsModel = new SettingsModel
		//{
		//	PluginRepositories =
		//	{
		//		new SettingsModel.PluginRepositoryModel
		//		{
		//			Name = "Default",
		//			Url = "https://project-eldora.github.io/EldoraPlugins/plugins.json"
		//		}
		//	},
		//};

		//SaveSettings();
	}

	/// <summary>
	/// Saves the settings to settings.json
	/// </summary>
	public static void SaveSettings()
	{
		//if (File.Exists(InternalPaths.SettingsFilePath)) File.Delete(InternalPaths.SettingsFilePath);
		//File.WriteAllText(InternalPaths.SettingsFilePath, JsonSerializer.Serialize(SettingsModel, DefaultSerializerOptions));
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
		MessageBoxes.RequestYesNoConfirmation(message, @"Info", MessageBoxIcon.Information, confirmationCallback: Restart);
	}

	public static void Startup()
	{
		LoadSettings();
		LoadPlugins();
	}

	public static void Shutdown()
	{
		// unload extension

		SaveSettings();
	}
}

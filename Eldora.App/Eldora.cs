using System.IO;
using System.Text.Json;
using Eldora.App.Plugins;

namespace Eldora.App;

public class Eldora
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
	public static Eldora Instance { get; private set; }
	public Settings Settings { get; private set; }
	
	public Eldora()
	{
		Instance = this;

		LoadSettings();
		LoadPlugins();
	}
	
	private static void LoadPlugins()
	{
		foreach (var file in Directory.GetFiles(Paths.PluginPath))
		{
			Log.Debug("Plugins path containing {file}", file);
			PluginHandler.LoadPlugin(file).OnLoad();
		}
	}
	
	private void LoadSettings()
	{
		try
		{
			if (!File.Exists(Paths.SettingsPath))
			{
				LoadDefaultSettings();
			}

			Settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(Paths.SettingsPath));
		}
		catch (JsonException e)
		{
			Log.Error(e);
			LoadDefaultSettings();
		}
	}

	private void LoadDefaultSettings()
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

	public void SaveSettings()
	{
		if (File.Exists(Paths.SettingsPath)) File.Delete(Paths.SettingsPath);
		File.WriteAllText(Paths.SettingsPath, JsonSerializer.Serialize(Settings, new JsonSerializerOptions
		{
			WriteIndented = true
		}));
	}
}
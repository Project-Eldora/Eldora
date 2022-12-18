using System;
using System.IO;

namespace Eldora.App;

internal static class Paths
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
	
	public static string RootPath { get; private set; }
	public static string PluginPath  { get; private set; }
	public static string LanguagePath  { get; private set; }
	public static string LogPath { get; private set; }

	public static string SettingsPath { get; private set; }
	
	public static void CreateFolderStructure()
	{
		RootPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Eldora";
		PluginPath = $@"{RootPath}\Plugins"; 
		LogPath = $@"{RootPath}\Logs";
		SettingsPath = $@"{RootPath}\settings.json";

		CreateFolder(RootPath);
		CreateFolder(PluginPath);
		CreateFolder(LogPath);
	}

	private static void CreateFolder(string path)
	{
		if (Directory.Exists(path))
		{
			Log.Info("{path} already exists", path);
			return;
		}
		
		Log.Info("Creating {path}", path);
		Directory.CreateDirectory(path);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eldora.App;
static internal class InternalPaths
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	/// <summary>
	/// The root path for the application data
	/// </summary>
	public static string RootPath { get; private set; } = "";
	/// <summary>
	/// The path for the application extensions
	/// </summary>
	public static string PackagesPath { get; private set; } = "";
	/// <summary>
	/// The path for the package projects
	/// </summary>
	public static string PackageProjectsPath { get; private set; } = "";
	/// <summary>
	/// The path for languages
	/// </summary>
	public static string LanguagePath { get; private set; } = "";
	/// <summary>
	/// The path for the logs
	/// </summary>
	public static string LogPath { get; private set; } = "";
	/// <summary>
	/// The path for the global settings file
	/// </summary>
	public static string SettingsFilePath { get; private set; } = "";

	public static void CreateFolderStructure()
	{
		RootPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Eldora";
		PackagesPath = $@"{RootPath}\Packages";
		LanguagePath = $@"{RootPath}\Langs";
		LogPath = $@"{RootPath}\Logs";
		SettingsFilePath = $@"{RootPath}\config.xml";

		PackageProjectsPath = $@"{RootPath}\PackageProjects";

		CreateFolder(RootPath);
		CreateFolder(PackagesPath);
		CreateFolder(PackageProjectsPath);
		CreateFolder(LogPath);
	}

	private static void CreateFolder(string path)
	{
		if (Directory.Exists(path))
		{
			Log.Info("{path} already exists. SKIPPING", path);
			return;
		}

		Log.Info("Creating {path}", path);
		Directory.CreateDirectory(path);
	}
}

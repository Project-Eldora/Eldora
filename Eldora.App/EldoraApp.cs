using System.Text.Json;
using System.Xml.Serialization;
using Eldora.App.Packaging;
using Eldora.Extensions;

namespace Eldora.App;

internal class EldoraApp
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	private static ProgramSettingsModel PackageSettings = new();
	public static readonly List<BundledPackage> LoadedPackages = new();

	public static event EventHandler? PluginsChanged;

	/// <summary>
	/// Loads the installed plugins
	/// </summary>
	private static void LoadPlugins()
	{
		var packagesPath = Directory.GetDirectories(InternalPaths.PackagesPath);

		var allowedFolders = new List<string>();

		foreach (var pkg in PackageSettings.InstalledPackages)
		{
			var path = Path.Combine(InternalPaths.PackagesPath, $"{pkg.PackageName}-{pkg.Version}");
			if (!Directory.Exists(path))
			{
				Log.Warn("Could not load package {pkgFolder}", path);
			}
			allowedFolders.Add(path);
		}

		foreach (var pkgFolder in allowedFolders)
		{
			var package = BundledPackage.FromFolder(pkgFolder);
			if (package == null) continue;

			LoadedPackages.Add(package);
			package.Load();
		}

		foreach (var dir in Directory.GetDirectories(InternalPaths.PackagesPath).Where(f => !allowedFolders.Contains(f)))
		{
			Directory.Delete(dir, true);
		}
	}

	public static void InstallPackage(string packagePath)
	{
		var package = BundledPackage.FromFile(packagePath);
		if (package == null) return;

		PackageSettings.InstalledPackages.Add(new InstalledPackage
		{
			PackageName = package.PackageMetadata!.Identifier,
			Version = package.PackageMetadata!.Version.ToString(),
		});
		SaveSettings();

		LoadedPackages.Add(package);
		package.Load();

		OnPluginsChanged();
	}

	/// <summary>
	/// Loads the settings from settings.json
	/// </summary>
	private static void LoadSettings()
	{
		Log.Info("Loading Settings.");

		if (!File.Exists(InternalPaths.SettingsFilePath))
		{
			Log.Info(message: "Missing Settings, saving.");
			SaveSettings();
		}

		var serializer = new XmlSerializer(typeof(ProgramSettingsModel));
		using var stream = new FileStream(InternalPaths.SettingsFilePath, FileMode.Open);
		PackageSettings = (serializer.Deserialize(stream) as ProgramSettingsModel)!;
	}

	/// <summary>
	/// Saves the settings to settings.json
	/// </summary>
	public static void SaveSettings()
	{
		Log.Info(message: "Saving Settings.");

		var serializer = new XmlSerializer(typeof(ProgramSettingsModel));
		using var stream = new FileStream(InternalPaths.SettingsFilePath, FileMode.Create);
		serializer.Serialize(stream, PackageSettings);
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

	internal static void UninstallPackage(BundledPackage pkg)
	{
		var idx = PackageSettings.InstalledPackages.FirstOrDefault(p => p.PackageName == pkg.PackageMetadata!.Identifier);
		if (idx == default) return;

		pkg.Unload();
		LoadedPackages.Remove(pkg);
		PackageSettings.InstalledPackages.Remove(idx);
		pkg.Dispose();

		OnPluginsChanged();
	}

	private static void OnPluginsChanged()
	{
		PluginsChanged?.Invoke(null, EventArgs.Empty);
	}
}

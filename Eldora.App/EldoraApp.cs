using System.Text.Json;
using System.Xml.Serialization;
using Eldora.App.Packaging;
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

	private static ProgramSettingsModel PackageSettings = new();
	public static readonly List<BundledPackage> BundledPackages = new();

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
				Log.Warn("Could not load package {pkg}", path);
			}
			allowedFolders.Add(path);
		}

		foreach (var pkg in allowedFolders)
		{
			var package = new BundledPackage(pkg);
			package.Load();

			BundledPackages.Add(package);
		}

		foreach (var dir in Directory.GetDirectories(InternalPaths.PackagesPath).Where(f => !allowedFolders.Contains(f)))
		{
			Directory.Delete(dir, true);
		}
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
		using (var stream = new FileStream(InternalPaths.SettingsFilePath, FileMode.Open))
		{
			PackageSettings = (serializer.Deserialize(stream) as ProgramSettingsModel)!;
		}
	}

	/// <summary>
	/// Saves the settings to settings.json
	/// </summary>
	public static void SaveSettings()
	{
		Log.Info(message: "Saving Settings.");

		var serializer = new XmlSerializer(typeof(ProgramSettingsModel));
		using (var stream = new FileStream(InternalPaths.SettingsFilePath, FileMode.Create))
		{
			serializer.Serialize(stream, PackageSettings);
		}
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

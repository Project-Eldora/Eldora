using NLog.Config;
using NLog.Targets;
using NLog;
using Eldora.Packaging;
using Eldora.InputBoxes;

namespace Eldora.App;

internal static class ProgramEntry
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	private static void Main()
	{
		InternalPaths.CreateFolderStructure();
		InitiateLogging();
		EldoraApp.Startup();

		ApplicationConfiguration.Initialize();
		//Application.Run(new MainWindow());

		var project = PackageProject.Open("C:\\Users\\Ra6\\AppData\\Roaming\\Eldora\\PackageProjects\\Example Project 2");
		project?.CreateNew("bin/test.txt", false);
		project?.CreateNew("lib/lib2/test.dll", true);
		project?.Bundle();

		EldoraApp.Shutdown();
	}

	private static void InitiateLogging()
	{
		const string layout = "[${longdate} | ${level:uppercase=true}] (${logger}) > ${message:withexception=true}";

		var config = new LoggingConfiguration();

		//var logfile = new FileTarget("log_file")
		//{
		//	FileName = $@"{InternalPaths.LogPath}\log.log",
		//	Layout = layout,
		//	ArchiveOldFileOnStartup = true
		//};
		var consoleTarget = new ConsoleTarget("log_console")
		{
			Layout = layout
		};

		var debugLogFile = new FileTarget("debug_log_file")
		{
			FileName = $@"{InternalPaths.LogPath}\debug.log",
			Layout = layout,
			ArchiveOldFileOnStartup = true
		};

		config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);
		config.AddRule(LogLevel.Debug, LogLevel.Fatal, debugLogFile);

		LogManager.Configuration = config;
	}
}
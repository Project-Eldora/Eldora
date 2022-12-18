using System.IO;

namespace Eldora.Utils;

public static class ExplorerTools
{
	/// <summary>
	/// Opens the explorer and selects the file if the file exists
	/// </summary>
	/// <param name="file"></param>
	public static void OpenAndSelect(string file)
	{
		if (!File.Exists(file))
		{
			return;
		}

		var argument = "/select, \"" + file + "\"";

		System.Diagnostics.Process.Start("explorer.exe", argument);
	}
}

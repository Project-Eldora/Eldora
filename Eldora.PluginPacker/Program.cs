using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eldora.PluginPacker
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Console.WriteLine(string.Join(",", args));
			if (args.Length == 0)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Form1());
			}
			else
			{
				var folder = args[0];
				var outputName = args[1];
				Console.WriteLine(folder);
				Console.WriteLine(outputName);

				var files = Directory.GetFiles(folder);

				using var memoryStream = new MemoryStream();

				using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
				{
					foreach (var file in files)
					{
						var fileName = file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
						archive.CreateEntryFromFile(file, fileName.Equals("plugininfo.json") ? "plugininfo.json" : $"content/{fileName}");
						
						Console.WriteLine(fileName);
					}
				}
				
				if(File.Exists(outputName)) File.Delete(outputName);
				using (var fileStream = new FileStream(outputName, FileMode.Create))
				{
					memoryStream.Seek(0, SeekOrigin.Begin);
					memoryStream.CopyTo(fileStream);
				}
			}
		}
	}
}
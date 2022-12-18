using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace Eldora.PluginPacker
{
	public sealed partial class Form1 : Form
	{
		private string _selectedPluinInfoJsonPath = "";
		private string _selectedFolderPath = "";
		
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			openFileDialog1.Filter = @"PluginInfo.json|plugininfo.json";
			openFileDialog1.FileName = "";
			openFileDialog1.Multiselect = false;
			
			if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
			
			var file = openFileDialog1.FileName;
			_selectedPluinInfoJsonPath = file;

			label1.Text = $@"Selected: {_selectedPluinInfoJsonPath}";
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() != DialogResult.OK) return;
			
			var folder = folderBrowserDialog1.SelectedPath;
			_selectedFolderPath = folder;

			label2.Text = $@"Selected: {_selectedFolderPath}";
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(_selectedPluinInfoJsonPath))
			{
				MessageBox.Show(@"Please select a plugininfo.json");
				return;
			}
			
			if (string.IsNullOrEmpty(_selectedFolderPath))
			{
				MessageBox.Show(@"Please select a folder");
				return;
			}

			saveFileDialog1.Filter = @"Plugin|*.zip";
			
			if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

			if (File.Exists(saveFileDialog1.FileName))
			{
				if (MessageBox.Show(@"Zip already exists. Override?") != DialogResult.OK) return;
				File.Delete(saveFileDialog1.FileName);
			}
			
			var files = Directory.GetFiles(_selectedFolderPath);

			using var memoryStream = new MemoryStream();

			using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
			{
				foreach (var file in files)
				{
					var fileName = file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
					archive.CreateEntryFromFile(file, $"content/{fileName}");
				}
					
				archive.CreateEntryFromFile(_selectedPluinInfoJsonPath, "plugininfo.json");
			}

			using (var fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create))
			{
				memoryStream.Seek(0, SeekOrigin.Begin);
				memoryStream.CopyTo(fileStream);
			}

			MessageBox.Show($@"Saved to {saveFileDialog1.FileName}");
		}
	}
}
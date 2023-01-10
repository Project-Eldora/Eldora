using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Eldora.App;
using Eldora.App.Packaging;
using Eldora.Extensions;
using Microsoft.VisualBasic.Logging;

namespace Eldora.Packaging;

public class PackageProject
{
	public const string PackageExtension = "eldpkg";
	public const string PackageMetadataExtension = "eldmt";
	private const string ProjectMetaFileName = "project.eldprj";
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	private readonly string _projectPath;
	private readonly PackageProjectMetaModel _projectMetadata;

	public PackageProjectMetaModel ProjectMetadata => _projectMetadata;

	public PackageProject(string projectPath, PackageProjectMetaModel metadata)
	{
		_projectPath = projectPath;
		_projectMetadata = metadata;

		CheckProjectFiles();
	}

	/// <summary>
	/// Checks if the projectfiles exist in the file system
	/// </summary>
	/// <exception cref="NotImplementedException"></exception>
	private void CheckProjectFiles()
	{
		var missing = new List<PackageProjectFileModel>();
		_projectMetadata.LibraryFiles.ForEach(f =>
		{
			var filePath = ConvertProjectPathToFullPath(f.FileName);
			if (File.Exists(filePath)) return;

			Log.Info("Project File {file} does not exist. Removing it from the file.", f.FileName);
			missing.Add(f);
		});

		missing.ForEach(f => _projectMetadata.LibraryFiles.Remove(f));
		SaveProjectFile();

		var bundleFolder = Path.Combine(_projectPath, _projectMetadata.OutputFolder);

		if (!Directory.Exists(bundleFolder))
		{
			Directory.CreateDirectory(bundleFolder);
		}
	}

	/// <summary>
	/// Converts a local path to a full path
	/// </summary>
	/// <param name="projectPath"></param>
	/// <returns></returns>
	private string ConvertProjectPathToFullPath(string projectPath)
	{
		return Path.Combine(_projectPath, projectPath);
	}

	/// <summary>
	/// Converts any given path to the local format
	/// </summary>
	/// <param name="fullPath"></param>
	/// <returns></returns>
	private static string ConvertFullPathToProjectPath(string fullPath)
	{
		return fullPath[(fullPath.LastIndexOf("\\") + 1)..];
	}

	/// <summary>
	/// Removes a file
	/// </summary>
	/// <param name="fileName"></param>
	public void RemoveFile(string fileName, bool isLibraryFile)
	{
		var files = isLibraryFile switch
		{
			true => _projectMetadata.LibraryFiles,
			false => _projectMetadata.ContentFiles
		};

		var toRemove = files.FirstOrDefault(p => p.FileName == fileName);
		if (toRemove == default) return;

		files.Remove(toRemove);

		SaveProjectFile();
	}

	/// <summary>
	/// Creates a new file
	/// </summary>
	/// <param name="fileName"></param>
	public void CreateNew(string fileName, bool isLibraryFile)
	{
		var fullLocalPath = ConvertProjectPathToFullPath(fileName);

		var files = isLibraryFile switch
		{
			true => _projectMetadata.LibraryFiles,
			false => _projectMetadata.ContentFiles
		};

		if (File.Exists(fullLocalPath) || files.Any(f => f.FileName == fileName))
		{
			Log.Info("File {filePath} does already exist. SKIPPING", fullLocalPath);
			return;
		}

		Log.Info("Creating file {filePath}", fullLocalPath);
		Directory.CreateDirectory(Path.GetDirectoryName(fullLocalPath)!);
		using var fs = File.Create(fullLocalPath);

		files.Add(new PackageProjectFileModel
		{
			FileName = fileName
		});

		SaveProjectFile();
	}

	/// <summary>
	/// Adds a file from the filesystem
	/// </summary>
	/// <param name="filePath"></param>
	public void AddFile(string filePath, bool isLibraryFile = false)
	{
		var localPath = ConvertFullPathToProjectPath(filePath);
		var fullLocalPath = ConvertProjectPathToFullPath(localPath);

		if (!File.Exists(filePath))
		{
			Log.Info("File {filePath} does not exist. SKIPPING", filePath);
			return;
		}

		if (!File.Exists(fullLocalPath))
		{
			Log.Info("File {filePath} does not exist. COPYING", fullLocalPath);
			File.Copy(filePath, fullLocalPath);
		}

		var files = isLibraryFile switch
		{
			true => _projectMetadata.LibraryFiles,
			false => _projectMetadata.ContentFiles
		};

		if (files.Any(f => f.FileName == localPath)) return;
		files.Add(new PackageProjectFileModel
		{
			FileName = localPath
		});

		SaveProjectFile();
	}

	/// <summary>
	/// Saves the project file
	/// </summary>
	public void SaveProjectFile()
	{
		try
		{
			var serializer = new XmlSerializer(typeof(PackageProjectMetaModel));
			using var stream = new FileStream(Path.Combine(_projectPath, ProjectMetaFileName), FileMode.Create);
			serializer.Serialize(stream, _projectMetadata);
		}
		catch (Exception e)
		{
			Log.Error("Could not save Project file from project {project}. (Message): {exception}", _projectPath, e);
		}
	}

	#region Bundling
	/// <summary>
	/// Bundles the project to a package
	/// </summary>
	/// <param name="path"></param>
	public void Bundle()
	{
		CheckProjectFiles();

		var bundleName = $"{_projectMetadata.PackageMetadata.Identifier}.{_projectMetadata.PackageMetadata.Version}.{PackageExtension}";
		var bundlePath = Path.Combine(_projectPath, _projectMetadata.OutputFolder, bundleName);

		Log.Info("---------------- Beginning bundeling of package {name} ----------------", bundleName);

		using (var memoryStream = new MemoryStream())
		{
			using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
			{
				BundleMetadataFile(archive);

				_projectMetadata.LibraryFiles.ForEach(f => BundleProjectFile(archive, f, true));
				_projectMetadata.ContentFiles.ForEach(f => BundleProjectFile(archive, f, false));
			}

			if (File.Exists(bundlePath)) File.Delete(bundlePath);

			using var fileStream = new FileStream(bundlePath, FileMode.Create);
			memoryStream.Seek(0, SeekOrigin.Begin);
			memoryStream.CopyTo(fileStream);
		}
		Log.Info("---------------- Finished bundeling of package {name} ----------------", bundleName);
	}

	private void BundleProjectFile(ZipArchive archive, PackageProjectFileModel fileModel, bool isLibraryFile)
	{
		Log.Info("Adding file {fileModel} to Bundle", fileModel.FileName);

		var fileModelName = fileModel.FileName.Replace("\\", "/");

		var archivePath = fileModelName[(fileModelName.LastIndexOf("/") + 1)..];
		if (isLibraryFile)
		{
			archivePath = Path.Combine("lib", archivePath);
		}

		var filePath = Path.Combine(_projectPath, ConvertProjectPathToFullPath(fileModel.FileName));
		if (!File.Exists(filePath)) return;

		archive.CreateEntryFromFile(filePath, archivePath);
	}

	private void BundleMetadataFile(ZipArchive archive)
	{
		Log.Info("Adding Meta file to Bundle");
		var metaFile = archive.CreateEntry($"{_projectMetadata.PackageMetadata.Identifier}.{PackageMetadataExtension}");
		using var stream = metaFile.Open();

		var serializer = new XmlSerializer(typeof(PackageMetadataModel));
		serializer.Serialize(stream, _projectMetadata.PackageMetadata);
	}
	#endregion

	/// <summary>
	/// Returns null if the directory does not exist, the meta filePath does not exist or there is a problem deserializing the meta filePath
	/// </summary>
	/// <param name="folderPath"></param>
	/// <returns></returns>
	public static PackageProject? Open(string folderPath)
	{
		if (!Directory.Exists(folderPath))
		{
			MessageBox.Show($"The path \"{folderPath}\" does not exist.", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return null;
		}

		var metaFile = Path.Combine(folderPath, ProjectMetaFileName);
		if (!File.Exists(metaFile))
		{
			MessageBox.Show($"The path \"{folderPath}\" is not a valid project.", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return null;
		}

		PackageProjectMetaModel? result;

		var serializer = new XmlSerializer(typeof(PackageProjectMetaModel));
		using (var stream = new FileStream(metaFile, FileMode.Open))
		{
			result = serializer.Deserialize(stream) as PackageProjectMetaModel;
		}
		if (result == null) return null;
		return new PackageProject(folderPath, result);
	}

	/// <summary>
	/// Returns null if the directory already exists
	/// </summary>
	/// <param name="folderPath"></param>
	/// <param name="projectName"></param>
	/// <returns></returns>
	public static PackageProject Create(string folderPath, string projectName)
	{
		var completeProjectPath = FileExtensions.NextAvailableDirectoryName(Path.Combine(folderPath, projectName));

		// Creates project path
		Directory.CreateDirectory(completeProjectPath);

		var pkg = new PackageMetadataModel
		{
			Identifier = projectName.Replace(" ", "_"),
		};
		var metadata = new PackageProjectMetaModel
		{
			ProjectName = projectName,
			PackageMetadata = pkg,
		};

		// Creates the output folder
		Directory.CreateDirectory(Path.Combine(completeProjectPath, metadata.OutputFolder));

		var serializer = new XmlSerializer(typeof(PackageProjectMetaModel));
		using (var stream = new FileStream(Path.Combine(completeProjectPath, ProjectMetaFileName), FileMode.Create))
		{
			serializer.Serialize(stream, metadata);
		}

		return new PackageProject(completeProjectPath, metadata);
	}
}

[XmlRoot("Project")]
public class PackageProjectMetaModel
{
	public string ProjectName { get; set; } = "Default";
	public string OutputFolder { get; set; } = "bundled";

	[XmlArrayItem("File")]
	public List<PackageProjectFileModel> LibraryFiles { get; set; } = new();

	[XmlArrayItem("File")]
	public List<PackageProjectFileModel> ContentFiles { get; set; } = new();

	public PackageMetadataModel PackageMetadata { get; set; } = new();
}

public class PackageProjectFileModel
{
	[XmlAttribute("name")]
	public string FileName { get; set; } = string.Empty;
}

/*
 * 	#region INotifyPropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
	protected bool SetField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "")
	{
		if (EqualityComparer<T>.Default.Equals(field, newValue)) return false;
		field = newValue;
		OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		return true;
	}
	#endregion
 */
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Eldora.Packaging;
using Eldora.Packaging.API.Attributes;

namespace Eldora.App.Packaging;

internal class BundledPackage : IDisposable
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	private PackageLoadContext _context;
	private string _path;
	private string _libPath;

	public object MainInstance { get; private set; }
	public Assembly RootAssembly { get; private set; }
	public bool IsValid { get; private set; } = false;

	public PackageMetadataModel? PackageMetadata { get; private set; }


	private readonly List<MethodInfo> _onLoadMethod = new();
	private readonly List<MethodInfo> _onUnloadMethod = new();

	public static BundledPackage? FromFolder(string path)
	{
		var result = new BundledPackage
		{
			_path = path.Replace("/", "\\")
		};
		result._libPath = Path.Combine(result._path, "lib");

		var packageName = Path.GetFileName(result._path)!;
		var packageNameWithoutVersion = packageName[..packageName.LastIndexOf("-")];

		var packedPackagePath = Path.Combine(result._path, $"{packageName}.{PackageProject.PackageExtension}");

		if (!File.Exists(packedPackagePath))
		{
			Log.Error("Could not validate package {pkg}.", packedPackagePath);
			return null;
		}

		if (Directory.Exists(result._libPath))
		{
			Directory.Delete(result._libPath, true);
		}
		Directory.CreateDirectory(result._libPath);

		using var packageFile = ZipFile.OpenRead(packedPackagePath);
		if (packageFile == null) return null;

		var metaFileEntry = packageFile.Entries.FirstOrDefault(entry => entry.FullName.Equals($"{packageNameWithoutVersion}.{PackageProject.PackageMetadataExtension}"));
		if (metaFileEntry == null)
		{
			Log.Error("Missing meta file!");
			return null;
		}

		var serializer = new XmlSerializer(typeof(PackageMetadataModel));
		using (var stream = metaFileEntry.Open())
		{
			result.PackageMetadata = serializer.Deserialize(stream) as PackageMetadataModel;
		}
		if (result.PackageMetadata == null) return null;

		var libfiles = packageFile.Entries.Where(entry => entry.FullName.StartsWith("lib")).ToList();
		foreach (var libfile in libfiles)
		{
			var targetFile = Path.Combine(result._path, libfile.FullName);
			libfile.ExtractToFile(targetFile);
		}

		var entryAssemblyPath = Path.Combine(result._libPath, $"{result.PackageMetadata.Identifier}.dll");
		if (!File.Exists(entryAssemblyPath))
		{
			Log.Error("Missing file {entry}", entryAssemblyPath);
			return null;
		};

		result.IsValid = true;
		return result;
	}

	public static BundledPackage? FromFile(string filePath)
	{
		var packageName = Path.GetFileNameWithoutExtension(filePath)!;
		var packageNameNoVersion = packageName[..packageName.LastIndexOf("-")];

		using (var packageFile = ZipFile.OpenRead(filePath))
		{
			PackageMetadataModel? metadata = null;
			if (packageFile == null) return null;

			var metaFileEntry = packageFile.Entries.FirstOrDefault(entry => entry.FullName.Equals($"{packageNameNoVersion}.{PackageProject.PackageMetadataExtension}"));
			if (metaFileEntry == null)
			{
				Log.Error("Missing meta file!");
				return null;
			}

			var serializer = new XmlSerializer(typeof(PackageMetadataModel));
			using (var stream = metaFileEntry.Open())
			{
				metadata = serializer.Deserialize(stream) as PackageMetadataModel;
			}
			if (metadata == null) return null;

			var existing = EldoraApp.LoadedPackages.FirstOrDefault(pkg => pkg.PackageMetadata!.Identifier == metadata.Identifier);
			if (existing != default)
			{
				Log.Warn("Package {pkg} is already installed! Uninstall package first!", existing.PackageMetadata!.Identifier);
				return null;
			}
		}

		var targetFolderPath = Path.Combine(InternalPaths.PackagesPath, $"{packageName}");
		var targetFilePath = Path.Combine(targetFolderPath, $"{packageName}.{PackageProject.PackageExtension}");
		Directory.CreateDirectory(targetFolderPath);
		File.Copy(filePath, targetFilePath);

		return FromFolder(targetFolderPath);
	}

	/// <summary>
	/// Loads the package
	/// </summary>
	public void Load()
	{
		if (!IsValid) return;

		var entryAssemblyPath = Path.Combine(_libPath, $"{PackageMetadata!.Identifier}.dll");
		_context = new PackageLoadContext(_libPath);
		RootAssembly = _context.LoadFromAssemblyPath(entryAssemblyPath);

		foreach (var type in RootAssembly.GetTypes())
		{
			if (!Attribute.IsDefined(type, typeof(PackageEntryAttribute))) continue;
			MainInstance = Activator.CreateInstance(type)!;

			foreach (var method in type.GetMethods())
			{
				if (Attribute.IsDefined(method, typeof(OnPackageLoadedAttribute)))
				{
					_onLoadMethod.Add(method);
					break;
				}

				if (Attribute.IsDefined(method, typeof(OnPackageUnloadedAttribute)))
				{
					_onUnloadMethod.Add(method);
					break;
				}
			}

			break;
		}

		_onLoadMethod.ForEach(info => info.Invoke(MainInstance, null));
	}

	/// <summary>
	/// Unloads the package
	/// </summary>
	public void Unload()
	{
		if (PackageMetadata == null) return;

		_onUnloadMethod.ForEach(info => info.Invoke(MainInstance, null));

		_context.Unload();
	}

	public void Dispose()
	{
		Log.Info("Disposing {pkg}", PackageMetadata?.Identifier);
	}
}

internal class PackageLoadContext : AssemblyLoadContext
{
	private AssemblyDependencyResolver _dependencyResolver;

	public PackageLoadContext(string packagePath) : base(true)
	{
		_dependencyResolver = new AssemblyDependencyResolver(packagePath);
	}

	protected override Assembly? Load(AssemblyName assemblyName)
	{
		var assemblyPath = _dependencyResolver.ResolveAssemblyToPath(assemblyName);
		if (assemblyPath == null) return null;
		return LoadFromAssemblyPath(assemblyPath);
	}

	protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
	{
		var libraryPath = _dependencyResolver.ResolveUnmanagedDllToPath(unmanagedDllName);
		if (libraryPath == null) return IntPtr.Zero;
		return LoadUnmanagedDllFromPath(libraryPath);
	}
}
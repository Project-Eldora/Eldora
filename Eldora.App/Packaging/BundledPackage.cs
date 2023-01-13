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

internal class BundledPackage
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	private PackageLoadContext _context;
	private readonly string _path;
	private readonly string _libPath;

	private PackageMetadataModel? _packageMetadata;
	private object _mainInstance;

	public object MainInstance => _mainInstance;

	public Assembly RootAssembly { get; private set; }
	public bool IsValid { get; private set; } = false;

	private readonly List<MethodInfo> _onLoadMethod = new();
	private readonly List<MethodInfo> _onUnloadMethod = new();

	public BundledPackage(string path)
	{
		_path = path.Replace("/", "\\");
		_libPath = Path.Combine(_path, "lib");

		OpenAndExtractPackage();
	}

	private void OpenAndExtractPackage()
	{
		var packageName = Path.GetFileName(_path)!;
		var packageNameWithoutVersion = packageName[..packageName.LastIndexOf("-")];

		var packedPackagePath = Path.Combine(_path, $"{packageName}.{PackageProject.PackageExtension}");

		if (!File.Exists(packedPackagePath))
		{
			Log.Error("Could not validate package {pkg}.", packedPackagePath);
			return;
		}

		if (Directory.Exists(_libPath))
		{
			Directory.Delete(_libPath, true);
		}
		Directory.CreateDirectory(_libPath);

		using var packageFile = ZipFile.OpenRead(packedPackagePath);
		if (packageFile == null) return;

		var metaFileEntry = packageFile.Entries.FirstOrDefault(entry => entry.FullName.Equals($"{packageNameWithoutVersion}.{PackageProject.PackageMetadataExtension}"));
		if (metaFileEntry == null)
		{
			Log.Error("Missing meta file!");
			return;
		}

		var serializer = new XmlSerializer(typeof(PackageMetadataModel));
		using (var stream = metaFileEntry.Open())
		{
			_packageMetadata = serializer.Deserialize(stream) as PackageMetadataModel;
		}
		if (_packageMetadata == null) return;

		var libfiles = packageFile.Entries.Where(entry => entry.FullName.StartsWith("lib")).ToList();
		foreach (var libfile in libfiles)
		{
			var targetFile = Path.Combine(_path, libfile.FullName);
			libfile.ExtractToFile(targetFile);
		}

		var entryAssemblyPath = Path.Combine(_libPath, $"{_packageMetadata.Identifier}.dll");
		if (!File.Exists(entryAssemblyPath))
		{
			Log.Error("Missing file {entry}", entryAssemblyPath);
			return;
		};

		IsValid = true;
	}

	/// <summary>
	/// Loads the package
	/// </summary>
	public void Load()
	{
		if (!IsValid) return;

		var entryAssemblyPath = Path.Combine(_libPath, $"{_packageMetadata!.Identifier}.dll");
		_context = new PackageLoadContext(_libPath);
		RootAssembly = _context.LoadFromAssemblyPath(entryAssemblyPath);

		foreach (var type in RootAssembly.GetTypes())
		{
			if (!Attribute.IsDefined(type, typeof(PackageEntryAttribute))) continue;
			_mainInstance = Activator.CreateInstance(type)!;

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

		_onLoadMethod.ForEach(info => info.Invoke(_mainInstance, null));
	}

	/// <summary>
	/// Unloads the package
	/// </summary>
	public void Unload()
	{
		if (_packageMetadata == null) return;

		_onUnloadMethod.ForEach(info => info.Invoke(_mainInstance, null));

		_context.Unload();
	}
}

internal class PackageLoadContext : AssemblyLoadContext
{
	private AssemblyDependencyResolver _dependencyResolver;

	public PackageLoadContext(string packagePath)
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
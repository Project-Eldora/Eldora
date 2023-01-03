using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.Logging;

namespace Eldora.Packaging;

public static class EldoraPackage
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	public const string PackedExtensionExt = ".eldpkg";

	public static void UnpackPackage(string packagePath, out PackageProject package)
	{
		package = new();
	}
}

public class PackageProject
{
	private PackageMetadata _metadata = new();
	private readonly Dictionary<string, string> _packageContents = new();

	public PackageProject()
	{

	}

	/// <summary>
	/// Bundles the project to a package
	/// </summary>
	/// <param name="path"></param>
	public void Bundle(string path)
	{
	}
}


public class PackageMetadata
{
}
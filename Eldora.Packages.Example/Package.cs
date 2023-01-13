using Eldora.Packaging.API.Attributes;

namespace Eldora.Packages.Example;

[PackageEntry]
public class Package
{
	[OnPackageLoaded]
	public void OnLoad()
	{

	}

	[OnPackageUnloaded]
	public void OnUnload()
	{

	}
}

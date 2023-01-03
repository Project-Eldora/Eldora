using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eldora.App.Packaging;

[XmlRoot("Package")]
public class PackageMetadataModel
{
	private Version _version = new(0, 1);
	private string _versionStr = "0.1";

	public string Identifier { get; set; } = "";

	[XmlElement("Version")]
	public string VersionString
	{
		get
		{
			return _versionStr;
		}
		set
		{
			if (!Version.TryParse(value, out var result)) return;
			_versionStr = value;
			_version = result;
		}
	}

	[XmlIgnore]
	public Version Version
	{
		get => _version;
		set
		{
			_version = value;
			_versionStr = _version.ToString();
		}
	}

	public string Title { get; set; } = "";

	[XmlArray("Authors")]
	[XmlArrayItem("Author")]
	public List<string> Authors { get; set; } = new();

	public string License { get; set; } = "";
	public string LicenseUrl { get; set; } = "";
	public string Icon { get; set; } = "";
	public string ProjectReference { get; set; } = "";
	public string Description { get; set; } = "";
	public string Copyright { get; set; } = "";

	[XmlArray("Tags")]
	[XmlArrayItem("Tag")]
	public List<string> Tags { get; set; } = new();

	public PackageMetadataRepositoryModel Repository { get; set; } = new();

	[XmlArray("Dependencies")]
	[XmlArrayItem("Dependency")]
	public List<PackageMetadataDependencyModel> Dependencies { get; set; } = new();

}

public class PackageMetadataDependencyModel
{
	[XmlAttribute("id")]
	public string Identifier { get; set; } = "";

	[XmlAttribute("version")]
	public string VersionString { get; set; } = "";

	[XmlIgnore]
	public Version Version { get; set; } = new();
}

public class PackageMetadataRepositoryModel
{
	[XmlAttribute("url")]
	public string Url { get; set; } = "";

	[XmlAttribute("type")]
	public RepositoryType Type { get; set; } = RepositoryType.None;

	public enum RepositoryType
	{
		[XmlEnum("none")]
		None,
		[XmlEnum("git")]
		Git
	}
}

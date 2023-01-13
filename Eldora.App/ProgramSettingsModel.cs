using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eldora.App;

[XmlRoot("ProgramConfig")]
public class ProgramSettingsModel
{
	[XmlArrayItem("Package")]
	public List<InstalledPackage> InstalledPackages = new();
}

public class InstalledPackage
{
	[XmlAttribute("name")]
	public string PackageName = "";
	[XmlAttribute("version")]
	public string Version = "";
}
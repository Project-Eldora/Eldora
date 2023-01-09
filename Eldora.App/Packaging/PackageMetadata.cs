using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Eldora.App.Packaging;

[XmlRoot("Package")]
public class PackageMetadataModel : INotifyPropertyChanged
{
	#region PropertyChanged
	public event PropertyChangedEventHandler? PropertyChanged;
	protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);

	protected void SetField<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(field, newValue)) return;

		field = newValue;
		OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
	}
	#endregion

	private Version _version = new(0, 1);
	private string _identifier = "";
	private string _title = "";
	private List<string> _authors = new();
	private string _license = "";
	private string _licenseUrl = "";
	private string _icon = "";
	private string _projectReference = "";
	private string _description = "";
	private string _copyright = "";
	private List<string> _tags = new();
	private PackageMetadataRepositoryModel _repository = new();
	private List<PackageMetadataDependencyModel> _dependencies = new();

	public string Identifier { get => _identifier; set => SetField(ref _identifier, value); }
	[XmlElement("Version")]
	public string VersionString
	{
		get
		{
			return _version.ToString();
		}
		set
		{
			if (!Version.TryParse(value, out var result)) return;
			SetField(ref _version, result);
		}
	}

	[XmlIgnore]
	public Version Version { get => _version; set => SetField(ref _version, value); }

	public string Title { get => _title; set => SetField(ref _title, value); }

	[XmlArray("Authors")]
	[XmlArrayItem("Author")]
	public List<string> Authors { get => _authors; set => SetField(ref _authors, value); }

	public string License { get => _license; set => SetField(ref _license, value); }

	public string LicenseUrl { get => _licenseUrl; set => SetField(ref _licenseUrl, value); }

	public string Icon { get => _icon; set => SetField(ref _icon, value); }

	public string ProjectReference { get => _projectReference; set => SetField(ref _projectReference, value); }

	public string Description { get => _description; set => SetField(ref _description, value); }

	public string Copyright { get => _copyright; set => SetField(ref _copyright, value); }

	[XmlArray("Tags")]
	[XmlArrayItem("Tag")]
	public List<string> Tags { get => _tags; set => SetField(ref _tags, value); }

	public PackageMetadataRepositoryModel Repository { get => _repository; set => SetField(ref _repository, value); }

	[XmlArray("Dependencies")]
	[XmlArrayItem("Dependency")]
	public List<PackageMetadataDependencyModel> Dependencies { get => _dependencies; set => SetField(ref _dependencies, value); }

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

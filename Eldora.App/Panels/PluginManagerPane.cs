using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eldora.App.Plugins;
using Eldora.Utils;
using Eldora.WinUtils;
using NLog;

namespace Eldora.App.Panels;

public partial class PluginManagerPane : UserControl
{
	private static readonly HttpClient HttpClient = new();
	private static readonly WebClient WebClient = new();
	private static readonly Logger Log = LogManager.GetCurrentClassLogger();

	private readonly ListViewGroup _installedPlugins = new("Installed", HorizontalAlignment.Left)
	{
		Name = "Installed",
	};

	private readonly List<(PluginRepository repo, string name)> _fetchedRepositories = new();

	public PluginManagerPane()
	{
		InitializeComponent();
	}

	protected override void OnGotFocus(EventArgs e)
	{
		LoadItems();
	}

	protected override void OnLoad(EventArgs e)
	{
		FetchRepositories();

		btnInstallFromRepo.Enabled = false;
		btnInstallFromDisk.Enabled = true;
		btnUninstall.Enabled = false;

		var columnHeaders = new[]
		{
			new ColumnHeader
			{
				Text = @"Plugin Name"
			},
			new ColumnHeader
			{
				Text = @"Version"
			},
			new ColumnHeader
			{
				Text = @"Author"
			},
			new ColumnHeader
			{
				Text = @"Description"
			}
		};
		lvwPlugins.Columns.AddRange(columnHeaders);

		LoadItems();

		PluginHandler.PluginsGotChanged += (_, _) => { LoadItems(); };
	}

	private void LoadItems()
	{
		lvwPlugins.BeginUpdate();
		lvwPlugins.Items.Clear();
		// Add existing plugins to installed group
		foreach (var loadedPlugin in PluginHandler.LoadedPlugins)
		{
			lvwPlugins.Items.Add(CreateItemFromPlugin(loadedPlugin));
		}

		foreach (var repositoryEntry in _fetchedRepositories)
		{
			var group = new ListViewGroup($"Repository: {repositoryEntry.name}", HorizontalAlignment.Left)
			{
				Name = $"Repository: {repositoryEntry.name}"
			};

			foreach (var pluginEntry in repositoryEntry.repo.Entries)
			{
				var item = new ListViewItem(new[]
				{
					pluginEntry.Name,
					pluginEntry.Version.ToString(),
					pluginEntry.Author,
					pluginEntry.Description,
				})
				{
					Group = group,
					Tag = pluginEntry
				};

				lvwPlugins.Items.Add(item);
			}

			lvwPlugins.Groups.Add(group);
		}

		lvwPlugins.Groups.Add(_installedPlugins);
		lvwPlugins.EndUpdate();

		lvwPlugins.AutoSizeColumns();
	}

	private ListViewItem CreateItemFromPlugin(EldoraPlugin plugin)
	{
		var item = new ListViewItem(new[]
		{
			plugin.PluginInfo.PluginName,
			plugin.PluginInfo.PluginVersion.ToString(),
			plugin.PluginInfo.Author,
			plugin.PluginInfo.Description
		})
		{
			Group = _installedPlugins,
			Tag = plugin
		};

		return item;
	}

	private async void FetchRepositories()
	{
		_fetchedRepositories.Clear();
		foreach (var pluginRepo in Eldora.Settings.PluginRepositories)
		{
			try
			{
				Log.Info("Fetching Online Repository {pluginName}", pluginRepo.Name);
				var httpResponseMessage = await HttpClient.GetAsync(pluginRepo.Url);
				var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
				using var reader = new StreamReader(stream);

				var fileData = await reader.ReadToEndAsync();
				var repository = JsonSerializer.Deserialize<PluginRepository>(fileData, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true,
					Converters =
					{
						new JsonVersionConverter()
					},
					Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				});
				repository.Url = pluginRepo.Url;

				foreach (var repositoryEntry in repository.Entries)
				{
					repositoryEntry.FullUrl = repository.Url.Substring(0, repository.Url.LastIndexOf("/", StringComparison.Ordinal)) + "/plugin-list/" + repositoryEntry.FileName;
				}

				_fetchedRepositories.Add((repository, pluginRepo.Name));
			}
			catch (Exception e)
			{
				Log.Error("Could not fetch ({name}|{url}). Cause {exception}", pluginRepo.Name, pluginRepo.Url, e);
			}
		}

		LoadItems();
	}

	// TODO: Extract to own file
	public class PluginRepository
	{
		[JsonPropertyName("entries")] public List<PluginEntry> Entries { get; set; } = new();
		[JsonIgnore] public string Url { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("Entries:");
			sb.Append("\n");
			Entries.ForEach(e => sb.Append(e).Append("\n"));
			return sb.ToString();
		}

		public class PluginEntry
		{
			[JsonPropertyName("name")] public string Name { get; set; }
			[JsonPropertyName("version")] public Version Version { get; set; }
			[JsonPropertyName("file_name")] public string FileName { get; set; }
			[JsonPropertyName("author")] public string Author { get; set; }
			[JsonPropertyName("description")] public string Description { get; set; }
			[JsonPropertyName("tags")] public string Tags { get; set; }
			[JsonIgnore] public string FullUrl { get; set; }

			public override string ToString()
			{
				return $"{nameof(Name)}: {Name}, {nameof(Version)}: {Version}, {nameof(FileName)}: {FileName}, {nameof(Author)}: {Author}, {nameof(Description)}: {Description}, {nameof(Tags)}: {Tags}";
			}
		}
	}

	private void lvwPlugins_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
	{
		Log.Info("Selected {item}", e.Item.Tag);
		btnInstallFromRepo.Enabled = false;
		btnUninstall.Enabled = false;

		switch (e.Item.Tag)
		{
			case EldoraPlugin plugin:
				btnInstallFromRepo.Enabled = false;
				btnUninstall.Enabled = true;
				return;
			case PluginRepository.PluginEntry entry:
				btnInstallFromRepo.Enabled = true;
				return;
		}
	}

	private void btnInstallFromRepo_Click(object sender, EventArgs e)
	{
		if (lvwPlugins.SelectedItems.Count == 0) return;
		var selectedItem = (PluginRepository.PluginEntry) lvwPlugins.SelectedItems[0].Tag;

		if (PluginHandler.IsNewerOrAlreadyInstalled(selectedItem.Name, selectedItem.Version))
		{
			MessageBox.Show($@"Plugin {selectedItem.Name} is already installed with the same or a greater version!");
			return;
		}

		var updated = false;
		if (PluginHandler.ShouldBeUpdated(selectedItem.Name, selectedItem.Version, out var plugin))
		{
			if (plugin != null)
			{
				File.Delete(plugin.LocalFilePath);
				updated = true;
			}
		}

		Log.Info("Downloading {file}", new Uri(selectedItem.FullUrl));

		var newFile = Path.Combine(Paths.PluginPath, $"{selectedItem.Name}-{selectedItem.Version}.zip");
		WebClient.DownloadFileCompleted += WebClientOnDownloadFileCompleted;
		WebClient.DownloadFileAsync(new Uri(selectedItem.FullUrl), newFile);

		void WebClientOnDownloadFileCompleted(object _, AsyncCompletedEventArgs __)
		{
			InstallFromDisk(newFile, updated, plugin);
			WebClient.DownloadFileCompleted -= WebClientOnDownloadFileCompleted;
		}
	}


	private void btnInstallFromDisk_Click(object sender, EventArgs e)
	{
		openFileDialog1.Filter = @"Zip Files|*.zip";
		openFileDialog1.Multiselect = false;
		if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

		var selectedFile = openFileDialog1.FileName;
		var info = PluginHandler.GetPluginInfo(selectedFile);
		if (info == null)
		{
			MessageBox.Show($@"File {selectedFile} is not a valid Plugin!");
			return;
		}

		if (PluginHandler.IsNewerOrAlreadyInstalled(info))
		{
			MessageBox.Show($@"Plugin {info.PluginName} is already installed with the same or a greater version!");
			return;
		}

		var updated = false;
		if (PluginHandler.ShouldBeUpdated(info, out var plugin))
		{
			if (plugin != null)
			{
				File.Delete(plugin.LocalFilePath);
				updated = true;
			}
		}

		var newFile = Path.Combine(Paths.PluginPath, $"{info.PluginName}-{info.PluginVersion}.zip");

		File.Copy(selectedFile, newFile, true);
		InstallFromDisk(newFile, updated, plugin);
	}

	private void InstallFromDisk(string filePath, bool updated, EldoraPlugin plugin)
	{
		var result = PluginHandler.LoadPlugin(filePath);

		if (result.Code != PluginLoadResult.ErrorCode.None)
		{
			MessageBox.Show($@"Failed loading plugin! Error Code: {(int) result.Code} ({result.Code})");
			File.Delete(filePath);
		}
		else
		{
			if (!updated)
			{
				Eldora.Settings.InstalledPlugins.Add(new Settings.InstalledPlugin
				{
					Name = result.Plugin.PluginInfo.PluginName,
					Version = result.Plugin.PluginInfo.PluginVersion,
				});

				Eldora.SaveSettings();

				MessageBox.Show($@"Installed Plugin {result.Plugin.PluginInfo.PluginName} with version {result.Plugin.PluginInfo.PluginVersion}");
				PluginHandler.RaisePluginsChangedEvent(this, new PluginChangedEventArgs(result.Plugin, PluginChangedEventArgs.PluginChangedEventType.Added));
			}
			else
			{
				var existing = Eldora.Settings.InstalledPlugins.First(p => p.Name == plugin!.PluginInfo.PluginName);
				
				var pluginIndex = Eldora.Settings.InstalledPlugins.FindIndex(p => p.Name == plugin!.PluginInfo.PluginName && p.Version == existing.Version);
				if (pluginIndex >= 0) Eldora.Settings.InstalledPlugins.RemoveAt(pluginIndex);

				var updateMessage = $@"Updated Plugin {result.Plugin.PluginInfo.PluginName} from version {existing.Version} to {result.Plugin.PluginInfo.PluginVersion}";
				existing.Version = result.Plugin.PluginInfo.PluginVersion;

				Eldora.SaveSettings();

				PluginHandler.RaisePluginsChangedEvent(this, new PluginChangedEventArgs(null, PluginChangedEventArgs.PluginChangedEventType.Changed));
				Eldora.RequestRestart(@$"{updateMessage}{Environment.NewLine}Eldora must be restarted to complete the updating. Restart now?");
			}
		}
	}

	private void btnUninstall_Click(object sender, EventArgs e)
	{
		if (lvwPlugins.SelectedItems.Count == 0) return;

		var selectedItem = (EldoraPlugin) lvwPlugins.SelectedItems[0].Tag;
		File.Delete(selectedItem.LocalFilePath);

		var pluginIndex = Eldora.Settings.InstalledPlugins.FindIndex(p => p.Name == selectedItem.PluginInfo.PluginName && p.Version == selectedItem.PluginInfo.PluginVersion);
		if (pluginIndex >= 0) Eldora.Settings.InstalledPlugins.RemoveAt(pluginIndex);

		Eldora.SaveSettings();

		Eldora.RequestRestart(@"Eldora must be restarted to complete the uninstalling. Restart now?");
	}

	private void btnFetchRepos_Click(object sender, EventArgs e)
	{
		FetchRepositories();
	}
}
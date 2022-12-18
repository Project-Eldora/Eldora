using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Eldora.App.Plugins;
using Eldora.WinUtils;
using NLog;
using NLog.Fluent;

namespace Eldora.App.Panels;

public partial class PluginManagerPane : UserControl
{
	private static readonly HttpClient Client = new();
	private static readonly Logger Log = LogManager.GetCurrentClassLogger();

	private readonly ListViewGroup _installedPlugins = new("Installed", HorizontalAlignment.Left)
	{
		Name = "Installed",
	};

	public PluginManagerPane()
	{
		InitializeComponent();
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

		// Add existing plugins to installed group
		foreach (var loadedPlugin in PluginHandler.LoadedPlugins)
		{
			lvwPlugins.Items.Add(CreateItemFromPlugin(loadedPlugin));
		}

		lvwPlugins.Groups.Add(_installedPlugins);
		
		lvwPlugins.AutoSizeColumns();
	}

	private ListViewItem CreateItemFromPlugin(EldoraPlugin plugin)
	{
		var item = new ListViewItem(new[]
		{
			plugin.PluginInfo.PluginName,
			plugin.PluginInfo.PluginVersion,
			plugin.PluginInfo.Author,
			plugin.PluginInfo.Description
		})
		{
			Group = _installedPlugins
		};

		return item;
	}

	private async void FetchRepositories()
	{
		foreach (var pluginRepo in Eldora.Instance.Settings.PluginRepositories)
		{
			try
			{
				Log.Info("Fetching Online Repository {pluginName}", pluginRepo.Name);
				var httpResponseMessage = await Client.GetAsync(pluginRepo.Url);
				var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
				using var reader = new StreamReader(stream);

				var fileData = await reader.ReadToEndAsync();
				var repository = JsonSerializer.Deserialize<PluginRepository>(fileData, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

				Log.Info(repository);

				var group = new ListViewGroup($"Repository: {pluginRepo.Name}", HorizontalAlignment.Left)
				{
					Name = $"Repository: {pluginRepo.Name}"
				};

				foreach (var repositoryEntry in repository.Entries)
				{
					var item = new ListViewItem(new[]
					{
						repositoryEntry.Name,
						repositoryEntry.Version,
						repositoryEntry.Author,
						repositoryEntry.Description
					})
					{
						Group = group,
						//Name = $@"{pluginRepo.Url}.{pluginRepo.Name}"
					};

					lvwPlugins.Items.Add(item);
				}

				lvwPlugins.Groups.Add(group);
			}
			catch (Exception e)
			{
				Log.Error("Could not fetch ({name}|{url}). Cause {exception}", pluginRepo.Name, pluginRepo.Url, e);
				continue;
			}
		}
	}

	// TODO: Extract to own file
	public class PluginRepository
	{
		[JsonPropertyName("entries")] public List<PluginEntry> Entries { get; set; } = new();

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
			[JsonPropertyName("version")] public string Version { get; set; }
			[JsonPropertyName("file_name")] public string FileName { get; set; }
			[JsonPropertyName("author")] public string Author { get; set; }
			[JsonPropertyName("description")] public string Description { get; set; }
			[JsonPropertyName("tags")] public string Tags { get; set; }

			public override string ToString()
			{
				return $"{nameof(Name)}: {Name}, {nameof(Version)}: {Version}, {nameof(FileName)}: {FileName}, {nameof(Author)}: {Author}, {nameof(Description)}: {Description}, {nameof(Tags)}: {Tags}";
			}
		}
	}
}
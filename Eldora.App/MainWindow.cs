using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Eldora.App.Panels;
using Eldora.App.Plugins;
using Eldora.PluginApi.Attributes;
using Eldora.WinUtils;

namespace Eldora.App;

internal sealed partial class MainWindow : Form
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	private readonly ChangelogPane _changelogPane = new();
	private readonly PluginManagerPane _pluginManagerPane = new();

	public MainWindow()
	{
		InitializeComponent();

		AddRootNodes();
		AddInternalPages();
		AddPluginPages();


		PluginHandler.PluginsGotChanged += (_, args) =>
		{
			if (args.Type != PluginChangedEventArgs.PluginChangedEventType.Added) return;
			AddPagesForPlugin(args.Plugin);
		};

		sidebarTreeView.ExpandAll();
	}

	private void AddInternalPages()
	{
		AddInternalPage(_newsNode, "Changelog", _changelogPane);
		AddInternalPage(_settingsNode, "Plugins", _pluginManagerPane);
	}

	private void AddInternalPage(TreeNode node, string name, Control control)
	{
		var pageNode = new TreeNode
		{
			Name = name,
			Text = name,
		};
		node.Nodes.Add(pageNode);

		MapControlToNode(pageNode.FullPath, control);
	}

	/// <summary>
	/// Adds all internal mapped pages
	/// </summary>
	private void AddPluginPages()
	{
		foreach (var loaded in PluginHandler.LoadedPlugins)
		{
			AddPagesForPlugin(loaded);
		}
	}

	private void AddPagesForPlugin(EldoraPlugin loaded)
	{
		var toAdd = new List<(ToolsPageAttribute, Control control)>();
		foreach (var type in loaded.PluginAssembly.GetExportedTypes())
		{
			if (Attribute.IsDefined(type, typeof(ToolsPageAttribute)))
			{
				var result = Activator.CreateInstance(type);
				
				Control control = (Control) result;
				toAdd.Add((type.GetCustomAttribute<ToolsPageAttribute>(), control));
			}
		}

		toAdd.ForEach(AddPagePath);
	}

	private void AddPagePath((ToolsPageAttribute attribute, Control page) mappedPage)
	{
		var path = mappedPage.attribute.PagePathWithTitle;

		var lastNode = _toolsNode;
		foreach (var t in path)
		{
			var foundNode = lastNode.Nodes.Cast<TreeNode>().FirstOrDefault(node => node.Name == t);

			if (foundNode == default)
			{
				var node = new TreeNode
				{
					Name = t,
					Text = t
				};
				lastNode.Nodes.Add(node);
				lastNode = node;
				continue;
			}

			lastNode = foundNode;
		}

		MapControlToNode(_toolsNode.Name + "/" + string.Join("/", mappedPage.attribute.PagePathWithTitle), mappedPage.page);
	}

	#region RootNodes

	private readonly TreeNode _newsNode = new("News")
	{
		Name = "News",
	};

	private readonly TreeNode _toolsNode = new("Tools")
	{
		Name = "Tools",
	};

	private readonly TreeNode _settingsNode = new("Settings")
	{
		Name = "Settings",
	};

	private readonly TreeNode _helpNode = new("Help")
	{
		Name = "Help",
	};

	#endregion

	/// <summary>
	/// The mapping for sidebar nodes and their coresponding controls
	/// </summary>
	private readonly Dictionary<string, Control> _nodeMapping = new();

	/// <summary>
	/// Adds the default nodes to the treeview
	/// </summary>
	private void AddRootNodes()
	{
		// Adds the root nodes to the panel
		sidebarTreeView.Nodes.AddRange(new[]
		{
			_newsNode,
			_toolsNode,
			_settingsNode,
			_helpNode
		});

		sidebarTreeView.SetDoubleBuffered();

		sidebarTreeView.AfterSelect += (_, _) =>
		{
			var path = sidebarTreeView.SelectedNode.FullPath;

			if (path.Contains("/"))
			{
				Log.Info("Path({path}) containts / ", path);
			}

			if (!_nodeMapping.ContainsKey(path)) return;

			contentPanel.Controls.Clear();
			contentPanel.Controls.Add(_nodeMapping[path]);

			Log.Info("Opening {path}", path);
		};
	}

	private void MapControlToNode(string path, Control control)
	{
		Log.Info("Adding mapping for {map}", path);
		control.Dock = DockStyle.Fill;

		_nodeMapping[path] = control;
	}

	private void toolStripButton1_Click(object sender, EventArgs e)
	{
		//_settingsWindow.ShowDialog(this);
	}
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Eldora.App.InternalPages.PackageCreator;
using Eldora.Extensions;
using Eldora.InputBoxes;
using Svg;

namespace Eldora.App;

internal sealed partial class MainWindow : Form
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	//private readonly PackageManager _extensionManagerPanel = new();
	private readonly PackageCreatorPanel _pkgCreator = new();

	public MainWindow()
	{
		InitializeComponent();

		showHideNavigationBar.Image = Bitmaps.LoadBitmapFromSvg(Properties.Resources.menu);
	}

	private void AddInternalPages()
	{
		//AddInternalPage(_newsNode, "Changelog", _changelogPane);
		AddInternalPage(null, "Package Creator", _pkgCreator);
		//AddInternalPage(sidebarTreeView.GetNodeByFullPath("Extension"), "Extension Creator", _extensionCreatorPanel);
	}

	private void AddInternalPage(TreeNode? node, string name, Control control)
	{
		var pageNode = new TreeNode
		{
			Name = name,
			Text = name,
		};

		if (node == null) sidebarTreeView.Nodes.Add(pageNode);
		else node.Nodes.Add(pageNode);

		MapControlToNodePath(pageNode.FullPath, control);
	}

	/// <summary>
	/// Adds all pages from all plugins
	/// </summary>
	private void AddPluginPages()
	{

		//foreach (var loaded in PluginHandler.LoadedPlugins)
		//{
		//	AddPagesForPlugin(loaded);
		//}

	}

	/// <summary>
	/// Adds all pages from a loaded plugin
	/// </summary>
	/// <param name="loaded"></param>
	//private void AddPagesForPlugin(PluginContainer loaded)
	//{
	//	var toAdd = new List<(ToolsPageAttribute, Control control)>();
	//	foreach (var type in loaded.PluginAssembly.GetExportedTypes())
	//	{
	//		if (!Attribute.IsDefined(type, typeof(ToolsPageAttribute))) continue;

	//		var control = (Control) Activator.CreateInstance(type);
	//		toAdd.Add((type.GetCustomAttribute<ToolsPageAttribute>(), control));
	//	}

	//	toAdd.ForEach(AddPagePath);
	//}

	/// <summary>
	/// Adds a page to the nav bar and maps its control
	/// </summary>
	/// <param name="mappedPage"></param>
	//private void AddPagePath((PackagePageAttribute attribute, Control page) mappedPage)
	//{
	//	var path = mappedPage.attribute.PagePathWithTitle;

	//	var lastNode = _toolsNode;
	//	foreach (var t in path)
	//	{
	//		var foundNode = lastNode.Nodes.Cast<TreeNode>().FirstOrDefault(node => node.Name == t);

	//		if (foundNode == default)
	//		{
	//			var node = new TreeNode
	//			{
	//				Name = t,
	//				Text = t
	//			};
	//			lastNode.Nodes.Add(node);
	//			lastNode = node;
	//			continue;
	//		}

	//		lastNode = foundNode;
	//	}

	//	MapControlToNodePath(_toolsNode.Name + sidebarTreeView.PathSeparator + string.Join(sidebarTreeView.PathSeparator, mappedPage.attribute.PagePathWithTitle), mappedPage.page);
	//}

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
	/// The mapping for sidebar nodes and their corresponding controls
	/// </summary>
	private readonly Dictionary<string, Control> _nodeMapping = new();

	/// <summary>
	/// Adds the default nodes to the tree view
	/// </summary>
	private void AddRootNodes()
	{
		// Adds the root nodes to the panel
		sidebarTreeView.Nodes.AddRange(new[]
		{
			//_newsNode,
			_toolsNode,
			_settingsNode,
			//_helpNode
		});

		sidebarTreeView.SetDoubleBuffered();

		sidebarTreeView.AfterSelect += (_, _) =>
		{
			var path = sidebarTreeView.SelectedNode.FullPath;

			if (path.Contains(sidebarTreeView.PathSeparator))
			{
				Log.Info("Path({path}) containts {seperator} ", path, sidebarTreeView.PathSeparator);
			}

			if (!_nodeMapping.ContainsKey(path)) return;

			containerWrapper.Panel2.Controls.Clear();
			containerWrapper.Panel2.Controls.Add(_nodeMapping[path]);

			Log.Info("Opening {path}", path);
		};
	}

	private void MapControlToNodePath(string path, Control control)
	{
		Log.Info("Adding mapping for {map}", path);
		control.Dock = DockStyle.Fill;

		_nodeMapping[path] = control;
	}

	private void MainWindow_Load(object sender, EventArgs e)
	{
		AddRootNodes();
		AddInternalPages();
		AddPluginPages();

		//PluginHandler.PluginsGotChanged += (_, args) =>
		//{
		//	if (args.Type != PluginChangedEventArgs.PluginChangedEventType.Added) return;
		//	AddPagesForPlugin(args.PluginContainer);
		//};

		sidebarTreeView.ExpandAll();
	}

	private static Bitmap LoadBitmapFromSvg(string resourcePath, int width = 24, int height = 24)
	{
		var assembly = typeof(MainWindow).Assembly;
		var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.Contains(resourcePath));
		// if resourceName is null return empty Bitmap
		if (resourceName == null) return new Bitmap(1, 1);

		using var stream = assembly.GetManifestResourceStream(resourceName);
		// if stream is null return empty Bitmap
		if (stream == null) return new Bitmap(1, 1);

		var svgDocument = SvgDocument.Open<SvgDocument>(stream);
		var bitmap = svgDocument.Draw(width, height);
		return bitmap;
	}

	private void MainWindow_Resize(object sender, EventArgs e)
	{
	}

	private void ShowHideNavigationBar_Click(object sender, EventArgs e)
	{
		containerWrapper.Panel1Collapsed = !containerWrapper.Panel1Collapsed;
	}
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Eldora.App.InternalPages.PackageCreator;
using Eldora.App.InternalPages.PackageManager;
using Eldora.Extensions;
using Eldora.InputBoxes;
using Eldora.Packaging.API.Attributes;
using Svg;

namespace Eldora.App;

internal sealed partial class MainWindow : Form
{
	private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

	//private readonly PackageManager _extensionManagerPanel = new();
	private readonly PackageCreatorPanel _pkgCreator = new();
	private readonly PackageManagerPanel _pkgManager = new();	

	private const string TITLE_PREFIX = "Eldora";

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

	private readonly List<DockableComponent> _components = new();

	private readonly ContextMenuStrip _navbarContextMenu = new();
	private readonly ToolStripMenuItem _dockUndockMenuItem = new("Undock");

	public MainWindow()
	{
		InitializeComponent();

		showHideNavigationBar.Image = Bitmaps.LoadBitmapFromSvg(Properties.Resources.menu);

		_dockUndockMenuItem.Click += DockUndockMenuItem_Click;
		_navbarContextMenu.Items.Add(_dockUndockMenuItem);
		_navbarContextMenu.Opening += NavbarContextMenu_Opening;
		sidebarTreeView.ContextMenuStrip = _navbarContextMenu;

		EldoraApp.PluginsChanged += EldoraApp_PluginsChanged;
	}

	private void EldoraApp_PluginsChanged(object? sender, EventArgs e)
	{
		_toolsNode.Nodes.Clear();

		AddPluginPages();
	}

	private void DockUndockMenuItem_Click(object? sender, EventArgs e)
	{
		if (sidebarTreeView.SelectedNode == null) return;

		var path = sidebarTreeView.SelectedNode.FullPath;

		var component = _components.FirstOrDefault(p => p.NavbarPath == path);
		if (component == default) { return; }

		component.Undock();
		sidebarTreeView.SelectedNode = sidebarTreeView.SelectedNode.NextVisibleNode;
		Text = $"{TITLE_PREFIX}";
	}

	private void NavbarContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
	{
		if (sidebarTreeView.SelectedNode == null)
		{
			_dockUndockMenuItem.Enabled = false;
			return;
		}

		_dockUndockMenuItem.Enabled = true;

		var path = sidebarTreeView.SelectedNode.FullPath;

		var component = _components.FirstOrDefault(p => p.NavbarPath == path);
		if (component == default) return;
		if (component.IsUndocked()) return;
	}

	private void AddInternalPages()
	{
		AddInternalPage(_settingsNode, "Package Creator", _pkgCreator);
		AddInternalPage(_settingsNode, "Package Manager", _pkgManager);
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

		MapControlToNodePath(pageNode.FullPath, name, control);
	}

	/// <summary>
	/// Adds a page to the nav bar and maps its control
	/// </summary>
	/// <param title="mappedPage"></param>
	private void AddPluginPage(PackagePageAttribute attribute, Control page)
	{
		var path = attribute.PagePathWithTitle;

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

		MapControlToNodePath(_toolsNode.Name + sidebarTreeView.PathSeparator + string.Join(sidebarTreeView.PathSeparator, attribute.PagePathWithTitle), attribute.PageTitle, page);
	}

	private void AddPluginPages()
	{
		foreach (var pkg in EldoraApp.LoadedPackages)
		{
			var assembly = pkg.RootAssembly;
			foreach (var type in assembly.GetTypes())
			{
				if (!Attribute.IsDefined(type, typeof(PackagePageAttribute))) continue;
				if (Attribute.GetCustomAttribute(type, typeof(PackagePageAttribute)) is not PackagePageAttribute attribute) continue;
				if (Activator.CreateInstance(type) is not Control control) continue;

				AddPluginPage(attribute, control);
			}
		}
	}

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

		sidebarTreeView.NodeMouseClick += (_, args) =>
		{
			if (args.Node == null) return;
			if (args.Button == MouseButtons.Right) return;

			var path = args.Node.FullPath;

			var component = _components.FirstOrDefault(p => p.NavbarPath == path);
			if (component == default) return;
			if (component.IsUndocked()) return;

			containerWrapper.Panel2.Controls.Clear();
			containerWrapper.Panel2.Controls.Add(component.Control);

			Text = $"{TITLE_PREFIX} - {component.Title}";

			Log.Info("Opening {path}", path);
		};
	}

	private void MapControlToNodePath(string path, string title, Control control)
	{
		if (_components.Any(c => c.NavbarPath == path)) return;
		Log.Info("Adding mapping for {map}", path);

		control.Dock = DockStyle.Fill;

		var comp = new DockableComponent(path, title, control);
		_components.Add(comp);
	}

	private void MainWindow_Load(object sender, EventArgs e)
	{
		AddRootNodes();
		AddInternalPages();
		AddPluginPages();

		sidebarTreeView.ExpandAll();
	}

	private void MainWindow_Resize(object sender, EventArgs e)
	{
	}

	private void ShowHideNavigationBar_Click(object sender, EventArgs e)
	{
		containerWrapper.Panel1Collapsed = !containerWrapper.Panel1Collapsed;
	}

	private class DockableComponent
	{
		private Form? _componentForm;

		public Control Control { get; }
		public string NavbarPath { get; }

		public string Title { get; set; }

		public DockableComponent(string navbarPath, string title, Control control)
		{
			NavbarPath = navbarPath;
			Title = $"{TITLE_PREFIX} - {title}";
			Control = control;
		}

		public void Undock()
		{
			CreateOrShow();
			_componentForm!.Controls.Add(Control);
		}

		private void CreateOrShow()
		{
			// if the form is not closed, show it
			if (_componentForm == null)
			{
				_componentForm = new Form
				{
					Text = Title
				};

				_componentForm.FormClosing += (_, _) =>
				{
					_componentForm.Controls.Clear();
				}; ;

				// attach the handler
				_componentForm.FormClosed += ChildFormClosed;
			}

			// show it
			_componentForm.Show();
		}

		// when the form closes, detach the handler and clear the field
		private void ChildFormClosed(object? sender, FormClosedEventArgs args)
		{
			// detach the handler
			_componentForm!.FormClosed -= ChildFormClosed;

			// let GC collect it (and this way we can tell if it's closed)
			_componentForm = null;
		}

		public bool IsUndocked()
		{
			var fc = Application.OpenForms;
			foreach (Form frm in fc)
			{
				if (frm.Text == Title)
				{
					return true;
				}
			}
			return false;
		}
	}
}
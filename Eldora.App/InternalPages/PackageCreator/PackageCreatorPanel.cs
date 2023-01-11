using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eldora.Packaging;

namespace Eldora.App.InternalPages.PackageCreator;
public partial class PackageCreatorPanel : UserControl
{
	private PackageCreatorSplashPanel _splashPanel;
	private PackageCreatorEditorPanel _editorPanel;

	private PackageProject? _project;

	public PackageCreatorPanel()
	{
		InitializeComponent();
		_splashPanel = new PackageCreatorSplashPanel
		{
			Tag = "splash"
		};
		_splashPanel.CreateButtonPressed += SplashPanel_CreateButtonPressed;
		_splashPanel.OpenButtonPressed += SplashPanel_OpenButtonPressed;

		_editorPanel = new PackageCreatorEditorPanel
		{
			Tag = "editor"
		};
		_editorPanel.ClosingRequested += EditorPanel_ClosingRequested;

		esp.AppendControl(_splashPanel);
		esp.AppendControl(_editorPanel);
	}

	private void EditorPanel_ClosingRequested(object? sender, EventArgs e)
	{
		esp.ShowControl("splash");
	}

	private void SplashPanel_OpenButtonPressed(object? sender, string e)
	{
		var path = Path.GetDirectoryName(e);
		if (path == null) return;

		_project = PackageProject.Open(path);
		if (_project == null) return;

		_editorPanel.SetProject(_project);
		esp.ShowControl("editor");
	}

	private void SplashPanel_CreateButtonPressed(object? sender, string e)
	{
		_project = PackageProject.Create(InternalPaths.PackageProjectsPath, e);
		_editorPanel.SetProject(_project);
		esp.ShowControl("editor");
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eldora.App.Packaging;
using Eldora.Packaging;

namespace Eldora.App.InternalPages.PackageCreator;

public partial class PackageCreatorEditorPanel : UserControl
{
	private PackageProject? _editingProject;

	public PackageCreatorEditorPanel()
	{
		InitializeComponent();
	}

	public void SetProject(PackageProject project)
	{
		_editingProject = project;

		packageCreatorMetaEditorPanel.SetProject(project);
	}
}

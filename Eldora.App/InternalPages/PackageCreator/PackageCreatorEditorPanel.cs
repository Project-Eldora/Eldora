using Eldora.Extensions;
using Eldora.Packaging;

namespace Eldora.App.InternalPages.PackageCreator;

public partial class PackageCreatorEditorPanel : UserControl
{
	private PackageProject? _editingProject;

	public event EventHandler? ClosingRequested;

	public PackageCreatorEditorPanel()
	{
		InitializeComponent();
	}

	public void SetProject(PackageProject project)
	{
		_editingProject = project;

		packageCreatorMetaEditorPanel.SetProject(project);
		packageCreatorContentPanel1.SetProject(project);
	}

	private void BundleToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (_editingProject == null) return;
		_editingProject.Bundle(out var bundleName);
		MessageBoxes.RequestYesNoConfirmation("Finished bundling. Open output directory?", "Info", MessageBoxIcon.Question, () =>
		{
			ExplorerExtensions.OpenExplorerAndSelectItem(_editingProject.GetOuputFolder(), bundleName);
		});
	}

	private void OpenOutputFolderToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (_editingProject == null) return;
		ExplorerExtensions.OpenExplorer(_editingProject.GetOuputFolder());
	}

	private void ClosePackageButton_Click(object sender, EventArgs e)
	{
		MessageBoxes.RequestYesNoConfirmation("Do you really want to close the package?", "Info", MessageBoxIcon.Question, () =>
		{
			ClosingRequested?.Invoke(this, EventArgs.Empty);
		});
	}
}

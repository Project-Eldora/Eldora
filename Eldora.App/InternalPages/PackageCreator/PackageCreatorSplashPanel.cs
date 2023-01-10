using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eldora.InputBoxes;

namespace Eldora.App.InternalPages.PackageCreator;

public partial class PackageCreatorSplashPanel : UserControl
{
	public event EventHandler<string> CreateButtonPressed;
	public event EventHandler<string> OpenButtonPressed;

	private readonly OpenFileDialog _openFileDialog;

	public PackageCreatorSplashPanel()
	{
		InitializeComponent();

		_openFileDialog = new OpenFileDialog
		{
			Filter = "Eldora Project File|project.eldprj",
			Multiselect = false,
			InitialDirectory = InternalPaths.PackageProjectsPath
		};
	}

	private void BtnCreateNew_Click(object sender, EventArgs e)
	{
		if (StringInputDialog.Show("Project Name", "Enter a name for a new Package Project:", out var name, validateInput: ValidateInput, validationText: "Text must not be empty") == DialogResult.OK)
		{
			CreateButtonPressed?.Invoke(this, name);
		}
	}

	private bool ValidateInput(string input)
	{
		return !string.IsNullOrEmpty(input);
	}

	private void BtnOpenExisting_Click(object sender, EventArgs e)
	{
		if (_openFileDialog.ShowDialog(this) != DialogResult.OK) { return; }
		OpenButtonPressed?.Invoke(this, _openFileDialog.FileName);
	}
}

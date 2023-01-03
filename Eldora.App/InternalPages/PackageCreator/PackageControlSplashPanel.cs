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

public partial class PackageControlSplashPanel : UserControl
{
	public event EventHandler<string> CreateButtonPressed;
	public event EventHandler<string> OpenButtonPressed;

	public PackageControlSplashPanel()
	{
		InitializeComponent();
	}

	private void btnCreateNew_Click(object sender, EventArgs e)
	{
		if (StringInputDialog.Show("Project Name", "Enter a name for a new Package Project:", out var name, ValidateInput, "Text must not be empty") == DialogResult.OK)
		{
			CreateButtonPressed?.Invoke(this, name);
		}
	}

	private bool ValidateInput(string input)
	{
		return !string.IsNullOrEmpty(input);
	}
}

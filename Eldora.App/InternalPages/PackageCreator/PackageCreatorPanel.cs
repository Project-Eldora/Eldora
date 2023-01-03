using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eldora.App.InternalPages.PackageCreator;
public partial class PackageCreatorPanel : UserControl
{
	private PackageControlSplashPanel _splashPanel;

	public PackageCreatorPanel()
	{
		InitializeComponent();
		_splashPanel = new();
		_splashPanel.CreateButtonPressed += SplashPanel_CreateButtonPressed;

		esp.AppendControl(_splashPanel);
	}

	private void SplashPanel_CreateButtonPressed(object? sender, string e)
	{
		MessageBox.Show("Creating new Package with the name: \"" + e + "\"");
		esp.ShowNextControl();
	}
}

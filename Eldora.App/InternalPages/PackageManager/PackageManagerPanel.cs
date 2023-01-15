using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eldora.App.Packaging;
using Eldora.Packaging;

namespace Eldora.App.InternalPages.PackageManager;

// TODO: Add changelistener for installed packages

public partial class PackageManagerPanel : UserControl
{
	public PackageManagerPanel()
	{
		InitializeComponent();
	}

	private void ReloadPackages()
	{
		flowLayoutPanel1.Controls.Clear();

		tabPage1.Text = $"Installed ({EldoraApp.LoadedPackages.Count})";

		foreach (var bundled in EldoraApp.LoadedPackages)
		{
			if (bundled.PackageMetadata == null) continue;
			AddPackage(bundled);
		}
	}


	private void PackageManagerPanel_Load(object sender, EventArgs e)
	{
		openFileDialog1.Filter = $"Eldora Package|*.{PackageProject.PackageExtension}";
		openFileDialog1.InitialDirectory = InternalPaths.PackageProjectsPath;

		ReloadPackages();
	}
	private void AddPackage(BundledPackage pkg)
	{
		var ctrl = new PackageManagerPackagePanel(pkg.PackageMetadata!);
		ctrl.EnableUninstallButton();
		ctrl.UninstallClicked += (s, e) =>
		{
			EldoraApp.UninstallPackage(pkg);

			ReloadPackages();
		};

		flowLayoutPanel1.Controls.Add(ctrl);
	}

	private void InstallFromDiskToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
		var file = openFileDialog1.FileName;

		EldoraApp.InstallPackage(file);
		ReloadPackages();
	}
}

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

namespace Eldora.App.InternalPages.PackageManager;

public partial class PackageManagerPackagePanel : UserControl
{
	public event EventHandler? UninstallClicked;

	private PackageMetadataModel? _model;

	private PackageMetadataModel? Model
	{
		set
		{
			_model = value;

			if (value == null) return;

			label1.Text = $"Title: {value.Title}";
			label2.Text = $"Id: {value.Identifier}";
			label3.Text = $"Description: {value.Description}";
			label4.Text = $"Authors: {string.Join(", ", value.Authors)}";
		}
	}

	public PackageManagerPackagePanel()
	{
		InitializeComponent();

		btnUninstall.Enabled = false;
		btnUpdate.Enabled = false;
	}

	public PackageManagerPackagePanel(PackageMetadataModel model)
	{
		InitializeComponent();

		Model = model;
		btnUninstall.Enabled = false;
		btnUpdate.Enabled = false;
	}

	public void EnableUpdateButton()
	{
		btnUpdate.Enabled = true;
	}

	public void EnableUninstallButton()
	{
		btnUninstall.Enabled = true;
		btnUninstall.Click += BtnUninstall_Click;
	}

	private void BtnUninstall_Click(object? sender, EventArgs e)
	{
		UninstallClicked?.Invoke(this, EventArgs.Empty);
	}
}

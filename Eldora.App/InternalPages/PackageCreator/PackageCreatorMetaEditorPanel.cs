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
public partial class PackageCreatorMetaEditorPanel : UserControl
{
	private PackageProject? _editingProject;
	private bool _editingEnabled = false;

	private event EventHandler? EditingStatusChanged;

	private bool EditingEnabled
	{
		get => _editingEnabled;
		set
		{
			_editingEnabled = value;
			EditingStatusChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	public PackageCreatorMetaEditorPanel()
	{
		InitializeComponent();
	}

	public void SetProject(PackageProject project)
	{
		_editingProject = project;

		InitializeMetadataEditor();
	}

	private void InitializeMetadataEditor()
	{
		tlpMetadata.Controls.Clear();
		tlpMetadata.RowStyles.Clear();

		var properties = typeof(PackageMetadataModel).GetProperties(BindingFlags.Instance | BindingFlags.Public);
		foreach (var property in properties)
		{
			AddProperty(property);
		}
	}

	private void AddProperty(PropertyInfo property)
	{
		if (property == null) return;

		var value = property.GetValue(_editingProject!.ProjectMetadata.PackageMetadata);
		if (value == null) return;

		var title = new Label
		{
			Font = new Font(Font, FontStyle.Bold),
			AutoSize = true,
			TextAlign = ContentAlignment.MiddleLeft,
			Text = $"{property.Name}:",
		};

		var valueLabel = new Label
		{
			AutoSize = true
		};

		Control valueControl = new Panel();

		switch (property.Name)
		{
			case nameof(PackageMetadataModel.Identifier):
			case nameof(PackageMetadataModel.Title):
			case nameof(PackageMetadataModel.License):
			case nameof(PackageMetadataModel.LicenseUrl):
			case nameof(PackageMetadataModel.Icon):
			case nameof(PackageMetadataModel.Description):
			case nameof(PackageMetadataModel.Copyright):
			case nameof(PackageMetadataModel.ProjectReference):
				{
					valueLabel.DataBindings.Add(nameof(Label.Text), _editingProject!.ProjectMetadata.PackageMetadata, property.Name);

					valueControl = new TextBox
					{
						Dock = DockStyle.Fill
					};
					valueControl.DataBindings.Add(nameof(TextBox.Text), _editingProject!.ProjectMetadata.PackageMetadata, property.Name);
				}
				break;

			case nameof(PackageMetadataModel.Version):
				{
					valueLabel.DataBindings.Add(nameof(Label.Text), _editingProject!.ProjectMetadata.PackageMetadata, property.Name);

					valueControl = new TextBox
					{
						Dock = DockStyle.Fill
					};

					var controlBinding = valueControl.DataBindings.Add(nameof(TextBox.Text), _editingProject!.ProjectMetadata.PackageMetadata, property.Name);
					controlBinding.Parse += (s, e) =>
					{
						if (s is not Binding b) return;

						var txb = (b.Control as TextBox);
						if (txb == null) return;

						var result = Version.TryParse(txb.Text, out var version);
						if (!result) return;

						e.Value = version;
					};
				}
				break;

			case nameof(PackageMetadataModel.Authors):
			case nameof(PackageMetadataModel.Tags):
				{
					var valueBinding = valueLabel.DataBindings.Add(nameof(Label.Text), _editingProject!.ProjectMetadata.PackageMetadata, property.Name);

					valueBinding.Format += (s, e) =>
					{
						if (s is not Binding b) return;
						if (e.Value is not List<string> list) return;

						var sb = "";
						list.ForEach(item => sb += item + ", ");
						e.Value = sb[..^2];
					};

					valueControl = new TextBox
					{
						Dock = DockStyle.Fill
					};

					var controlBinding = valueControl.DataBindings.Add(nameof(TextBox.Text), _editingProject!.ProjectMetadata.PackageMetadata, property.Name, true);
					controlBinding.Parse += (s, e) =>
					{
						if (s is not Binding b) return;

						var txb = (b.Control as TextBox);
						if (txb == null) return;

						var result = new List<string>();

						foreach (var item in txb.Text.Split(","))
						{
							result.Add(item.Trim());
						}

						e.Value = result;
					};
					controlBinding.Format += (s, e) =>
					{
						if (s is not Binding b) return;
						if (e.Value is not List<string> list) return;

						var sb = "";
						list.ForEach(item => sb += item + ", ");
						e.Value = sb[..^2];
					};
				}
				break;

			case nameof(PackageMetadataModel.Repository):
				{
					var repo = (PackageMetadataRepositoryModel)value;

					valueLabel.Text = $"[{repo.Type}] " + repo.Url;

					valueControl = new TextBox
					{
						Dock = DockStyle.Fill
					};
				}
				break;

			case nameof(PackageMetadataModel.Dependencies):
				{
					var list = (List<PackageMetadataDependencyModel>)value;

				}
				break;

			default: return;
		}

		valueControl.Visible = false;
		valueLabel.Visible = true;

		EditingStatusChanged += (_, _) =>
		{
			valueControl.Visible = EditingEnabled;
			valueLabel.Visible = !EditingEnabled;
		};

		tlpMetadata.RowStyles.Add(new(SizeType.AutoSize));
		tlpMetadata.Controls.Add(title, 0, tlpMetadata.RowCount);
		tlpMetadata.Controls.Add(valueLabel, 1, tlpMetadata.RowCount);
		tlpMetadata.Controls.Add(valueControl, 1, tlpMetadata.RowCount);

		tlpMetadata.RowCount++;
	}

	private void TsbEditSave_Click(object sender, EventArgs e)
	{
		EditingEnabled = !EditingEnabled;
	}
}

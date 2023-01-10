using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accessibility;
using Eldora.InputBoxes;
using Eldora.Packaging;
using Eldora.Extensions;

namespace Eldora.App.InternalPages.PackageCreator;

public partial class PackageCreatorContentPanel : UserControl
{
	private PackageProject? _editingProject;

	private readonly ContextMenuStrip _contextMenuStrip = new();

	private readonly ListViewGroup _libraryGroup = new("Library files", HorizontalAlignment.Left)
	{
		Tag = FileType.Library
	};
	private readonly ListViewGroup _contentGroup = new("Content files", HorizontalAlignment.Left)
	{
		Tag = FileType.Content
	};

	private readonly ToolStripMenuItem _tsmiRenameItem = new("Rename")
	{
		Enabled = false,
	};

	private readonly ToolStripMenuItem _tsmiDeleteItem = new("Remove")
	{
		Enabled = false,
	};

	private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();

	public PackageCreatorContentPanel()
	{
		InitializeComponent();

		listView1.ResizeAllColumns();

		_openFileDialog.Multiselect = true;
	}

	public void SetProject(PackageProject project)
	{
		if (_editingProject != null) _editingProject.ProjectFilesChanged -= ProjectFilesChanged;

		_editingProject = project;
		_editingProject.ProjectFilesChanged += ProjectFilesChanged;

		SetListContent();
	}

	private void SetListContent()
	{
		listView1.BeginUpdate();
		listView1.Items.Clear();

		_editingProject?.ProjectMetadata.LibraryFiles.ForEach(file => AddFileToList(file, true));
		_editingProject?.ProjectMetadata.ContentFiles.ForEach(file => AddFileToList(file, false));

		listView1.EndUpdate();

		listView1.ResizeAllColumns();
	}

	private void ProjectFilesChanged(object? sender, PackageProjectEventArgs e)
	{
		SetListContent();
	}

	private void AddFileToList(PackageProjectFileModel fileModel, bool isLibraryFile)
	{
		var item = new ListViewItem(fileModel.FileName)
		{
			Group = isLibraryFile ? _libraryGroup : _contentGroup,
			Tag = fileModel,
		};

		listView1.Items.Add(item);
	}

	private enum FileType
	{
		Library,
		Content
	}

	private void PackageCreatorContentPanel_Load(object sender, EventArgs e)
	{
		ToolStripMenuItem addItem = new("Add");
		ToolStripMenuItem addLibFile = new("Library file...")
		{
			Tag = FileType.Library
		};
		ToolStripMenuItem addConFile = new("Content file...")
		{
			Tag = FileType.Content
		};

		addLibFile.Click += AddFile_Click;
		addConFile.Click += AddFile_Click;

		addItem.DropDownItems.Add(addLibFile);
		addItem.DropDownItems.Add(addConFile);


		ToolStripMenuItem createItem = new("Create new");
		ToolStripMenuItem createLibFile = new("Library file...")
		{
			Tag = FileType.Library
		};
		ToolStripMenuItem createConFile = new("Resource file...")
		{
			Tag = FileType.Content
		};

		createLibFile.Click += CreateFile_Click;
		createConFile.Click += CreateFile_Click;

		createItem.DropDownItems.Add(createLibFile);
		createItem.DropDownItems.Add(createConFile);

		_tsmiDeleteItem.Click += RemoveItem_Click;
		_tsmiRenameItem.Click += RenameItem_Click;
		_contextMenuStrip.Items.Add(_tsmiRenameItem);
		_contextMenuStrip.Items.Add(_tsmiDeleteItem);
		_contextMenuStrip.Items.AddRange(new[] { addItem, createItem });

		listView1.ContextMenuStrip = _contextMenuStrip;

		listView1.Groups.Add(_libraryGroup);
		listView1.Groups.Add(_contentGroup);
	}

	private void RemoveItem_Click(object? sender, EventArgs e)
	{
		if (listView1.SelectedItems.Count == 0)
		{
			_tsmiDeleteItem.Enabled = false;
			return;
		}

		var toRemove = new List<(PackageProjectFileModel, bool)>();

		for (var i = 0; i < listView1.SelectedItems.Count; i++)
		{
			var selected = listView1.SelectedItems[i]!;
			if (selected.Tag is not PackageProjectFileModel fileModel) return;
			var isLibrary = (FileType)selected.Group.Tag! == FileType.Library;
			toRemove.Add((fileModel, isLibrary));
		}

		MessageBoxes.RequestYesNoConfirmation($"Are you sure you want to remove {toRemove.Count} File{(toRemove.Count > 1 ? "s" : "")}?\nIt won't be deleted from the file system", "Confirm", MessageBoxIcon.Question, () =>
		{
			toRemove.ForEach((a) =>
			{
				_editingProject?.RemoveFile(a.Item1.FileName, a.Item2);
			});
		});
	}

	private void RenameItem_Click(object? sender, EventArgs e)
	{
		if (listView1.SelectedItems.Count == 0)
		{
			_tsmiRenameItem.Enabled = false;
			return;
		}

		var selected = listView1.SelectedItems[0]!;
		if (selected.Tag is not PackageProjectFileModel fileModel) return;
		var isLibrary = (FileType)selected.Group.Tag! == FileType.Library;

		if (StringInputDialog.Show("File name", "Please input a new filename:", out var fileName, fileModel.FileName) == DialogResult.Cancel) return;

		MessageBoxes.RequestYesNoConfirmation($"Are you sure you want to rename {fileModel.FileName} to {fileName}?", "Confirm", MessageBoxIcon.Question, () =>
		{
			_editingProject?.MoveFile(fileModel.FileName, fileName, isLibrary);
		});
	}

	private void CreateFile_Click(object? sender, EventArgs e)
	{
		if (sender is not ToolStripMenuItem item) return;
		if (item.Tag is not FileType targetType) return;

		if (StringInputDialog.Show("File name", "Please input filename:", out var fileName) == DialogResult.Cancel) return;
		MessageBoxes.RequestYesNoConfirmation($"Are you sure you want to create {fileName}?", "Confirm", MessageBoxIcon.Question, () =>
		{
			switch (targetType)
			{
				case FileType.Library:
					_editingProject?.CreateNew(fileName, true);
					break;
				case FileType.Content:
					_editingProject?.CreateNew(fileName, false);
					break;
				default: throw new ArgumentOutOfRangeException(targetType.ToString(), "Invalid target type");
			}
		});
	}

	private void AddFile_Click(object? sender, EventArgs e)
	{
		if (sender is not ToolStripMenuItem item) return;
		if (item.Tag is not FileType targetType) return;

		if (_openFileDialog.ShowDialog() != DialogResult.OK) return;
		var files = _openFileDialog.FileNames;

		MessageBoxes.RequestYesNoConfirmation($"Are you sure you want to add {files.Length} File{(files.Length > 1 ? "s" : "")}", "Confirm", MessageBoxIcon.Question, () =>
		{
			switch (targetType)
			{
				case FileType.Library:
					foreach (var file in files)
					{
						_editingProject?.AddFile(file, true);
					}
					break;
				case FileType.Content:
					foreach (var file in files)
					{
						_editingProject?.AddFile(file, false);
					}
					break;
				default: throw new ArgumentOutOfRangeException(targetType.ToString(), "Invalid target type");
			}
		});
	}

	private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
	{
		_tsmiRenameItem.Enabled = listView1.SelectedItems.Count != 0;
		_tsmiDeleteItem.Enabled = listView1.SelectedItems.Count != 0;
	}

	private void MsbOpenTargetFolder_Click(object sender, EventArgs e)
	{
		if (_editingProject == null) return;
		ExplorerExtensions.OpenExplorer(_editingProject.GetOuputFolder());
	}

	private void MsbBundle_Click(object sender, EventArgs e)
	{
		if(_editingProject == null) return;
		_editingProject.Bundle(out var bundleName);
		MessageBoxes.RequestYesNoConfirmation("Finished bundling. Open output directory?", "Info", MessageBoxIcon.Question, () =>
		{
			ExplorerExtensions.OpenExplorerAndSelectItem(_editingProject.GetOuputFolder(), bundleName);
		});
	}
}

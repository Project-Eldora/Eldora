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
using Eldora.Packaging;
using ExCSS;

namespace Eldora.App.InternalPages.PackageCreator;
public partial class PackageCreatorContentPanel : UserControl
{
	private PackageProject? _editingProject;

	private readonly ContextMenuStrip _contextMenuStrip = new();

	public PackageCreatorContentPanel()
	{
		InitializeComponent();
	}

	public void SetProject(PackageProject project)
	{
		_editingProject = project;
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

		_contextMenuStrip.Items.AddRange(new[] { addItem, createItem });

		listView1.ContextMenuStrip = _contextMenuStrip;
	}

	private void CreateFile_Click(object? sender, EventArgs e)
	{
		if (sender is not ToolStripMenuItem item) return;
		if (item.Tag is not FileType targetType) return;

		if (StringInputDialog.Show("File name", "Please input filename:", out var fileName) == DialogResult.Cancel) return;

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
	}

	private void AddFile_Click(object? sender, EventArgs e)
	{
		if (sender is not ToolStripMenuItem item) return;
		if (item.Tag is not FileType targetType) return;

		switch (targetType)
		{
			case FileType.Library:
				break;
			case FileType.Content:
				break;
			default: throw new ArgumentOutOfRangeException(targetType.ToString(), "Invalid target type");
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Eldora.Utils;
using Eldora.WinUtils;

namespace Eldora.Components;

public sealed partial class DbViewerControl<TType> : UserControl
{
	private readonly BindingList<TType> _items = new();
	public BindingList<TType> Items => _items;

	private string _sortingColumnName = string.Empty;
	private ListSortDirection _sortDirection = ListSortDirection.Ascending;

	public event MouseEventHandler MouseClicked;
	
	public DbViewerControl()
	{
		InitializeComponent();

		dgv.SetDoubleBuffered();
	}

	public void HideColumn(string property)
	{
		if (!dgv.Columns.Contains(property))
		{
			return;
		}

		dgv.Columns[property].Visible = false;
	}

	private void DBViewerControl_Load(object sender, EventArgs e)
	{
		dgv.DataSource = _items;

		InitializeColumns();
	}

	private void InitializeColumns()
	{
		var type = typeof(TType);

		for (var i = 0; i < dgv.ColumnCount; i++)
		{
			var column = dgv.Columns[i];
			column.HeaderText = column.Name.SplitCamelCase();
			column.Frozen = false;
		}
	}

	public void ChangeItem(int index, TType item)
	{
		_items[index] = item;
	}

	public void AddItem(TType item)
	{
		_items.Add(item);
	}

	public void AddRange(IEnumerable<TType> items)
	{
		foreach (var item in items)
		{
			AddItem(item);
		}
	}

	public void RemoveItemAt(int index)
	{
		_items.RemoveAt(index);
	}

	// TODO: Add filter
	public void Filter(string filter)
	{
		if (string.IsNullOrEmpty(filter))
		{
			// Reset filter
			return;
		}
	}

	/// <summary>
	/// Resorts based on the last sorting data
	/// </summary>
	private void Resort()
	{
		Sort(_sortingColumnName, _sortDirection);
	}

	public void Sort(string column, ListSortDirection order = ListSortDirection.Ascending)
	{
		if (dgv.Columns[column] == null) return;

		_sortingColumnName = column;
		_sortDirection = order;

		dgv.Sort(dgv.Columns[column], order);
	}

	private void Dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		Resort();
	}

	private void Dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		Resort();
	}

	public void ClearItems()
	{
		_items.Clear();
	}

	private void dgv_MouseClick(object sender, MouseEventArgs e)
	{
		MouseClicked?.Invoke(sender, e);
	}

	public DataGridView.HitTestInfo HitTest(int argsX, int argsY)
	{
		return dgv.HitTest(argsX, argsY);
	}

	public TType SelectedElement()
	{
		if (_items.Count == 0) return default;
		return _items[dgv.SelectedRows[0].Index];
	}
}
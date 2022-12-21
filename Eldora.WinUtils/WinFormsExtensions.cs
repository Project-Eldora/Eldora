using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Eldora.WinUtils;

public static class WinFormsExtensions
{
	#region DoubleBuffering

	private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
	private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
	private const int TVS_EX_DOUBLEBUFFER = 0x0004;

	/// <summary>
	/// Enables double buffering for any given control
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="type"></param>
	public static void SetDoubleBuffered<T>(this T type) where T : Control
	{
		SendMessage(type.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr) TVS_EX_DOUBLEBUFFER, (IntPtr) TVS_EX_DOUBLEBUFFER);
	}

	#endregion

	[DllImport("user32.dll")]
	private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

	/// <summary>
	/// Gets an allways valid rectangle where right is allways greater or equal to left and bottom allways greater or equal to top.
	/// </summary>
	/// <param name="rect"></param>
	/// <returns></returns>
	public static Rectangle Normalized(this Rectangle rect)
	{
		return Rectangle.FromLTRB(Math.Min(rect.Left, rect.Right), Math.Min(rect.Top, rect.Bottom), Math.Max(rect.Left, rect.Right), Math.Max(rect.Top, rect.Bottom));
	}

	/// <summary>
	/// Gets the Center of any image as a point
	/// </summary>
	/// <param name="image"></param>
	/// <returns></returns>
	public static Point GetCenter(this Image image)
	{
		return new Point(image.Width / 2, image.Height / 2);
	}

	public static void DisableDropdownClosingOnClickRecursivly(this ToolStripDropDownItem item)
	{
		item.DropDown.Closing += InternalHandleDropDownClosing;

		foreach (ToolStripItem itemDropDownItem in item.DropDownItems)
		{
			if (itemDropDownItem is not ToolStripDropDownItem menuItem) continue;
			DisableDropdownClosingOnClickRecursivly(menuItem);
		}
	}

	public static void AutoSizeColumns(this ListView listView, int maxColumnWidth = 50)
	{
		//Prevents flickering
		listView.BeginUpdate();

		//Auto size using header
		listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

		//Grab column size based on header
		var columnSize = listView.Columns.Cast<ColumnHeader>().ToDictionary(colHeader => colHeader.Index, colHeader => colHeader.Width);

		//Auto size using data
		listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

		//Grab comumn size based on data and set max width
		foreach (ColumnHeader colHeader in listView.Columns)
		{
			//Default to 50
			colHeader.Width = Math.Max(columnSize.TryGetValue(colHeader.Index, out var nColWidth) ? nColWidth : maxColumnWidth, colHeader.Width);
		}

		listView.EndUpdate();
	}

	private static void InternalHandleDropDownClosing(object sender, ToolStripDropDownClosingEventArgs e)
	{
		if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
		{
			e.Cancel = true;
		}
	}

	public static Image ReadEmbeddedRessourceImage(this Assembly assembly, string searchPattern)
	{
		return FormsUtils.ReadEmbeddedRessourceImage(assembly, searchPattern);
	}
}
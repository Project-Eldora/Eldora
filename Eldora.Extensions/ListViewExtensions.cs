using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eldora.Extensions;

public static class ListViewExtensions
{

	public static void ResizeAllColumns(this ListView listView)
	{
		listView.BeginUpdate();
		listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		ListView.ColumnHeaderCollection cc = listView.Columns;
		for (int i = 0; i < cc.Count; i++)
		{
			int colWidth = TextRenderer.MeasureText(cc[i].Text, listView.Font).Width + 10;
			if (colWidth > cc[i].Width)
			{
				cc[i].Width = colWidth;
			}
		}
		listView.EndUpdate();
	}

}

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Eldora.WinUtils;

namespace Eldora.Components.Standard;

public class EldoraTreeView : TreeView
{
	private int _markerSpacing = 20;

	[Category("Spacing")]
	[Description("Sets the marking spacer")]
	public int MarkerSpacing
	{
		get => _markerSpacing;
		set
		{
			_markerSpacing = value;
			Invalidate();
		}
	}

	public EldoraTreeView()
	{
		Initialize();
	}

	private void Initialize()
	{
		DrawMode = TreeViewDrawMode.OwnerDrawAll;
		Indent = 10;
	}

	protected override void OnDrawNode(DrawTreeNodeEventArgs e)
	{
		var g = e.Graphics;
		var leftPos = e.Bounds.Left + e.Node.Level * Indent;

		var imageSize = e.Bounds.Height - 4;

		using (var b = new SolidBrush(e.Node.IsSelected ? SystemColors.Control : SystemColors.Window))
		{
			g.FillRectangle(b, new Rectangle(0, e.Bounds.Top, ClientSize.Width, e.Bounds.Height));
		}

		// Draws the expand collapse image if the node has children
		if (e.Node.Nodes.Count > 0)
		{
			g.DrawImage(GetIcon(e.Node.IsExpanded), leftPos + 2, e.Bounds.Top + 1, imageSize, imageSize);
		}

		g.DrawString(e.Node.Text, Font, new SolidBrush(Color.Black), leftPos + _markerSpacing, e.Bounds.Top + 1);
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		var hitTest = HitTest(e.Location);
		if (hitTest.Node == null) return;
		SelectedNode = hitTest.Node;
	}

	protected override void OnMouseDoubleClick(MouseEventArgs e)
	{
		var hitTest = HitTest(e.Location);
		if (hitTest.Node == null) return;
		SelectedNode = hitTest.Node;

		// Test if the clicked location is on the right of the label
		// if so toggle the element
		if (hitTest.Location != TreeViewHitTestLocations.RightOfLabel) return;
		SelectedNode.Toggle();
	}

	/// <summary>
	/// Gets the expand or collapse icon
	/// </summary>
	/// <param name="isExpanded"></param>
	/// <returns></returns>
	private static Image GetIcon(bool isExpanded)
	{
		return typeof(EldoraTreeView).Assembly.ReadEmbeddedRessourceImage($"Eldora.Components.Resources.chevron_{(isExpanded ? "down" : "right")}.png");
	}
}
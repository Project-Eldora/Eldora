using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Eldora.Extensions;

namespace Eldora.UI.Components.Standard;

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

	private Bitmap _expanded;
	private Bitmap _collapsed;

	public EldoraTreeView()
	{
		Initialize();
	}

	private void Initialize()
	{
		BackColor = SystemColors.Control;
		BorderStyle = BorderStyle.None;
		DrawMode = TreeViewDrawMode.OwnerDrawAll;
		Indent = 10;

		_expanded = Bitmaps.LoadBitmapFromSvg(Properties.Resources.expand_more);
		_collapsed = Bitmaps.LoadBitmapFromSvg(Properties.Resources.chevron_right);
	}



	protected override void OnDrawNode(DrawTreeNodeEventArgs e)
	{
		base.OnDrawNode(e);

		if (e.Node == null) return;

		var g = e.Graphics;
		var leftPos = e.Bounds.Left + e.Node.Level * Indent;

		var imageSize = e.Bounds.Height - 4;

		using (var b = new SolidBrush(e.Node.IsSelected ? SystemColors.ControlLight : SystemColors.Control))
		{
			g.FillRectangle(b, new Rectangle(0, e.Bounds.Top, ClientSize.Width, e.Bounds.Height));
		}

		// Draws the expand collapse image if the node has children
		if (e.Node.Nodes.Count > 0)
		{
			var icon = GetIcon(e.Node.IsExpanded);
			if (icon != null) g.DrawImage(icon, leftPos + 2, e.Bounds.Top + 1, imageSize, imageSize);
		}

		g.DrawString(e.Node.Text, Font, new SolidBrush(Color.Black), leftPos + _markerSpacing, e.Bounds.Top + 1);
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		var hitTest = HitTest(e.Location);
		if (hitTest.Node == null) return;
		
		if (hitTest.Location != TreeViewHitTestLocations.Label &&
			hitTest.Location != TreeViewHitTestLocations.RightOfLabel) return;

		SelectedNode = hitTest.Node;
	}

	protected override void OnMouseDoubleClick(MouseEventArgs e)
	{
		var hitTest = HitTest(e.Location);
		if (hitTest.Node == null) return;

		// Test if the clicked location is on the right of the label
		// if so toggle the element
		if (hitTest.Location != TreeViewHitTestLocations.RightOfLabel) return;

		SelectedNode = hitTest.Node;
		SelectedNode.Toggle();
	}

	/// <summary>
	/// Gets the expand or collapse icon
	/// </summary>
	/// <param name="isExpanded"></param>
	/// <returns></returns>
	private Image GetIcon(bool isExpanded)
	{
		//return typeof(EldoraTreeView).Assembly.ReadEmbeddedRessourceImage($"Eldora.UI.Components.Resources.chevron_{(isExpanded ? "down" : "right")}.png");
		if (isExpanded) return _expanded;
		return _collapsed;
	}

	public TreeNode? GetNodeByFullPath(params string[] paths)
	{
		if (paths.Length == 0) return null;
		var path = string.Join(PathSeparator, paths);
		var result = Nodes.Find(path, true);
		if (result.Length == 0) return null;
		return result[0];
	}
}
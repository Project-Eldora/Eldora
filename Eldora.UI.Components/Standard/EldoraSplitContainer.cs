using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Eldora.UI.Components.Standard;

public class EldoraSplitContainer : SplitContainer
{
	private int _maxSize = 0;
	private bool _maxSizeEnabled = false;

	[Description("Sets the max size for panel 1")]
	public int Panel1MaxSize
	{
		get => _maxSize;
		set
		{
			_maxSize = value;
			Invalidate();
		}
	}

	public bool MaxSizeEnabled
	{
		get => _maxSizeEnabled;
		set
		{
			_maxSizeEnabled = value;
			Invalidate();
		}
	}

	public EldoraSplitContainer()
	{
		Initialize();
	}

	private void Initialize()
	{
		BorderStyle = BorderStyle.None;
		CalculateSplitterDistance();
	}

	protected override void OnInvalidated(InvalidateEventArgs e)
	{
		base.OnInvalidated(e);

		// Calculate splitter distance on invalidation
		CalculateSplitterDistance();
	}

	// TODO: Test if panel2 max size is working

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);

		CalculateSplitterDistance();
	}

	private void CalculateSplitterDistance()
	{
		if (!_maxSizeEnabled) { return; }

		Panel2MinSize = Width - Panel1MaxSize - SplitterWidth;
	}
}
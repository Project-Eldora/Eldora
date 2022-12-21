using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Eldora.Components.Standard;

public class EldoraSplitContainer : SplitContainer
{
	private int _maxSize;
	private MaxSizedPanelType _maxSizedPanel = MaxSizedPanelType.None;

	[Category("Panel Size")]
	[Description("Sets the max size")]
	public int PanelMaxSize
	{
		get => _maxSize;
		set
		{
			_maxSize = value;
			Invalidate();
		}
	}

	[Category("Panel Size")]
	[Description("Sets the panel to be used for max size")]
	public MaxSizedPanelType MaxSizedPanel
	{
		get => _maxSizedPanel;
		set
		{
			_maxSizedPanel = value;
			Invalidate();
		}
	}

	public enum MaxSizedPanelType
	{
		None,
		Panel1,
		Panel2
	}

	public EldoraSplitContainer()
	{
		Initialize();
	}

	private void Initialize()
	{
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
		switch (_maxSizedPanel)
		{
			case MaxSizedPanelType.Panel1:
				Panel2MinSize = Width - PanelMaxSize - SplitterWidth;
				break;
			case MaxSizedPanelType.Panel2:
				Panel1MinSize = Width - PanelMaxSize - SplitterWidth;
				break;
			case MaxSizedPanelType.None:
				return;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
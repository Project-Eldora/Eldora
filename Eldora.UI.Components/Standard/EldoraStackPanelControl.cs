using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eldora.UI.Components.Standard;

public sealed class EldoraStackPanelControl : UserControl
{
	[Description("The items of the stack panel"), Category("Data")]
	public List<Control> Items { get; set; } = new();

	private int _stackIndex = 0;

	private Control CurrentControl { get => Items[_stackIndex]; }

	public EldoraStackPanelControl()
	{
		Initialize();
	}

	private void Initialize()
	{
		BorderStyle = BorderStyle.None;
	}


	/// <summary>
	/// Adds a control to the end of the stack
	/// </summary>
	/// <param name="control"></param>
	public void AppendControl(Control control)
	{
		control.Dock = DockStyle.Fill;
		Items.Add(control);

		if (Items.Count == 1)
		{
			UpdateCurrentControl();
		}
	}

	/// <summary>
	/// Adds a control to the front of the stack
	/// </summary>
	/// <param name="control"></param>
	public void PrependControl(Control control)
	{
		control.Dock = DockStyle.Fill;
		Items.Insert(0, control);

		if (Items.Count == 1)
		{
			UpdateCurrentControl();
		}
	}

	/// <summary>
	/// Shows the next control on the stack
	/// </summary>
	public void ShowNextControl()
	{
		if (_stackIndex >= Items.Count - 1) return;
		_stackIndex++;

		UpdateCurrentControl();
	}

	/// <summary>
	/// Shows the previous control on the stack
	/// </summary>
	public void ShowPreviousControl()
	{
		if (_stackIndex == 0) { return; }
		_stackIndex--;

		UpdateCurrentControl();
	}

	private void UpdateCurrentControl()
	{
		Controls.Clear();
		Controls.Add(CurrentControl);

		Invalidate();
	}

	/// <summary>
	/// Shows the control where the tag equals the tagname
	/// </summary>
	/// <param name="tagName"></param>
	public void ShowControl(string tagName)
	{
		var ctrlIndex = Items.Select((control, index) => new { value=control, index })
					.Where(x => x.value.Tag.ToString() == tagName)
					.Select(x => x.index).FirstOrDefault(-1);

		if (ctrlIndex == -1) return;
		_stackIndex = ctrlIndex;

		UpdateCurrentControl();
	}
}

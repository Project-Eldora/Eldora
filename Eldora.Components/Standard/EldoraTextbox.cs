using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Eldora.Components.Standard;

public class EldoraTextbox : TextBox
{
	protected string _placeholderText = "Placeholder";
	protected Color _placeholderColor;
	protected Color _placeholderActiveColor;

	private Panel _placeholderContainer;
	private Font _placeholderFont;
	private SolidBrush _placeholderBrush;

	public EldoraTextbox()
	{
		Initialize();
	}

	private void Initialize()
	{
		_placeholderColor = Color.LightGray;
		_placeholderActiveColor = Color.Gray;
		_placeholderFont = Font;
		_placeholderBrush = new SolidBrush(_placeholderActiveColor);
		_placeholderContainer = null;

		// Draw Placeholder to see in design time
		DrawPlaceholder();

		Enter += OnEnter;
		Leave += OnLeave;
		TextChanged += OnTextChanged;
	}

	private void RemovePlaceholder()
	{
		if (_placeholderContainer == null) return;

		Controls.Remove(_placeholderContainer);
		_placeholderContainer = null;
	}

	/// <summary>
	/// Draws  the placeholder if text length is 0
	/// </summary>
	private void DrawPlaceholder()
	{
		if (_placeholderContainer != null) return;
		if (TextLength > 0) return;

		_placeholderContainer = new Panel();
		_placeholderContainer.Paint += PlaceholderContainerOnPaint;
		_placeholderContainer.Invalidate();
		_placeholderContainer.Click += PlaceholderContainerOnClick;
		Controls.Add(_placeholderContainer);
	}

	private void PlaceholderContainerOnClick(object sender, EventArgs e)
	{
		Focus();
	}

	private void PlaceholderContainerOnPaint(object sender, PaintEventArgs e)
	{
		_placeholderContainer.Location = new Point(2, 0);
		_placeholderContainer.Height = Height;
		_placeholderContainer.Width = Width;
		_placeholderContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right;

		_placeholderBrush = ContainsFocus ? new SolidBrush(_placeholderActiveColor) : new SolidBrush(_placeholderColor);

		var g = e.Graphics;
		g.DrawString(_placeholderText, _placeholderFont, _placeholderBrush, new PointF(-2, 1));
	}

	private void OnTextChanged(object sender, EventArgs e)
	{
		if (TextLength > 0) RemovePlaceholder();
		else DrawPlaceholder();
	}

	private void OnLeave(object sender, EventArgs e)
	{
		if (TextLength > 0) RemovePlaceholder();
		else Invalidate();
	}

	private void OnEnter(object sender, EventArgs e)
	{
		_placeholderBrush = new SolidBrush(_placeholderActiveColor);
		if (TextLength > 0) return;

		RemovePlaceholder();
		DrawPlaceholder();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);

		DrawPlaceholder();
	}

	protected override void OnInvalidated(InvalidateEventArgs e)
	{
		base.OnInvalidated(e);

		_placeholderContainer?.Invalidate();
	}

	[Category("Placeholder Attributes")]
	[Description("Sets the placeholder text")]
	public string Placeholder
	{
		get => _placeholderText;
		set
		{
			_placeholderText = value;
			Invalidate();
		}
	}

	[Category("Placeholder Attributes")]
	[Description("Sets the placeholder color if not focused")]
	public Color PlaceholderColor
	{
		get => _placeholderColor;
		set
		{
			_placeholderColor = value;
			Invalidate();
		}
	}

	[Category("Placeholder Attributes")]
	[Description("Sets the placeholder color if focused")]
	public Color PlaceholderActiveColor
	{
		get => _placeholderActiveColor;
		set
		{
			_placeholderActiveColor = value;
			Invalidate();
		}
	}

	[Category("Placeholder Attributes")]
	[Description("Sets the font of the placeholder")]
	public Font PlaceholderFont
	{
		get => _placeholderFont;
		set
		{
			_placeholderFont = value;
			Invalidate();
		}
	}
}
namespace Eldora.App.ExtensionCreator;

partial class EldoraExtensionCreatorPanel
{
	/// <summary> 
	/// Erforderliche Designervariable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary> 
	/// Verwendete Ressourcen bereinigen.
	/// </summary>
	/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Vom Komponenten-Designer generierter Code

	/// <summary> 
	/// Erforderliche Methode für die Designerunterstützung. 
	/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
	/// </summary>
	private void InitializeComponent()
	{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EldoraExtensionCreatorPanel));
			this.eldoraStackPanelControl1 = new Eldora.UI.Components.Standard.EldoraStackPanelControl();
			this.SuspendLayout();
			// 
			// eldoraStackPanelControl1
			// 
			this.eldoraStackPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.eldoraStackPanelControl1.Location = new System.Drawing.Point(0, 0);
			this.eldoraStackPanelControl1.Name = "eldoraStackPanelControl1";
			this.eldoraStackPanelControl1.Size = new System.Drawing.Size(944, 623);
			this.eldoraStackPanelControl1.TabIndex = 0;
			// 
			// EldoraExtensionCreatorPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.eldoraStackPanelControl1);
			this.Name = "EldoraExtensionCreatorPanel";
			this.Size = new System.Drawing.Size(944, 623);
			this.ResumeLayout(false);

	}

	#endregion

	private UI.Components.Standard.EldoraStackPanelControl eldoraStackPanelControl1;
}

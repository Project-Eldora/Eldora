namespace Eldora.App.InternalPages.PackageCreator;

partial class PackageCreatorPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageCreatorPanel));
			this.esp = new Eldora.UI.Components.Standard.EldoraStackPanelControl();
			this.SuspendLayout();
			// 
			// esp
			// 
			this.esp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.esp.Location = new System.Drawing.Point(0, 0);
			this.esp.Name = "esp";
			this.esp.Size = new System.Drawing.Size(1110, 726);
			this.esp.TabIndex = 0;
			// 
			// PackageCreatorPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.esp);
			this.Name = "PackageCreatorPanel";
			this.Size = new System.Drawing.Size(1110, 726);
			this.ResumeLayout(false);

	}

	#endregion

	private UI.Components.Standard.EldoraStackPanelControl esp;
}

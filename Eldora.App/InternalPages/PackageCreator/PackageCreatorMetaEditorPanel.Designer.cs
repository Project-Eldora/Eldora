namespace Eldora.App.InternalPages.PackageCreator;

partial class PackageCreatorMetaEditorPanel
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
			this.tlpMetadata = new System.Windows.Forms.TableLayoutPanel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsbEditSave = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMetadata
			// 
			this.tlpMetadata.AutoScroll = true;
			this.tlpMetadata.ColumnCount = 2;
			this.tlpMetadata.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpMetadata.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMetadata.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMetadata.Location = new System.Drawing.Point(0, 24);
			this.tlpMetadata.Name = "tlpMetadata";
			this.tlpMetadata.RowCount = 1;
			this.tlpMetadata.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMetadata.Size = new System.Drawing.Size(660, 556);
			this.tlpMetadata.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEditSave});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(660, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsbEditSave
			// 
			this.tsbEditSave.Name = "tsbEditSave";
			this.tsbEditSave.Size = new System.Drawing.Size(68, 20);
			this.tsbEditSave.Text = "Edit/Save";
			this.tsbEditSave.Click += new System.EventHandler(this.TsbEditSave_Click);
			// 
			// PackageCreatorMetaEditorPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tlpMetadata);
			this.Controls.Add(this.menuStrip1);
			this.Name = "PackageCreatorMetaEditorPanel";
			this.Size = new System.Drawing.Size(660, 580);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private TableLayoutPanel tlpMetadata;
	private MenuStrip menuStrip1;
	private ToolStripMenuItem tsbEditSave;
}

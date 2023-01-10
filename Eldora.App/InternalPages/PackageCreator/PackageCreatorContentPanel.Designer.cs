namespace Eldora.App.InternalPages.PackageCreator;

partial class PackageCreatorContentPanel
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.msbBundle = new System.Windows.Forms.ToolStripMenuItem();
			this.msbOpenTargetFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.Location = new System.Drawing.Point(0, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(567, 593);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "File";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msbBundle,
            this.msbOpenTargetFolder});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(567, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// msbBundle
			// 
			this.msbBundle.Name = "msbBundle";
			this.msbBundle.Size = new System.Drawing.Size(56, 20);
			this.msbBundle.Text = "Bundle";
			this.msbBundle.Click += new System.EventHandler(this.MsbBundle_Click);
			// 
			// msbOpenTargetFolder
			// 
			this.msbOpenTargetFolder.Name = "msbOpenTargetFolder";
			this.msbOpenTargetFolder.Size = new System.Drawing.Size(119, 20);
			this.msbOpenTargetFolder.Text = "Open Target Folder";
			this.msbOpenTargetFolder.Click += new System.EventHandler(this.MsbOpenTargetFolder_Click);
			// 
			// PackageCreatorContentPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.menuStrip1);
			this.Name = "PackageCreatorContentPanel";
			this.Size = new System.Drawing.Size(567, 617);
			this.Load += new System.EventHandler(this.PackageCreatorContentPanel_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private ListView listView1;
	private ColumnHeader columnHeader1;
	private MenuStrip menuStrip1;
	private ToolStripMenuItem msbBundle;
	private ToolStripMenuItem msbOpenTargetFolder;
}

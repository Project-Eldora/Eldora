namespace Eldora.App.InternalPages.PackageManager;

partial class PackageManagerPanel
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
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.installFromDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.tabPage1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.flowLayoutPanel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(983, 520);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Installed";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(977, 514);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(991, 548);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.menuStrip1);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(983, 520);
			this.tabPage2.TabIndex = 2;
			this.tabPage2.Text = "Available";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installFromDiskToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(983, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// installFromDiskToolStripMenuItem
			// 
			this.installFromDiskToolStripMenuItem.Name = "installFromDiskToolStripMenuItem";
			this.installFromDiskToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
			this.installFromDiskToolStripMenuItem.Text = "Install from disk";
			this.installFromDiskToolStripMenuItem.Click += new System.EventHandler(this.InstallFromDiskToolStripMenuItem_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// PackageManagerPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.tabControl1);
			this.Name = "PackageManagerPanel";
			this.Size = new System.Drawing.Size(991, 548);
			this.Load += new System.EventHandler(this.PackageManagerPanel_Load);
			this.tabPage1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion
	private TabPage tabPage1;
	private TabControl tabControl1;
	private TabPage tabPage2;
	private FlowLayoutPanel flowLayoutPanel1;
	private MenuStrip menuStrip1;
	private ToolStripMenuItem installFromDiskToolStripMenuItem;
	private OpenFileDialog openFileDialog1;
}

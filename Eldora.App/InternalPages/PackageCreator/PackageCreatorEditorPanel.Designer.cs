namespace Eldora.App.InternalPages.PackageCreator;

partial class PackageCreatorEditorPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageCreatorEditorPanel));
			this.eldoraSplitContainer1 = new Eldora.UI.Components.Standard.EldoraSplitContainer();
			this.packageCreatorMetaEditorPanel = new Eldora.App.InternalPages.PackageCreator.PackageCreatorMetaEditorPanel();
			this.packageCreatorContentPanel1 = new Eldora.App.InternalPages.PackageCreator.PackageCreatorContentPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.eldoraSplitContainer1)).BeginInit();
			this.eldoraSplitContainer1.Panel1.SuspendLayout();
			this.eldoraSplitContainer1.Panel2.SuspendLayout();
			this.eldoraSplitContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// eldoraSplitContainer1
			// 
			this.eldoraSplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.eldoraSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.eldoraSplitContainer1.Location = new System.Drawing.Point(0, 0);
			this.eldoraSplitContainer1.MaxSizeEnabled = false;
			this.eldoraSplitContainer1.Name = "eldoraSplitContainer1";
			// 
			// eldoraSplitContainer1.Panel1
			// 
			this.eldoraSplitContainer1.Panel1.Controls.Add(this.packageCreatorMetaEditorPanel);
			this.eldoraSplitContainer1.Panel1MaxSize = 0;
			this.eldoraSplitContainer1.Panel1MinSize = 400;
			// 
			// eldoraSplitContainer1.Panel2
			// 
			this.eldoraSplitContainer1.Panel2.Controls.Add(this.packageCreatorContentPanel1);
			this.eldoraSplitContainer1.Panel2MinSize = 400;
			this.eldoraSplitContainer1.Size = new System.Drawing.Size(980, 668);
			this.eldoraSplitContainer1.SplitterDistance = 400;
			this.eldoraSplitContainer1.TabIndex = 0;
			// 
			// packageCreatorMetaEditorPanel
			// 
			this.packageCreatorMetaEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.packageCreatorMetaEditorPanel.Location = new System.Drawing.Point(0, 0);
			this.packageCreatorMetaEditorPanel.Name = "packageCreatorMetaEditorPanel";
			this.packageCreatorMetaEditorPanel.Size = new System.Drawing.Size(398, 666);
			this.packageCreatorMetaEditorPanel.TabIndex = 0;
			// 
			// packageCreatorContentPanel1
			// 
			this.packageCreatorContentPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.packageCreatorContentPanel1.Location = new System.Drawing.Point(0, 0);
			this.packageCreatorContentPanel1.Name = "packageCreatorContentPanel1";
			this.packageCreatorContentPanel1.Size = new System.Drawing.Size(574, 666);
			this.packageCreatorContentPanel1.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton3});
			this.toolStrip1.Location = new System.Drawing.Point(0, 668);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(980, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(95, 22);
			this.toolStripButton1.Text = "Bundle package";
			this.toolStripButton1.Click += new System.EventHandler(this.BundleToolStripMenuItem_Click);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(113, 22);
			this.toolStripButton2.Text = "Open output folder";
			this.toolStripButton2.Click += new System.EventHandler(this.OpenOutputFolderToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(87, 22);
			this.toolStripButton3.Text = "Close package";
			this.toolStripButton3.Click += new System.EventHandler(this.ClosePackageButton_Click);
			// 
			// PackageCreatorEditorPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.eldoraSplitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "PackageCreatorEditorPanel";
			this.Size = new System.Drawing.Size(980, 693);
			this.eldoraSplitContainer1.Panel1.ResumeLayout(false);
			this.eldoraSplitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.eldoraSplitContainer1)).EndInit();
			this.eldoraSplitContainer1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private UI.Components.Standard.EldoraSplitContainer eldoraSplitContainer1;
	private PackageCreatorMetaEditorPanel packageCreatorMetaEditorPanel;
	private PackageCreatorContentPanel packageCreatorContentPanel1;
	private ToolStrip toolStrip1;
	private ToolStripButton toolStripButton1;
	private ToolStripButton toolStripButton2;
	private ToolStripSeparator toolStripSeparator1;
	private ToolStripButton toolStripButton3;
}

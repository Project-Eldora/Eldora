namespace Eldora.App
{
	sealed partial class MainWindow
	
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			base.Dispose(disposing);
		}

#region Windows Form Designer generated code

/// <summary>
/// Required method for Designer support - do not modify
/// the contents of this method with the code editor.
/// </summary>
private void InitializeComponent()
{
	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
	this.sidebarTreeView = new System.Windows.Forms.TreeView();
	this.contentPanel = new System.Windows.Forms.Panel();
	this.toolStrip1 = new System.Windows.Forms.ToolStrip();
	this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
	this.toolStrip1.SuspendLayout();
	this.SuspendLayout();
	// 
	// sidebarTreeView
	// 
	this.sidebarTreeView.Dock = System.Windows.Forms.DockStyle.Left;
	this.sidebarTreeView.Location = new System.Drawing.Point(0, 25);
	this.sidebarTreeView.Name = "sidebarTreeView";
	this.sidebarTreeView.PathSeparator = "/";
	this.sidebarTreeView.Size = new System.Drawing.Size(175, 656);
	this.sidebarTreeView.TabIndex = 0;
	// 
	// contentPanel
	// 
	this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
	this.contentPanel.Location = new System.Drawing.Point(175, 25);
	this.contentPanel.Name = "contentPanel";
	this.contentPanel.Size = new System.Drawing.Size(1089, 656);
	this.contentPanel.TabIndex = 1;
	// 
	// toolStrip1
	// 
	this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolStripButton1});
	this.toolStrip1.Location = new System.Drawing.Point(0, 0);
	this.toolStrip1.Name = "toolStrip1";
	this.toolStrip1.Size = new System.Drawing.Size(1264, 25);
	this.toolStrip1.TabIndex = 2;
	this.toolStrip1.Text = "toolStrip1";
	// 
	// toolStripButton1
	// 
	this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
	this.toolStripButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButton1.Image")));
	this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
	this.toolStripButton1.Name = "toolStripButton1";
	this.toolStripButton1.Size = new System.Drawing.Size(53, 22);
	this.toolStripButton1.Text = "Settings";
	this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
	// 
	// MainWindow
	// 
	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	this.ClientSize = new System.Drawing.Size(1264, 681);
	this.Controls.Add(this.contentPanel);
	this.Controls.Add(this.sidebarTreeView);
	this.Controls.Add(this.toolStrip1);
	this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
	this.MinimumSize = new System.Drawing.Size(1280, 720);
	this.Name = "MainWindow";
	this.Text = "Eldora";
	this.toolStrip1.ResumeLayout(false);
	this.toolStrip1.PerformLayout();
	this.ResumeLayout(false);
	this.PerformLayout();
}

private System.Windows.Forms.ToolStripButton toolStripButton1;

private System.Windows.Forms.ToolStrip toolStrip1;

#endregion

		private System.Windows.Forms.TreeView sidebarTreeView;
		private System.Windows.Forms.Panel contentPanel;
	}
}
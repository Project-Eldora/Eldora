using Eldora.Components.Standard;

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
	this.toolStrip1 = new System.Windows.Forms.ToolStrip();
	this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
	this.splitContainer1 = new System.Windows.Forms.SplitContainer();
	this.sidebarTreeView = new Eldora.Components.Standard.EldoraTreeView();
	this.toolStrip1.SuspendLayout();
	((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
	this.splitContainer1.Panel1.SuspendLayout();
	this.splitContainer1.SuspendLayout();
	this.SuspendLayout();
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
	this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
	this.toolStripButton1.Name = "toolStripButton1";
	this.toolStripButton1.Size = new System.Drawing.Size(53, 22);
	this.toolStripButton1.Text = "Settings";
	this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
	// 
	// splitContainer1
	// 
	this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
	this.splitContainer1.Location = new System.Drawing.Point(0, 25);
	this.splitContainer1.Name = "splitContainer1";
	// 
	// splitContainer1.Panel1
	// 
	this.splitContainer1.Panel1.Controls.Add(this.sidebarTreeView);
	this.splitContainer1.Panel1MinSize = 200;
	// 
	// splitContainer1.Panel2
	// 
	this.splitContainer1.Panel2.AutoScroll = true;
	this.splitContainer1.Size = new System.Drawing.Size(1264, 656);
	this.splitContainer1.SplitterDistance = 200;
	this.splitContainer1.TabIndex = 3;
	// 
	// sidebarTreeView
	// 
	this.sidebarTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
	this.sidebarTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
	this.sidebarTreeView.Indent = 10;
	this.sidebarTreeView.Location = new System.Drawing.Point(0, 0);
	this.sidebarTreeView.MarkerSpacing = 20;
	this.sidebarTreeView.Name = "sidebarTreeView";
	this.sidebarTreeView.Size = new System.Drawing.Size(200, 656);
	this.sidebarTreeView.TabIndex = 0;
	// 
	// MainWindow
	// 
	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	this.ClientSize = new System.Drawing.Size(1264, 681);
	this.Controls.Add(this.splitContainer1);
	this.Controls.Add(this.toolStrip1);
	this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
	this.MinimumSize = new System.Drawing.Size(1280, 720);
	this.Name = "MainWindow";
	this.Text = "Eldora";
	this.toolStrip1.ResumeLayout(false);
	this.toolStrip1.PerformLayout();
	this.splitContainer1.Panel1.ResumeLayout(false);
	((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
	this.splitContainer1.ResumeLayout(false);
	this.ResumeLayout(false);
	this.PerformLayout();
}

private Eldora.Components.Standard.EldoraTreeView sidebarTreeView;

private System.Windows.Forms.SplitContainer splitContainer1;

private System.Windows.Forms.ToolStripButton toolStripButton1;

private System.Windows.Forms.ToolStrip toolStrip1;

#endregion
	}
}
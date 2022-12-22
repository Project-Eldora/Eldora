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
	this.splitContainer1 = new Eldora.Components.Standard.EldoraSplitContainer();
	this.sidebarTreeView = new Eldora.Components.Standard.EldoraTreeView();
	this.sidebarMenuStrip = new System.Windows.Forms.MenuStrip();
	this.showHideNavigationBar = new System.Windows.Forms.ToolStripMenuItem();
	((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
	this.splitContainer1.Panel1.SuspendLayout();
	this.splitContainer1.SuspendLayout();
	this.sidebarMenuStrip.SuspendLayout();
	this.SuspendLayout();
	// 
	// splitContainer1
	// 
	this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
	this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
	this.splitContainer1.Location = new System.Drawing.Point(34, 0);
	this.splitContainer1.Margin = new System.Windows.Forms.Padding(10);
	this.splitContainer1.MaxSizedPanel = Eldora.Components.Standard.EldoraSplitContainer.MaxSizedPanelType.Panel1;
	this.splitContainer1.Name = "splitContainer1";
	// 
	// splitContainer1.Panel1
	// 
	this.splitContainer1.Panel1.Controls.Add(this.sidebarTreeView);
	this.splitContainer1.Panel1MinSize = 10;
	// 
	// splitContainer1.Panel2
	// 
	this.splitContainer1.Panel2.AutoScroll = true;
	this.splitContainer1.Panel2MinSize = 776;
	this.splitContainer1.PanelMaxSize = 450;
	this.splitContainer1.Size = new System.Drawing.Size(1230, 681);
	this.splitContainer1.SplitterDistance = 194;
	this.splitContainer1.TabIndex = 3;
	// 
	// sidebarTreeView
	// 
	this.sidebarTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
	this.sidebarTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
	this.sidebarTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
	this.sidebarTreeView.Indent = 10;
	this.sidebarTreeView.Location = new System.Drawing.Point(0, 0);
	this.sidebarTreeView.MarkerSpacing = 20;
	this.sidebarTreeView.Name = "sidebarTreeView";
	this.sidebarTreeView.Size = new System.Drawing.Size(192, 679);
	this.sidebarTreeView.TabIndex = 0;
	// 
	// sidebarMenuStrip
	// 
	this.sidebarMenuStrip.Dock = System.Windows.Forms.DockStyle.Left;
	this.sidebarMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.showHideNavigationBar});
	this.sidebarMenuStrip.Location = new System.Drawing.Point(0, 0);
	this.sidebarMenuStrip.Name = "sidebarMenuStrip";
	this.sidebarMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
	this.sidebarMenuStrip.Size = new System.Drawing.Size(34, 681);
	this.sidebarMenuStrip.TabIndex = 4;
	this.sidebarMenuStrip.Text = "menuStrip1";
	// 
	// showHideNavigationBar
	// 
	this.showHideNavigationBar.AutoToolTip = true;
	this.showHideNavigationBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
	this.showHideNavigationBar.Image = ((System.Drawing.Image) (resources.GetObject("showHideNavigationBar.Image")));
	this.showHideNavigationBar.Name = "showHideNavigationBar";
	this.showHideNavigationBar.ShowShortcutKeys = false;
	this.showHideNavigationBar.Size = new System.Drawing.Size(21, 20);
	this.showHideNavigationBar.ToolTipText = "Show/Hide Navigationbar";
	this.showHideNavigationBar.Click += new System.EventHandler(this.showHideNavigationBar_Click);
	// 
	// MainWindow
	// 
	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	this.ClientSize = new System.Drawing.Size(1264, 681);
	this.Controls.Add(this.splitContainer1);
	this.Controls.Add(this.sidebarMenuStrip);
	this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
	this.MainMenuStrip = this.sidebarMenuStrip;
	this.MinimumSize = new System.Drawing.Size(1280, 720);
	this.Name = "MainWindow";
	this.Text = "Eldora";
	this.Load += new System.EventHandler(this.MainWindow_Load);
	this.Resize += new System.EventHandler(this.MainWindow_Resize);
	this.splitContainer1.Panel1.ResumeLayout(false);
	((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
	this.splitContainer1.ResumeLayout(false);
	this.sidebarMenuStrip.ResumeLayout(false);
	this.sidebarMenuStrip.PerformLayout();
	this.ResumeLayout(false);
	this.PerformLayout();
}

private System.Windows.Forms.ToolStripMenuItem showHideNavigationBar;

private System.Windows.Forms.MenuStrip sidebarMenuStrip;

private Eldora.Components.Standard.EldoraTreeView sidebarTreeView;

private Eldora.Components.Standard.EldoraSplitContainer splitContainer1;

#endregion
	}
}
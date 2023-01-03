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
			this.containerWrapper = new Eldora.UI.Components.Standard.EldoraSplitContainer();
			this.sidebarTreeView = new Eldora.UI.Components.Standard.EldoraTreeView();
			this.sidebarMenuStrip = new System.Windows.Forms.MenuStrip();
			this.showHideNavigationBar = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.containerWrapper)).BeginInit();
			this.containerWrapper.Panel1.SuspendLayout();
			this.containerWrapper.SuspendLayout();
			this.sidebarMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// containerWrapper
			// 
			this.containerWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
			this.containerWrapper.Location = new System.Drawing.Point(34, 0);
			this.containerWrapper.Margin = new System.Windows.Forms.Padding(12);
			this.containerWrapper.MaxSizeEnabled = false;
			this.containerWrapper.Name = "containerWrapper";
			// 
			// containerWrapper.Panel1
			// 
			this.containerWrapper.Panel1.Controls.Add(this.sidebarTreeView);
			this.containerWrapper.Panel1MaxSize = 450;
			this.containerWrapper.Panel1MinSize = 10;
			// 
			// containerWrapper.Panel2
			// 
			this.containerWrapper.Panel2.AutoScroll = true;
			this.containerWrapper.Panel2MinSize = 986;
			this.containerWrapper.Size = new System.Drawing.Size(1441, 786);
			this.containerWrapper.SplitterDistance = 225;
			this.containerWrapper.SplitterWidth = 5;
			this.containerWrapper.TabIndex = 3;
			// 
			// sidebarTreeView
			// 
			this.sidebarTreeView.BackColor = System.Drawing.SystemColors.Control;
			this.sidebarTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.sidebarTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sidebarTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
			this.sidebarTreeView.Indent = 10;
			this.sidebarTreeView.Location = new System.Drawing.Point(0, 0);
			this.sidebarTreeView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.sidebarTreeView.MarkerSpacing = 20;
			this.sidebarTreeView.Name = "sidebarTreeView";
			this.sidebarTreeView.Size = new System.Drawing.Size(225, 786);
			this.sidebarTreeView.TabIndex = 0;
			// 
			// sidebarMenuStrip
			// 
			this.sidebarMenuStrip.Dock = System.Windows.Forms.DockStyle.Left;
			this.sidebarMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHideNavigationBar});
			this.sidebarMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.sidebarMenuStrip.Name = "sidebarMenuStrip";
			this.sidebarMenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.sidebarMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.sidebarMenuStrip.Size = new System.Drawing.Size(34, 786);
			this.sidebarMenuStrip.TabIndex = 4;
			this.sidebarMenuStrip.Text = "menuStrip1";
			// 
			// showHideNavigationBar
			// 
			this.showHideNavigationBar.AutoToolTip = true;
			this.showHideNavigationBar.Font = new System.Drawing.Font("Lucida Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.showHideNavigationBar.Name = "showHideNavigationBar";
			this.showHideNavigationBar.ShowShortcutKeys = false;
			this.showHideNavigationBar.Size = new System.Drawing.Size(19, 75);
			this.showHideNavigationBar.Text = "Navigation";
			this.showHideNavigationBar.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
			this.showHideNavigationBar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
			this.showHideNavigationBar.ToolTipText = "Show/Hide Navigationbar";
			this.showHideNavigationBar.Click += new System.EventHandler(this.ShowHideNavigationBar_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1475, 786);
			this.Controls.Add(this.containerWrapper);
			this.Controls.Add(this.sidebarMenuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.sidebarMenuStrip;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MinimumSize = new System.Drawing.Size(1491, 825);
			this.Name = "MainWindow";
			this.Text = "Eldora";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.Resize += new System.EventHandler(this.MainWindow_Resize);
			this.containerWrapper.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.containerWrapper)).EndInit();
			this.containerWrapper.ResumeLayout(false);
			this.sidebarMenuStrip.ResumeLayout(false);
			this.sidebarMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

}

private System.Windows.Forms.ToolStripMenuItem showHideNavigationBar;

private System.Windows.Forms.MenuStrip sidebarMenuStrip;

private Eldora.UI.Components.Standard.EldoraTreeView sidebarTreeView;

private Eldora.UI.Components.Standard.EldoraSplitContainer containerWrapper;

#endregion
	}
}
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
	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Eldora.App.MainWindow));
	this.splitContainer1 = new Eldora.Components.Standard.EldoraSplitContainer();
	this.sidebarTreeView = new Eldora.Components.Standard.EldoraTreeView();
	((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
	this.splitContainer1.Panel1.SuspendLayout();
	this.splitContainer1.SuspendLayout();
	this.SuspendLayout();
	this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
	this.splitContainer1.Location = new System.Drawing.Point(0, 0);
	this.splitContainer1.MaxSizedPanel = Eldora.Components.Standard.EldoraSplitContainer.MaxSizedPanelType.Panel1;
	this.splitContainer1.Name = "splitContainer1";
	this.splitContainer1.Panel1.Controls.Add(this.sidebarTreeView);
	this.splitContainer1.Panel1MinSize = 200;
	this.splitContainer1.Panel2.AutoScroll = true;
	this.splitContainer1.Panel2MinSize = 810;
	this.splitContainer1.PanelMaxSize = 450;
	this.splitContainer1.Size = new System.Drawing.Size(1264, 681);
	this.splitContainer1.SplitterDistance = 200;
	this.splitContainer1.TabIndex = 3;
	this.sidebarTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
	this.sidebarTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
	this.sidebarTreeView.Indent = 10;
	this.sidebarTreeView.Location = new System.Drawing.Point(0, 0);
	this.sidebarTreeView.MarkerSpacing = 20;
	this.sidebarTreeView.Name = "sidebarTreeView";
	this.sidebarTreeView.Size = new System.Drawing.Size(200, 681);
	this.sidebarTreeView.TabIndex = 0;
	this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	this.ClientSize = new System.Drawing.Size(1264, 681);
	this.Controls.Add(this.splitContainer1);
	this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
	this.MinimumSize = new System.Drawing.Size(1280, 720);
	this.Name = "MainWindow";
	this.Text = "Eldora";
	this.Load += new System.EventHandler(this.MainWindow_Load);
	this.Resize += new System.EventHandler(this.MainWindow_Resize);
	this.splitContainer1.Panel1.ResumeLayout(false);
	((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
	this.splitContainer1.ResumeLayout(false);
	this.ResumeLayout(false);
}

private Eldora.Components.Standard.EldoraTreeView sidebarTreeView;

private Eldora.Components.Standard.EldoraSplitContainer splitContainer1;

#endregion
	}
}
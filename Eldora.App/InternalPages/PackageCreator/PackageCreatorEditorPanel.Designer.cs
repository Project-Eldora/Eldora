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
			this.components = new System.ComponentModel.Container();
			this.eldoraSplitContainer1 = new Eldora.UI.Components.Standard.EldoraSplitContainer();
			this.packageCreatorMetaEditorPanel = new Eldora.App.InternalPages.PackageCreator.PackageCreatorMetaEditorPanel();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.packageCreatorContentPanel1 = new Eldora.App.InternalPages.PackageCreator.PackageCreatorContentPanel();
			((System.ComponentModel.ISupportInitialize)(this.eldoraSplitContainer1)).BeginInit();
			this.eldoraSplitContainer1.Panel1.SuspendLayout();
			this.eldoraSplitContainer1.Panel2.SuspendLayout();
			this.eldoraSplitContainer1.SuspendLayout();
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
			this.eldoraSplitContainer1.Size = new System.Drawing.Size(980, 693);
			this.eldoraSplitContainer1.SplitterDistance = 400;
			this.eldoraSplitContainer1.TabIndex = 0;
			// 
			// packageCreatorMetaEditorPanel
			// 
			this.packageCreatorMetaEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.packageCreatorMetaEditorPanel.Location = new System.Drawing.Point(0, 0);
			this.packageCreatorMetaEditorPanel.Name = "packageCreatorMetaEditorPanel";
			this.packageCreatorMetaEditorPanel.Size = new System.Drawing.Size(398, 691);
			this.packageCreatorMetaEditorPanel.TabIndex = 0;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
			// 
			// packageCreatorContentPanel1
			// 
			this.packageCreatorContentPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.packageCreatorContentPanel1.Location = new System.Drawing.Point(0, 0);
			this.packageCreatorContentPanel1.Name = "packageCreatorContentPanel1";
			this.packageCreatorContentPanel1.Size = new System.Drawing.Size(574, 691);
			this.packageCreatorContentPanel1.TabIndex = 0;
			// 
			// PackageCreatorEditorPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.eldoraSplitContainer1);
			this.Name = "PackageCreatorEditorPanel";
			this.Size = new System.Drawing.Size(980, 693);
			this.eldoraSplitContainer1.Panel1.ResumeLayout(false);
			this.eldoraSplitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.eldoraSplitContainer1)).EndInit();
			this.eldoraSplitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

	}

	#endregion

	private UI.Components.Standard.EldoraSplitContainer eldoraSplitContainer1;
	private ContextMenuStrip contextMenuStrip1;
	private PackageCreatorMetaEditorPanel packageCreatorMetaEditorPanel;
	private PackageCreatorContentPanel packageCreatorContentPanel1;
}

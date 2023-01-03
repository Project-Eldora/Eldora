namespace Eldora.App.InternalPages.PackageCreator;

partial class PackageControlSplashPanel
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnCreateNew = new System.Windows.Forms.Button();
			this.btnOpenExisting = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.88889F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.88889F));
			this.tableLayoutPanel1.Controls.Add(this.btnCreateNew, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnOpenExisting, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(936, 652);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// btnCreateNew
			// 
			this.btnCreateNew.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnCreateNew.Location = new System.Drawing.Point(367, 279);
			this.btnCreateNew.Name = "btnCreateNew";
			this.btnCreateNew.Size = new System.Drawing.Size(201, 44);
			this.btnCreateNew.TabIndex = 0;
			this.btnCreateNew.Text = "Create new Package";
			this.btnCreateNew.UseVisualStyleBackColor = true;
			this.btnCreateNew.Click += new System.EventHandler(this.btnCreateNew_Click);
			// 
			// btnOpenExisting
			// 
			this.btnOpenExisting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnOpenExisting.Location = new System.Drawing.Point(367, 329);
			this.btnOpenExisting.Name = "btnOpenExisting";
			this.btnOpenExisting.Size = new System.Drawing.Size(201, 44);
			this.btnOpenExisting.TabIndex = 1;
			this.btnOpenExisting.Text = "Open existing Package";
			this.btnOpenExisting.UseVisualStyleBackColor = true;
			// 
			// PackageControlSplashPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PackageControlSplashPanel";
			this.Size = new System.Drawing.Size(936, 652);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

	}

	#endregion

	private TableLayoutPanel tableLayoutPanel1;
	private Button btnCreateNew;
	private Button btnOpenExisting;
}

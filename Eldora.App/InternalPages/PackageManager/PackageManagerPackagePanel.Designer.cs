namespace Eldora.App.InternalPages.PackageManager;

partial class PackageManagerPackagePanel
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnUninstall = new System.Windows.Forms.Button();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 21);
			this.label1.TabIndex = 0;
			this.label1.Text = "Title: Example";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(3, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Id: Eldora.Example";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(3, 50);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(188, 21);
			this.label3.TabIndex = 0;
			this.label3.Text = "Description: Lorem Ipsum";
			// 
			// btnUninstall
			// 
			this.btnUninstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUninstall.Location = new System.Drawing.Point(480, 32);
			this.btnUninstall.Name = "btnUninstall";
			this.btnUninstall.Size = new System.Drawing.Size(150, 23);
			this.btnUninstall.TabIndex = 1;
			this.btnUninstall.Text = "Uninstall";
			this.btnUninstall.UseVisualStyleBackColor = true;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUpdate.Location = new System.Drawing.Point(480, 3);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(150, 23);
			this.btnUpdate.TabIndex = 1;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label4.Location = new System.Drawing.Point(3, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(75, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Authors: Joss";
			// 
			// PackageManagerPackagePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.btnUninstall);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Name = "PackageManagerPackagePanel";
			this.Size = new System.Drawing.Size(633, 76);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private Label label1;
	private Label label2;
	private Label label3;
	private Button btnUninstall;
	private Button btnUpdate;
	private Label label4;
}

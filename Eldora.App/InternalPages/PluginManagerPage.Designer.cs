using System.ComponentModel;

namespace Eldora.App.InternalPages;

partial class PluginManagerPane
{
	/// <summary> 
	/// Required designer variable.
	/// </summary>
	private IContainer components = null;

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

	#region Component Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		this.lvwPlugins = new System.Windows.Forms.ListView();
		this.btnInstallFromRepo = new System.Windows.Forms.Button();
		this.btnInstallFromDisk = new System.Windows.Forms.Button();
		this.btnUninstall = new System.Windows.Forms.Button();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.btnFetchRepos = new System.Windows.Forms.Button();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.tableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		// 
		// lvwPlugins
		// 
		this.lvwPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
		this.lvwPlugins.FullRowSelect = true;
		this.lvwPlugins.GridLines = true;
		this.lvwPlugins.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.lvwPlugins.Location = new System.Drawing.Point(0, 0);
		this.lvwPlugins.MultiSelect = false;
		this.lvwPlugins.Name = "lvwPlugins";
		this.lvwPlugins.Size = new System.Drawing.Size(606, 481);
		this.lvwPlugins.TabIndex = 0;
		this.lvwPlugins.UseCompatibleStateImageBehavior = false;
		this.lvwPlugins.View = System.Windows.Forms.View.Details;
		this.lvwPlugins.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwPlugins_ItemSelectionChanged);
		// 
		// btnInstallFromRepo
		// 
		this.btnInstallFromRepo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnInstallFromRepo.Location = new System.Drawing.Point(3, 3);
		this.btnInstallFromRepo.Name = "btnInstallFromRepo";
		this.btnInstallFromRepo.Size = new System.Drawing.Size(196, 22);
		this.btnInstallFromRepo.TabIndex = 1;
		this.btnInstallFromRepo.Text = "Install";
		this.btnInstallFromRepo.UseVisualStyleBackColor = true;
		this.btnInstallFromRepo.Click += new System.EventHandler(this.btnInstallFromRepo_Click);
		// 
		// btnInstallFromDisk
		// 
		this.btnInstallFromDisk.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnInstallFromDisk.Location = new System.Drawing.Point(3, 31);
		this.btnInstallFromDisk.Name = "btnInstallFromDisk";
		this.btnInstallFromDisk.Size = new System.Drawing.Size(196, 22);
		this.btnInstallFromDisk.TabIndex = 1;
		this.btnInstallFromDisk.Text = "Install from Disk";
		this.btnInstallFromDisk.UseVisualStyleBackColor = true;
		this.btnInstallFromDisk.Click += new System.EventHandler(this.btnInstallFromDisk_Click);
		// 
		// btnUninstall
		// 
		this.btnUninstall.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnUninstall.Location = new System.Drawing.Point(407, 3);
		this.btnUninstall.Name = "btnUninstall";
		this.btnUninstall.Size = new System.Drawing.Size(196, 22);
		this.btnUninstall.TabIndex = 1;
		this.btnUninstall.Text = "Uninstall";
		this.btnUninstall.UseVisualStyleBackColor = true;
		this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
		// 
		// tableLayoutPanel1
		// 
		this.tableLayoutPanel1.ColumnCount = 3;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
		this.tableLayoutPanel1.Controls.Add(this.btnInstallFromRepo, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnInstallFromDisk, 0, 1);
		this.tableLayoutPanel1.Controls.Add(this.btnFetchRepos, 1, 0);
		this.tableLayoutPanel1.Controls.Add(this.btnUninstall, 2, 0);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 425);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 2;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(606, 56);
		this.tableLayoutPanel1.TabIndex = 2;
		// 
		// btnFetchRepos
		// 
		this.btnFetchRepos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.btnFetchRepos.Location = new System.Drawing.Point(205, 3);
		this.btnFetchRepos.Name = "btnFetchRepos";
		this.btnFetchRepos.Size = new System.Drawing.Size(196, 22);
		this.btnFetchRepos.TabIndex = 3;
		this.btnFetchRepos.Text = "Fetch Repos";
		this.btnFetchRepos.UseVisualStyleBackColor = true;
		this.btnFetchRepos.Click += new System.EventHandler(this.btnFetchRepos_Click);
		// 
		// openFileDialog1
		// 
		this.openFileDialog1.FileName = "openFileDialog1";
		// 
		// PluginManagerPane
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Controls.Add(this.tableLayoutPanel1);
		this.Controls.Add(this.lvwPlugins);
		this.Name = "PluginManagerPane";
		this.Size = new System.Drawing.Size(606, 481);
		this.tableLayoutPanel1.ResumeLayout(false);
		this.ResumeLayout(false);
	}

	private System.Windows.Forms.Button btnFetchRepos;

	private System.Windows.Forms.OpenFileDialog openFileDialog1;

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

	private System.Windows.Forms.Button btnInstallFromDisk;
	private System.Windows.Forms.Button btnUninstall;

	private System.Windows.Forms.Button btnInstallFromRepo;

	private System.Windows.Forms.ListView lvwPlugins;

	#endregion
}
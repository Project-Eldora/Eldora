namespace Eldora.Components
{
	sealed partial class DbViewerControl<TType>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
	        this.dgv = new System.Windows.Forms.DataGridView();
	        ((System.ComponentModel.ISupportInitialize) (this.dgv)).BeginInit();
	        this.SuspendLayout();
	        // 
	        // dgv
	        // 
	        this.dgv.AllowUserToAddRows = false;
	        this.dgv.AllowUserToDeleteRows = false;
	        this.dgv.AllowUserToOrderColumns = true;
	        this.dgv.AllowUserToResizeRows = false;
	        this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
	        this.dgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
	        this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
	        this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
	        this.dgv.Location = new System.Drawing.Point(0, 0);
	        this.dgv.MultiSelect = false;
	        this.dgv.Name = "dgv";
	        this.dgv.ReadOnly = true;
	        this.dgv.RowHeadersVisible = false;
	        this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
	        this.dgv.Size = new System.Drawing.Size(800, 450);
	        this.dgv.TabIndex = 0;
	        this.dgv.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Dgv_RowsAdded);
	        this.dgv.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Dgv_RowsRemoved);
	        this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
	        // 
	        // DbViewerControl
	        // 
	        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
	        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
	        this.Controls.Add(this.dgv);
	        this.Name = "DbViewerControl";
	        this.Size = new System.Drawing.Size(800, 450);
	        this.Load += new System.EventHandler(this.DBViewerControl_Load);
	        ((System.ComponentModel.ISupportInitialize) (this.dgv)).EndInit();
	        this.ResumeLayout(false);
        }

		#endregion

		private System.Windows.Forms.DataGridView dgv;
	}
}

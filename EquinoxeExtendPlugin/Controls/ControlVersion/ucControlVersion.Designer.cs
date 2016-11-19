namespace EquinoxeExtendPlugin.Controls.ControlVersion
{
    partial class ucControlVersion
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblOldConstantCount = new System.Windows.Forms.Label();
            this.lblNewConstantCount = new System.Windows.Forms.Label();
            this.lblOldControlCount = new System.Windows.Forms.Label();
            this.cmdOldConstantCopy = new System.Windows.Forms.Button();
            this.cmdNewConstantCopy = new System.Windows.Forms.Button();
            this.cmdOldControlCopy = new System.Windows.Forms.Button();
            this.cmdNewControlCopy = new System.Windows.Forms.Button();
            this.dgvNewControl = new System.Windows.Forms.DataGridView();
            this.dgvOldControl = new System.Windows.Forms.DataGridView();
            this.dgvNewConstant = new System.Windows.Forms.DataGridView();
            this.dgvOldConstant = new System.Windows.Forms.DataGridView();
            this.lblNewControlCount = new System.Windows.Forms.Label();
            this.lblNewControl = new System.Windows.Forms.Label();
            this.lblOldControl = new System.Windows.Forms.Label();
            this.lblNewConstant = new System.Windows.Forms.Label();
            this.lblOldConstant = new System.Windows.Forms.Label();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOldControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewConstant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOldConstant)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpMain.Controls.Add(this.lblOldConstantCount, 1, 10);
            this.tlpMain.Controls.Add(this.lblNewConstantCount, 1, 7);
            this.tlpMain.Controls.Add(this.lblOldControlCount, 1, 4);
            this.tlpMain.Controls.Add(this.cmdOldConstantCopy, 1, 11);
            this.tlpMain.Controls.Add(this.cmdNewConstantCopy, 1, 8);
            this.tlpMain.Controls.Add(this.cmdOldControlCopy, 1, 5);
            this.tlpMain.Controls.Add(this.cmdNewControlCopy, 1, 2);
            this.tlpMain.Controls.Add(this.dgvNewControl, 0, 1);
            this.tlpMain.Controls.Add(this.dgvOldControl, 0, 4);
            this.tlpMain.Controls.Add(this.dgvNewConstant, 0, 7);
            this.tlpMain.Controls.Add(this.dgvOldConstant, 0, 10);
            this.tlpMain.Controls.Add(this.lblNewControlCount, 1, 1);
            this.tlpMain.Controls.Add(this.lblNewControl, 0, 0);
            this.tlpMain.Controls.Add(this.lblOldControl, 0, 3);
            this.tlpMain.Controls.Add(this.lblNewConstant, 0, 6);
            this.tlpMain.Controls.Add(this.lblOldConstant, 0, 9);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 12;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpMain.Size = new System.Drawing.Size(675, 553);
            this.tlpMain.TabIndex = 0;
            // 
            // lblOldConstantCount
            // 
            this.lblOldConstantCount.AutoSize = true;
            this.lblOldConstantCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOldConstantCount.Location = new System.Drawing.Point(568, 434);
            this.lblOldConstantCount.Name = "lblOldConstantCount";
            this.lblOldConstantCount.Size = new System.Drawing.Size(104, 20);
            this.lblOldConstantCount.TabIndex = 11;
            this.lblOldConstantCount.Text = "0";
            this.lblOldConstantCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNewConstantCount
            // 
            this.lblNewConstantCount.AutoSize = true;
            this.lblNewConstantCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNewConstantCount.Location = new System.Drawing.Point(568, 296);
            this.lblNewConstantCount.Name = "lblNewConstantCount";
            this.lblNewConstantCount.Size = new System.Drawing.Size(104, 20);
            this.lblNewConstantCount.TabIndex = 10;
            this.lblNewConstantCount.Text = "0";
            this.lblNewConstantCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOldControlCount
            // 
            this.lblOldControlCount.AutoSize = true;
            this.lblOldControlCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOldControlCount.Location = new System.Drawing.Point(568, 158);
            this.lblOldControlCount.Name = "lblOldControlCount";
            this.lblOldControlCount.Size = new System.Drawing.Size(104, 20);
            this.lblOldControlCount.TabIndex = 9;
            this.lblOldControlCount.Text = "0";
            this.lblOldControlCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdOldConstantCopy
            // 
            this.cmdOldConstantCopy.Location = new System.Drawing.Point(568, 457);
            this.cmdOldConstantCopy.Name = "cmdOldConstantCopy";
            this.cmdOldConstantCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdOldConstantCopy.TabIndex = 7;
            this.cmdOldConstantCopy.Text = "Copier";
            this.cmdOldConstantCopy.UseVisualStyleBackColor = true;
            this.cmdOldConstantCopy.Click += new System.EventHandler(this.cmdOldConstantCopy_Click);
            // 
            // cmdNewConstantCopy
            // 
            this.cmdNewConstantCopy.Location = new System.Drawing.Point(568, 319);
            this.cmdNewConstantCopy.Name = "cmdNewConstantCopy";
            this.cmdNewConstantCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdNewConstantCopy.TabIndex = 6;
            this.cmdNewConstantCopy.Text = "Copier";
            this.cmdNewConstantCopy.UseVisualStyleBackColor = true;
            this.cmdNewConstantCopy.Click += new System.EventHandler(this.cmdNewConstantCopy_Click);
            // 
            // cmdOldControlCopy
            // 
            this.cmdOldControlCopy.Location = new System.Drawing.Point(568, 181);
            this.cmdOldControlCopy.Name = "cmdOldControlCopy";
            this.cmdOldControlCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdOldControlCopy.TabIndex = 5;
            this.cmdOldControlCopy.Text = "Copier";
            this.cmdOldControlCopy.UseVisualStyleBackColor = true;
            this.cmdOldControlCopy.Click += new System.EventHandler(this.cmdOldControlCopy_Click);
            // 
            // cmdNewControlCopy
            // 
            this.cmdNewControlCopy.Location = new System.Drawing.Point(568, 43);
            this.cmdNewControlCopy.Name = "cmdNewControlCopy";
            this.cmdNewControlCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdNewControlCopy.TabIndex = 4;
            this.cmdNewControlCopy.Text = "Copier";
            this.cmdNewControlCopy.UseVisualStyleBackColor = true;
            this.cmdNewControlCopy.Click += new System.EventHandler(this.cmdNewControlCopy_Click);
            // 
            // dgvNewControl
            // 
            this.dgvNewControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNewControl.Location = new System.Drawing.Point(3, 23);
            this.dgvNewControl.Name = "dgvNewControl";
            this.tlpMain.SetRowSpan(this.dgvNewControl, 2);
            this.dgvNewControl.Size = new System.Drawing.Size(559, 112);
            this.dgvNewControl.TabIndex = 0;
            // 
            // dgvOldControl
            // 
            this.dgvOldControl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOldControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOldControl.Location = new System.Drawing.Point(3, 161);
            this.dgvOldControl.Name = "dgvOldControl";
            this.tlpMain.SetRowSpan(this.dgvOldControl, 2);
            this.dgvOldControl.Size = new System.Drawing.Size(559, 112);
            this.dgvOldControl.TabIndex = 1;
            // 
            // dgvNewConstant
            // 
            this.dgvNewConstant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNewConstant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNewConstant.Location = new System.Drawing.Point(3, 299);
            this.dgvNewConstant.Name = "dgvNewConstant";
            this.tlpMain.SetRowSpan(this.dgvNewConstant, 2);
            this.dgvNewConstant.Size = new System.Drawing.Size(559, 112);
            this.dgvNewConstant.TabIndex = 3;
            // 
            // dgvOldConstant
            // 
            this.dgvOldConstant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOldConstant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOldConstant.Location = new System.Drawing.Point(3, 437);
            this.dgvOldConstant.Name = "dgvOldConstant";
            this.tlpMain.SetRowSpan(this.dgvOldConstant, 2);
            this.dgvOldConstant.Size = new System.Drawing.Size(559, 113);
            this.dgvOldConstant.TabIndex = 2;
            // 
            // lblNewControlCount
            // 
            this.lblNewControlCount.AutoSize = true;
            this.lblNewControlCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNewControlCount.Location = new System.Drawing.Point(568, 20);
            this.lblNewControlCount.Name = "lblNewControlCount";
            this.lblNewControlCount.Size = new System.Drawing.Size(104, 20);
            this.lblNewControlCount.TabIndex = 8;
            this.lblNewControlCount.Text = "0";
            this.lblNewControlCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNewControl
            // 
            this.lblNewControl.AutoSize = true;
            this.lblNewControl.Location = new System.Drawing.Point(3, 0);
            this.lblNewControl.Name = "lblNewControl";
            this.lblNewControl.Size = new System.Drawing.Size(92, 13);
            this.lblNewControl.TabIndex = 12;
            this.lblNewControl.Text = "Nouveau controle";
            // 
            // lblOldControl
            // 
            this.lblOldControl.AutoSize = true;
            this.lblOldControl.Location = new System.Drawing.Point(3, 138);
            this.lblOldControl.Name = "lblOldControl";
            this.lblOldControl.Size = new System.Drawing.Size(91, 13);
            this.lblOldControl.TabIndex = 13;
            this.lblOldControl.Text = "Controle supprimé";
            // 
            // lblNewConstant
            // 
            this.lblNewConstant.AutoSize = true;
            this.lblNewConstant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNewConstant.Location = new System.Drawing.Point(3, 276);
            this.lblNewConstant.Name = "lblNewConstant";
            this.lblNewConstant.Size = new System.Drawing.Size(559, 20);
            this.lblNewConstant.TabIndex = 14;
            this.lblNewConstant.Text = "Nouvelle constante";
            // 
            // lblOldConstant
            // 
            this.lblOldConstant.AutoSize = true;
            this.lblOldConstant.Location = new System.Drawing.Point(3, 414);
            this.lblOldConstant.Name = "lblOldConstant";
            this.lblOldConstant.Size = new System.Drawing.Size(106, 13);
            this.lblOldConstant.TabIndex = 15;
            this.lblOldConstant.Text = "Constante supprimée";
            // 
            // ucControlVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ucControlVersion";
            this.Size = new System.Drawing.Size(675, 553);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOldControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewConstant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOldConstant)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Button cmdOldConstantCopy;
        private System.Windows.Forms.Button cmdNewConstantCopy;
        private System.Windows.Forms.Button cmdOldControlCopy;
        private System.Windows.Forms.DataGridView dgvNewControl;
        private System.Windows.Forms.DataGridView dgvOldControl;
        private System.Windows.Forms.DataGridView dgvNewConstant;
        private System.Windows.Forms.DataGridView dgvOldConstant;
        private System.Windows.Forms.Button cmdNewControlCopy;
        private System.Windows.Forms.Label lblOldConstantCount;
        private System.Windows.Forms.Label lblNewConstantCount;
        private System.Windows.Forms.Label lblOldControlCount;
        private System.Windows.Forms.Label lblNewControlCount;
        private System.Windows.Forms.Label lblNewControl;
        private System.Windows.Forms.Label lblOldControl;
        private System.Windows.Forms.Label lblNewConstant;
        private System.Windows.Forms.Label lblOldConstant;
    }
}

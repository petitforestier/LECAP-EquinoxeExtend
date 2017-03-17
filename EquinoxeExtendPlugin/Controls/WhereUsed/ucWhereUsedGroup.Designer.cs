namespace EquinoxeExtendPlugin.Controls.WhereUsedTable
{
    partial class ucWhereUsedGroup
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
            this.tlpWhereUsed = new System.Windows.Forms.TableLayoutPanel();
            this.lblGroupTable = new System.Windows.Forms.Label();
            this.trvGroupTable = new System.Windows.Forms.TreeView();
            this.lblProjectTable = new System.Windows.Forms.Label();
            this.trvProjectTable = new System.Windows.Forms.TreeView();
            this.cmdRunWhereUsedGroupTableAnalyse = new System.Windows.Forms.Button();
            this.tlpProjectTable = new System.Windows.Forms.TableLayoutPanel();
            this.cmdRunWhereUsedProjectTableAnalyse = new System.Windows.Forms.Button();
            this.cboPackage = new System.Windows.Forms.ComboBox();
            this.tlpWhereUsed.SuspendLayout();
            this.tlpProjectTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpWhereUsed
            // 
            this.tlpWhereUsed.ColumnCount = 2;
            this.tlpWhereUsed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.78843F));
            this.tlpWhereUsed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.21157F));
            this.tlpWhereUsed.Controls.Add(this.lblGroupTable, 0, 1);
            this.tlpWhereUsed.Controls.Add(this.trvGroupTable, 0, 2);
            this.tlpWhereUsed.Controls.Add(this.lblProjectTable, 1, 1);
            this.tlpWhereUsed.Controls.Add(this.trvProjectTable, 1, 2);
            this.tlpWhereUsed.Controls.Add(this.cmdRunWhereUsedGroupTableAnalyse, 0, 0);
            this.tlpWhereUsed.Controls.Add(this.tlpProjectTable, 1, 0);
            this.tlpWhereUsed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpWhereUsed.Location = new System.Drawing.Point(0, 0);
            this.tlpWhereUsed.Name = "tlpWhereUsed";
            this.tlpWhereUsed.RowCount = 4;
            this.tlpWhereUsed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpWhereUsed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlpWhereUsed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWhereUsed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpWhereUsed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpWhereUsed.Size = new System.Drawing.Size(998, 624);
            this.tlpWhereUsed.TabIndex = 0;
            // 
            // lblGroupTable
            // 
            this.lblGroupTable.AutoSize = true;
            this.lblGroupTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGroupTable.Location = new System.Drawing.Point(3, 30);
            this.lblGroupTable.Name = "lblGroupTable";
            this.lblGroupTable.Size = new System.Drawing.Size(490, 32);
            this.lblGroupTable.TabIndex = 0;
            this.lblGroupTable.Text = "Tables de groupes dans tous les projets";
            this.lblGroupTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trvGroupTable
            // 
            this.trvGroupTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvGroupTable.Location = new System.Drawing.Point(3, 65);
            this.trvGroupTable.Name = "trvGroupTable";
            this.trvGroupTable.Size = new System.Drawing.Size(490, 536);
            this.trvGroupTable.TabIndex = 3;
            // 
            // lblProjectTable
            // 
            this.lblProjectTable.AutoSize = true;
            this.lblProjectTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProjectTable.Location = new System.Drawing.Point(499, 30);
            this.lblProjectTable.Name = "lblProjectTable";
            this.lblProjectTable.Size = new System.Drawing.Size(496, 32);
            this.lblProjectTable.TabIndex = 1;
            this.lblProjectTable.Text = "Tables de projets dans tout le groupe";
            this.lblProjectTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trvProjectTable
            // 
            this.trvProjectTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvProjectTable.Location = new System.Drawing.Point(499, 65);
            this.trvProjectTable.Name = "trvProjectTable";
            this.trvProjectTable.Size = new System.Drawing.Size(496, 536);
            this.trvProjectTable.TabIndex = 4;
            // 
            // cmdRunWhereUsedGroupTableAnalyse
            // 
            this.cmdRunWhereUsedGroupTableAnalyse.Location = new System.Drawing.Point(3, 3);
            this.cmdRunWhereUsedGroupTableAnalyse.Name = "cmdRunWhereUsedGroupTableAnalyse";
            this.cmdRunWhereUsedGroupTableAnalyse.Size = new System.Drawing.Size(75, 24);
            this.cmdRunWhereUsedGroupTableAnalyse.TabIndex = 5;
            this.cmdRunWhereUsedGroupTableAnalyse.Text = "Analyser";
            this.cmdRunWhereUsedGroupTableAnalyse.UseVisualStyleBackColor = true;
            this.cmdRunWhereUsedGroupTableAnalyse.Click += new System.EventHandler(this.cmdRunWhereUsedAnalyse_Click);
            // 
            // tlpProjectTable
            // 
            this.tlpProjectTable.ColumnCount = 3;
            this.tlpProjectTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tlpProjectTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 167F));
            this.tlpProjectTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tlpProjectTable.Controls.Add(this.cmdRunWhereUsedProjectTableAnalyse, 0, 0);
            this.tlpProjectTable.Controls.Add(this.cboPackage, 1, 0);
            this.tlpProjectTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProjectTable.Location = new System.Drawing.Point(496, 0);
            this.tlpProjectTable.Margin = new System.Windows.Forms.Padding(0);
            this.tlpProjectTable.Name = "tlpProjectTable";
            this.tlpProjectTable.RowCount = 1;
            this.tlpProjectTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProjectTable.Size = new System.Drawing.Size(502, 30);
            this.tlpProjectTable.TabIndex = 7;
            // 
            // cmdRunWhereUsedProjectTableAnalyse
            // 
            this.cmdRunWhereUsedProjectTableAnalyse.Location = new System.Drawing.Point(3, 3);
            this.cmdRunWhereUsedProjectTableAnalyse.Name = "cmdRunWhereUsedProjectTableAnalyse";
            this.cmdRunWhereUsedProjectTableAnalyse.Size = new System.Drawing.Size(75, 23);
            this.cmdRunWhereUsedProjectTableAnalyse.TabIndex = 6;
            this.cmdRunWhereUsedProjectTableAnalyse.Text = "Analyser";
            this.cmdRunWhereUsedProjectTableAnalyse.UseVisualStyleBackColor = true;
            this.cmdRunWhereUsedProjectTableAnalyse.Click += new System.EventHandler(this.cmdRunWhereUsedProjectTableAnalyse_Click);
            // 
            // cboPackage
            // 
            this.cboPackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPackage.FormattingEnabled = true;
            this.cboPackage.Location = new System.Drawing.Point(91, 3);
            this.cboPackage.Name = "cboPackage";
            this.cboPackage.Size = new System.Drawing.Size(160, 21);
            this.cboPackage.TabIndex = 7;
            this.cboPackage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboPackage_KeyDown);
            // 
            // ucWhereUsed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpWhereUsed);
            this.Name = "ucWhereUsed";
            this.Size = new System.Drawing.Size(998, 624);
            this.tlpWhereUsed.ResumeLayout(false);
            this.tlpWhereUsed.PerformLayout();
            this.tlpProjectTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpWhereUsed;
        private System.Windows.Forms.Label lblGroupTable;
        private System.Windows.Forms.Label lblProjectTable;
        private System.Windows.Forms.TreeView trvGroupTable;
        private System.Windows.Forms.TreeView trvProjectTable;
        private System.Windows.Forms.Button cmdRunWhereUsedGroupTableAnalyse;
        private System.Windows.Forms.Button cmdRunWhereUsedProjectTableAnalyse;
        private System.Windows.Forms.TableLayoutPanel tlpProjectTable;
        private System.Windows.Forms.ComboBox cboPackage;
    }
}

namespace EquinoxeExtendPlugin.Controls.WhereUsedTable
{
    partial class ucWhereUsed
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
            this.cmdRunWhereUsedAnalyse = new System.Windows.Forms.Button();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.pagGroup = new System.Windows.Forms.TabPage();
            this.pagProject = new System.Windows.Forms.TabPage();
            this.tabProject = new System.Windows.Forms.TabControl();
            this.pagProjectTable = new System.Windows.Forms.TabPage();
            this.tlpUnused = new System.Windows.Forms.TableLayoutPanel();
            this.cmdRunUnusedAnalyse = new System.Windows.Forms.Button();
            this.dgvUnusedTable = new System.Windows.Forms.DataGridView();
            this.cmdExportProjectTableToExcel = new System.Windows.Forms.Button();
            this.pagVariable = new System.Windows.Forms.TabPage();
            this.tlpVariables = new System.Windows.Forms.TableLayoutPanel();
            this.dgvUnusedVariables = new System.Windows.Forms.DataGridView();
            this.cmdExportProjectVariablesToExcel = new System.Windows.Forms.Button();
            this.cmdRunUnusedVariable = new System.Windows.Forms.Button();
            this.tlpWhereUsed.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.pagGroup.SuspendLayout();
            this.pagProject.SuspendLayout();
            this.tabProject.SuspendLayout();
            this.pagProjectTable.SuspendLayout();
            this.tlpUnused.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedTable)).BeginInit();
            this.pagVariable.SuspendLayout();
            this.tlpVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedVariables)).BeginInit();
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
            this.tlpWhereUsed.Controls.Add(this.cmdRunWhereUsedAnalyse, 0, 0);
            this.tlpWhereUsed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpWhereUsed.Location = new System.Drawing.Point(3, 3);
            this.tlpWhereUsed.Name = "tlpWhereUsed";
            this.tlpWhereUsed.RowCount = 3;
            this.tlpWhereUsed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpWhereUsed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpWhereUsed.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWhereUsed.Size = new System.Drawing.Size(984, 592);
            this.tlpWhereUsed.TabIndex = 0;
            // 
            // lblGroupTable
            // 
            this.lblGroupTable.AutoSize = true;
            this.lblGroupTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGroupTable.Location = new System.Drawing.Point(3, 30);
            this.lblGroupTable.Name = "lblGroupTable";
            this.lblGroupTable.Size = new System.Drawing.Size(483, 45);
            this.lblGroupTable.TabIndex = 0;
            this.lblGroupTable.Text = "Tables de groupes dans tous les projets";
            this.lblGroupTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trvGroupTable
            // 
            this.trvGroupTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvGroupTable.Location = new System.Drawing.Point(3, 78);
            this.trvGroupTable.Name = "trvGroupTable";
            this.trvGroupTable.Size = new System.Drawing.Size(483, 511);
            this.trvGroupTable.TabIndex = 3;
            // 
            // lblProjectTable
            // 
            this.lblProjectTable.AutoSize = true;
            this.lblProjectTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProjectTable.Location = new System.Drawing.Point(492, 30);
            this.lblProjectTable.Name = "lblProjectTable";
            this.lblProjectTable.Size = new System.Drawing.Size(489, 45);
            this.lblProjectTable.TabIndex = 1;
            this.lblProjectTable.Text = "Tables de projets dans tout le groupe\r\n- Ligne rouge : fichier excel introuvable " +
    "au chein spécifié\r\n- Icone warning : Les données des tables utilisant le fichier" +
    " ne base ne sont pas identiques";
            this.lblProjectTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trvProjectTable
            // 
            this.trvProjectTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvProjectTable.Location = new System.Drawing.Point(492, 78);
            this.trvProjectTable.Name = "trvProjectTable";
            this.trvProjectTable.Size = new System.Drawing.Size(489, 511);
            this.trvProjectTable.TabIndex = 4;
            // 
            // cmdRunWhereUsedAnalyse
            // 
            this.cmdRunWhereUsedAnalyse.Location = new System.Drawing.Point(3, 3);
            this.cmdRunWhereUsedAnalyse.Name = "cmdRunWhereUsedAnalyse";
            this.cmdRunWhereUsedAnalyse.Size = new System.Drawing.Size(75, 24);
            this.cmdRunWhereUsedAnalyse.TabIndex = 5;
            this.cmdRunWhereUsedAnalyse.Text = "Analyser";
            this.cmdRunWhereUsedAnalyse.UseVisualStyleBackColor = true;
            this.cmdRunWhereUsedAnalyse.Click += new System.EventHandler(this.cmdRunWhereUsedAnalyse_Click);
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.pagGroup);
            this.tabMain.Controls.Add(this.pagProject);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(998, 624);
            this.tabMain.TabIndex = 1;
            // 
            // pagGroup
            // 
            this.pagGroup.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagGroup.Controls.Add(this.tlpWhereUsed);
            this.pagGroup.Location = new System.Drawing.Point(4, 22);
            this.pagGroup.Name = "pagGroup";
            this.pagGroup.Padding = new System.Windows.Forms.Padding(3);
            this.pagGroup.Size = new System.Drawing.Size(990, 598);
            this.pagGroup.TabIndex = 0;
            this.pagGroup.Text = "Groupe";
            // 
            // pagProject
            // 
            this.pagProject.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagProject.Controls.Add(this.tabProject);
            this.pagProject.Location = new System.Drawing.Point(4, 22);
            this.pagProject.Name = "pagProject";
            this.pagProject.Padding = new System.Windows.Forms.Padding(3);
            this.pagProject.Size = new System.Drawing.Size(990, 598);
            this.pagProject.TabIndex = 1;
            this.pagProject.Text = "Projet";
            // 
            // tabProject
            // 
            this.tabProject.Controls.Add(this.pagProjectTable);
            this.tabProject.Controls.Add(this.pagVariable);
            this.tabProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabProject.Location = new System.Drawing.Point(3, 3);
            this.tabProject.Name = "tabProject";
            this.tabProject.SelectedIndex = 0;
            this.tabProject.Size = new System.Drawing.Size(984, 592);
            this.tabProject.TabIndex = 1;
            // 
            // pagProjectTable
            // 
            this.pagProjectTable.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagProjectTable.Controls.Add(this.tlpUnused);
            this.pagProjectTable.Location = new System.Drawing.Point(4, 22);
            this.pagProjectTable.Name = "pagProjectTable";
            this.pagProjectTable.Padding = new System.Windows.Forms.Padding(3);
            this.pagProjectTable.Size = new System.Drawing.Size(976, 566);
            this.pagProjectTable.TabIndex = 0;
            this.pagProjectTable.Text = "Tables";
            // 
            // tlpUnused
            // 
            this.tlpUnused.ColumnCount = 2;
            this.tlpUnused.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUnused.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tlpUnused.Controls.Add(this.cmdRunUnusedAnalyse, 0, 0);
            this.tlpUnused.Controls.Add(this.dgvUnusedTable, 0, 1);
            this.tlpUnused.Controls.Add(this.cmdExportProjectTableToExcel, 1, 0);
            this.tlpUnused.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUnused.Location = new System.Drawing.Point(3, 3);
            this.tlpUnused.Name = "tlpUnused";
            this.tlpUnused.RowCount = 2;
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlpUnused.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUnused.Size = new System.Drawing.Size(970, 560);
            this.tlpUnused.TabIndex = 0;
            // 
            // cmdRunUnusedAnalyse
            // 
            this.cmdRunUnusedAnalyse.Location = new System.Drawing.Point(3, 3);
            this.cmdRunUnusedAnalyse.Name = "cmdRunUnusedAnalyse";
            this.cmdRunUnusedAnalyse.Size = new System.Drawing.Size(75, 23);
            this.cmdRunUnusedAnalyse.TabIndex = 0;
            this.cmdRunUnusedAnalyse.Text = "Analyser";
            this.cmdRunUnusedAnalyse.UseVisualStyleBackColor = true;
            this.cmdRunUnusedAnalyse.Click += new System.EventHandler(this.cmdRunUnusedAnalyse_Click);
            // 
            // dgvUnusedTable
            // 
            this.dgvUnusedTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpUnused.SetColumnSpan(this.dgvUnusedTable, 2);
            this.dgvUnusedTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnusedTable.Location = new System.Drawing.Point(3, 42);
            this.dgvUnusedTable.Name = "dgvUnusedTable";
            this.dgvUnusedTable.Size = new System.Drawing.Size(964, 515);
            this.dgvUnusedTable.TabIndex = 1;
            // 
            // cmdExportProjectTableToExcel
            // 
            this.cmdExportProjectTableToExcel.Image = global::EquinoxeExtendPlugin.Properties.Resources.Excel_icon24;
            this.cmdExportProjectTableToExcel.Location = new System.Drawing.Point(932, 3);
            this.cmdExportProjectTableToExcel.Name = "cmdExportProjectTableToExcel";
            this.cmdExportProjectTableToExcel.Size = new System.Drawing.Size(35, 33);
            this.cmdExportProjectTableToExcel.TabIndex = 3;
            this.cmdExportProjectTableToExcel.UseVisualStyleBackColor = true;
            this.cmdExportProjectTableToExcel.Click += new System.EventHandler(this.cmdExportProjectTableToExcel_Click);
            // 
            // pagVariable
            // 
            this.pagVariable.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagVariable.Controls.Add(this.tlpVariables);
            this.pagVariable.Location = new System.Drawing.Point(4, 22);
            this.pagVariable.Name = "pagVariable";
            this.pagVariable.Padding = new System.Windows.Forms.Padding(3);
            this.pagVariable.Size = new System.Drawing.Size(464, 471);
            this.pagVariable.TabIndex = 1;
            this.pagVariable.Text = "Variables";
            // 
            // tlpVariables
            // 
            this.tlpVariables.ColumnCount = 2;
            this.tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVariables.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlpVariables.Controls.Add(this.dgvUnusedVariables, 0, 1);
            this.tlpVariables.Controls.Add(this.cmdExportProjectVariablesToExcel, 1, 0);
            this.tlpVariables.Controls.Add(this.cmdRunUnusedVariable, 0, 0);
            this.tlpVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVariables.Location = new System.Drawing.Point(3, 3);
            this.tlpVariables.Name = "tlpVariables";
            this.tlpVariables.RowCount = 2;
            this.tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlpVariables.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpVariables.Size = new System.Drawing.Size(458, 465);
            this.tlpVariables.TabIndex = 1;
            // 
            // dgvUnusedVariables
            // 
            this.dgvUnusedVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpVariables.SetColumnSpan(this.dgvUnusedVariables, 2);
            this.dgvUnusedVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnusedVariables.Location = new System.Drawing.Point(3, 42);
            this.dgvUnusedVariables.Name = "dgvUnusedVariables";
            this.dgvUnusedVariables.Size = new System.Drawing.Size(452, 420);
            this.dgvUnusedVariables.TabIndex = 1;
            // 
            // cmdExportProjectVariablesToExcel
            // 
            this.cmdExportProjectVariablesToExcel.Image = global::EquinoxeExtendPlugin.Properties.Resources.Excel_icon24;
            this.cmdExportProjectVariablesToExcel.Location = new System.Drawing.Point(416, 3);
            this.cmdExportProjectVariablesToExcel.Name = "cmdExportProjectVariablesToExcel";
            this.cmdExportProjectVariablesToExcel.Size = new System.Drawing.Size(36, 33);
            this.cmdExportProjectVariablesToExcel.TabIndex = 2;
            this.cmdExportProjectVariablesToExcel.UseVisualStyleBackColor = true;
            this.cmdExportProjectVariablesToExcel.Click += new System.EventHandler(this.cmdExportProjectVariablesToExcel_Click);
            // 
            // cmdRunUnusedVariable
            // 
            this.cmdRunUnusedVariable.Location = new System.Drawing.Point(3, 3);
            this.cmdRunUnusedVariable.Name = "cmdRunUnusedVariable";
            this.cmdRunUnusedVariable.Size = new System.Drawing.Size(75, 23);
            this.cmdRunUnusedVariable.TabIndex = 0;
            this.cmdRunUnusedVariable.Text = "Analyser";
            this.cmdRunUnusedVariable.UseVisualStyleBackColor = true;
            this.cmdRunUnusedVariable.Click += new System.EventHandler(this.cmdRunUnusedVariable_Click);
            // 
            // ucWhereUsed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Name = "ucWhereUsed";
            this.Size = new System.Drawing.Size(998, 624);
            this.tlpWhereUsed.ResumeLayout(false);
            this.tlpWhereUsed.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.pagGroup.ResumeLayout(false);
            this.pagProject.ResumeLayout(false);
            this.tabProject.ResumeLayout(false);
            this.pagProjectTable.ResumeLayout(false);
            this.tlpUnused.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedTable)).EndInit();
            this.pagVariable.ResumeLayout(false);
            this.tlpVariables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedVariables)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpWhereUsed;
        private System.Windows.Forms.Label lblGroupTable;
        private System.Windows.Forms.Label lblProjectTable;
        private System.Windows.Forms.TreeView trvGroupTable;
        private System.Windows.Forms.TreeView trvProjectTable;
        private System.Windows.Forms.Button cmdRunWhereUsedAnalyse;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage pagGroup;
        private System.Windows.Forms.TabPage pagProject;
        private System.Windows.Forms.TableLayoutPanel tlpUnused;
        private System.Windows.Forms.Button cmdRunUnusedAnalyse;
        private System.Windows.Forms.DataGridView dgvUnusedTable;
        private System.Windows.Forms.TableLayoutPanel tlpVariables;
        private System.Windows.Forms.Button cmdRunUnusedVariable;
        private System.Windows.Forms.DataGridView dgvUnusedVariables;
        private System.Windows.Forms.TabControl tabProject;
        private System.Windows.Forms.TabPage pagProjectTable;
        private System.Windows.Forms.TabPage pagVariable;
        private System.Windows.Forms.Button cmdExportProjectVariablesToExcel;
        private System.Windows.Forms.Button cmdExportProjectTableToExcel;
    }
}

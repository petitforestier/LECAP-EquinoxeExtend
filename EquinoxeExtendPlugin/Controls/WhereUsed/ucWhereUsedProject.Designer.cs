namespace EquinoxeExtendPlugin.Controls.WhereUsedTable
{
    partial class ucWhereUsedProject
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
            this.tabProject.SuspendLayout();
            this.pagProjectTable.SuspendLayout();
            this.tlpUnused.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedTable)).BeginInit();
            this.pagVariable.SuspendLayout();
            this.tlpVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnusedVariables)).BeginInit();
            this.SuspendLayout();
            // 
            // tabProject
            // 
            this.tabProject.Controls.Add(this.pagProjectTable);
            this.tabProject.Controls.Add(this.pagVariable);
            this.tabProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabProject.Location = new System.Drawing.Point(0, 0);
            this.tabProject.Name = "tabProject";
            this.tabProject.SelectedIndex = 0;
            this.tabProject.Size = new System.Drawing.Size(620, 517);
            this.tabProject.TabIndex = 1;
            // 
            // pagProjectTable
            // 
            this.pagProjectTable.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pagProjectTable.Controls.Add(this.tlpUnused);
            this.pagProjectTable.Location = new System.Drawing.Point(4, 22);
            this.pagProjectTable.Name = "pagProjectTable";
            this.pagProjectTable.Padding = new System.Windows.Forms.Padding(3);
            this.pagProjectTable.Size = new System.Drawing.Size(612, 491);
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
            this.tlpUnused.Size = new System.Drawing.Size(606, 485);
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
            this.dgvUnusedTable.Size = new System.Drawing.Size(600, 440);
            this.dgvUnusedTable.TabIndex = 1;
            // 
            // cmdExportProjectTableToExcel
            // 
            this.cmdExportProjectTableToExcel.Image = global::EquinoxeExtendPlugin.Properties.Resources.Excel_icon24;
            this.cmdExportProjectTableToExcel.Location = new System.Drawing.Point(568, 3);
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
            this.pagVariable.Size = new System.Drawing.Size(513, 319);
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
            this.tlpVariables.Size = new System.Drawing.Size(507, 313);
            this.tlpVariables.TabIndex = 1;
            // 
            // dgvUnusedVariables
            // 
            this.dgvUnusedVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tlpVariables.SetColumnSpan(this.dgvUnusedVariables, 2);
            this.dgvUnusedVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnusedVariables.Location = new System.Drawing.Point(3, 42);
            this.dgvUnusedVariables.Name = "dgvUnusedVariables";
            this.dgvUnusedVariables.Size = new System.Drawing.Size(501, 268);
            this.dgvUnusedVariables.TabIndex = 1;
            // 
            // cmdExportProjectVariablesToExcel
            // 
            this.cmdExportProjectVariablesToExcel.Image = global::EquinoxeExtendPlugin.Properties.Resources.Excel_icon24;
            this.cmdExportProjectVariablesToExcel.Location = new System.Drawing.Point(465, 3);
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
            // ucWhereUsedProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabProject);
            this.Name = "ucWhereUsedProject";
            this.Size = new System.Drawing.Size(620, 517);
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

namespace EquinoxeExtendPlugin.Controls.Task
{
    partial class ucImportTaskFromPDC
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
            this.dgvProject = new System.Windows.Forms.DataGridView();
            this.tlsMain = new System.Windows.Forms.ToolStrip();
            this.cmdAddTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdHideProjet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdRefresh = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlpFooter = new System.Windows.Forms.TableLayoutPanel();
            this.cboExternalProjectSearch = new System.Windows.Forms.ComboBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProject)).BeginInit();
            this.tlsMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tlpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.dgvProject, 0, 2);
            this.tlpMain.Controls.Add(this.tlsMain, 0, 1);
            this.tlpMain.Controls.Add(this.statusStrip1, 0, 3);
            this.tlpMain.Controls.Add(this.tlpFooter, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.Size = new System.Drawing.Size(975, 519);
            this.tlpMain.TabIndex = 0;
            // 
            // dgvProject
            // 
            this.dgvProject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProject.Location = new System.Drawing.Point(3, 63);
            this.dgvProject.Name = "dgvProject";
            this.dgvProject.Size = new System.Drawing.Size(969, 428);
            this.dgvProject.TabIndex = 0;
            // 
            // tlsMain
            // 
            this.tlsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlsMain.ImageScalingSize = new System.Drawing.Size(29, 29);
            this.tlsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdAddTask,
            this.toolStripSeparator1,
            this.cmdHideProjet,
            this.toolStripSeparator2,
            this.cmdRefresh});
            this.tlsMain.Location = new System.Drawing.Point(0, 30);
            this.tlsMain.Name = "tlsMain";
            this.tlsMain.Size = new System.Drawing.Size(975, 30);
            this.tlsMain.TabIndex = 1;
            this.tlsMain.Text = "toolStrip1";
            // 
            // cmdAddTask
            // 
            this.cmdAddTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdAddTask.Image = global::EquinoxeExtendPlugin.Properties.Resources.add_icone;
            this.cmdAddTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAddTask.Name = "cmdAddTask";
            this.cmdAddTask.Size = new System.Drawing.Size(33, 27);
            this.cmdAddTask.Text = "Créer une tâche depuis le projet sélectionné";
            this.cmdAddTask.Click += new System.EventHandler(this.cmdAddTask_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // cmdHideProjet
            // 
            this.cmdHideProjet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdHideProjet.Image = global::EquinoxeExtendPlugin.Properties.Resources.delete_icone;
            this.cmdHideProjet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdHideProjet.Name = "cmdHideProjet";
            this.cmdHideProjet.Size = new System.Drawing.Size(33, 27);
            this.cmdHideProjet.Text = "Cacher le projet";
            this.cmdHideProjet.Click += new System.EventHandler(this.cmdHideProjet_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdRefresh.Image = global::EquinoxeExtendPlugin.Properties.Resources.Button_Refresh_icon;
            this.cmdRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(33, 27);
            this.cmdRefresh.Text = "Rafraichir la liste des projets en cours";
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 494);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(975, 25);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 20);
            // 
            // tlpFooter
            // 
            this.tlpFooter.ColumnCount = 3;
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFooter.Controls.Add(this.cboExternalProjectSearch, 0, 0);
            this.tlpFooter.Controls.Add(this.cmdSearch, 1, 0);
            this.tlpFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFooter.Location = new System.Drawing.Point(0, 0);
            this.tlpFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpFooter.Name = "tlpFooter";
            this.tlpFooter.RowCount = 1;
            this.tlpFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFooter.Size = new System.Drawing.Size(975, 30);
            this.tlpFooter.TabIndex = 3;
            // 
            // cboExternalProjectSearch
            // 
            this.cboExternalProjectSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboExternalProjectSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExternalProjectSearch.FormattingEnabled = true;
            this.cboExternalProjectSearch.Location = new System.Drawing.Point(3, 3);
            this.cboExternalProjectSearch.Name = "cboExternalProjectSearch";
            this.cboExternalProjectSearch.Size = new System.Drawing.Size(148, 21);
            this.cboExternalProjectSearch.TabIndex = 1;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSearch.Location = new System.Drawing.Point(157, 3);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 24);
            this.cmdSearch.TabIndex = 0;
            this.cmdSearch.Text = "Rechercher";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // ucImportTaskFromPDC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ucImportTaskFromPDC";
            this.Size = new System.Drawing.Size(975, 519);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProject)).EndInit();
            this.tlsMain.ResumeLayout(false);
            this.tlsMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tlpFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.DataGridView dgvProject;
        private System.Windows.Forms.ToolStrip tlsMain;
        private System.Windows.Forms.ToolStripButton cmdAddTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.ToolStripButton cmdRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdHideProjet;
        private System.Windows.Forms.TableLayoutPanel tlpFooter;
        private System.Windows.Forms.ComboBox cboExternalProjectSearch;
        private System.Windows.Forms.Button cmdSearch;
    }
}

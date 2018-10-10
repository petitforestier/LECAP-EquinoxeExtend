namespace EquinoxeExtendPlugin.Controls.Task
{
    partial class ucMainTaskManager
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.ucNavigator = new Library.Control.UserControls.ucNavigator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdAddMainTask = new System.Windows.Forms.ToolStripButton();
            this.cmdUpdateMainTask = new System.Windows.Forms.ToolStripButton();
            this.cmdCancelTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdImportFromProjectExcelFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdUpPriority = new System.Windows.Forms.ToolStripButton();
            this.cmdDownPriority = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdAcceptRequestMainTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.cmdSetTaskPriority = new System.Windows.Forms.ToolStripButton();
            this.tlpMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.statusStrip1, 0, 2);
            this.tlpMain.Controls.Add(this.tlpHeader, 0, 0);
            this.tlpMain.Controls.Add(this.dgvMain, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(880, 470);
            this.tlpMain.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 450);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(880, 20);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(13, 15);
            this.lblMessage.Text = "--";
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 311F));
            this.tlpHeader.Controls.Add(this.ucNavigator, 1, 0);
            this.tlpHeader.Controls.Add(this.toolStrip1, 0, 0);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeader.Location = new System.Drawing.Point(0, 0);
            this.tlpHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Size = new System.Drawing.Size(880, 30);
            this.tlpHeader.TabIndex = 1;
            // 
            // ucNavigator
            // 
            this.ucNavigator.Count = 0;
            this.ucNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucNavigator.Location = new System.Drawing.Point(569, 0);
            this.ucNavigator.Margin = new System.Windows.Forms.Padding(0);
            this.ucNavigator.Name = "ucNavigator";
            this.ucNavigator.Size = new System.Drawing.Size(311, 30);
            this.ucNavigator.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(29, 29);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdAddMainTask,
            this.cmdUpdateMainTask,
            this.cmdCancelTask,
            this.toolStripSeparator4,
            this.cmdImportFromProjectExcelFile,
            this.toolStripSeparator1,
            this.cmdUpPriority,
            this.cmdDownPriority,
            this.cmdSetTaskPriority,
            this.toolStripSeparator2,
            this.cmdAcceptRequestMainTask,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(569, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdAddMainTask
            // 
            this.cmdAddMainTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdAddMainTask.Image = global::EquinoxeExtendPlugin.Properties.Resources.add_icone;
            this.cmdAddMainTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAddMainTask.Name = "cmdAddMainTask";
            this.cmdAddMainTask.Size = new System.Drawing.Size(33, 27);
            this.cmdAddMainTask.Text = "Ajouter tâche";
            this.cmdAddMainTask.Click += new System.EventHandler(this.cmdAddMainTask_Click);
            // 
            // cmdUpdateMainTask
            // 
            this.cmdUpdateMainTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdUpdateMainTask.Image = global::EquinoxeExtendPlugin.Properties.Resources.edit_icon;
            this.cmdUpdateMainTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUpdateMainTask.Name = "cmdUpdateMainTask";
            this.cmdUpdateMainTask.Size = new System.Drawing.Size(33, 27);
            this.cmdUpdateMainTask.Text = "toolStripButton2";
            this.cmdUpdateMainTask.ToolTipText = "Modifier tâche";
            this.cmdUpdateMainTask.Click += new System.EventHandler(this.cmdUpdateMainTask_Click);
            // 
            // cmdCancelTask
            // 
            this.cmdCancelTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdCancelTask.Image = global::EquinoxeExtendPlugin.Properties.Resources.delete_icone;
            this.cmdCancelTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdCancelTask.Name = "cmdCancelTask";
            this.cmdCancelTask.Size = new System.Drawing.Size(33, 27);
            this.cmdCancelTask.Text = "Annuler la tâche";
            this.cmdCancelTask.Click += new System.EventHandler(this.cmdCancelTask_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 30);
            // 
            // cmdImportFromProjectExcelFile
            // 
            this.cmdImportFromProjectExcelFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdImportFromProjectExcelFile.Image = global::EquinoxeExtendPlugin.Properties.Resources.import_icon;
            this.cmdImportFromProjectExcelFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdImportFromProjectExcelFile.Name = "cmdImportFromProjectExcelFile";
            this.cmdImportFromProjectExcelFile.Size = new System.Drawing.Size(33, 27);
            this.cmdImportFromProjectExcelFile.Text = "Importer des projects développer catalogue";
            this.cmdImportFromProjectExcelFile.Click += new System.EventHandler(this.cmdImportFromProjectExcelFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // cmdUpPriority
            // 
            this.cmdUpPriority.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdUpPriority.Image = global::EquinoxeExtendPlugin.Properties.Resources.up_icon;
            this.cmdUpPriority.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUpPriority.Name = "cmdUpPriority";
            this.cmdUpPriority.Size = new System.Drawing.Size(33, 27);
            this.cmdUpPriority.Text = "Monter la priorité";
            this.cmdUpPriority.Click += new System.EventHandler(this.cmdUpPriority_Click);
            // 
            // cmdDownPriority
            // 
            this.cmdDownPriority.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDownPriority.Image = global::EquinoxeExtendPlugin.Properties.Resources.down_icon;
            this.cmdDownPriority.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDownPriority.Name = "cmdDownPriority";
            this.cmdDownPriority.Size = new System.Drawing.Size(33, 27);
            this.cmdDownPriority.Text = "Descendre la priorité";
            this.cmdDownPriority.Click += new System.EventHandler(this.cmdDownPriority_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // cmdAcceptRequestMainTask
            // 
            this.cmdAcceptRequestMainTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdAcceptRequestMainTask.Image = global::EquinoxeExtendPlugin.Properties.Resources.accept;
            this.cmdAcceptRequestMainTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAcceptRequestMainTask.Name = "cmdAcceptRequestMainTask";
            this.cmdAcceptRequestMainTask.Size = new System.Drawing.Size(33, 27);
            this.cmdAcceptRequestMainTask.Text = "Accepter la demande et la mettre en attente";
            this.cmdAcceptRequestMainTask.ToolTipText = "Valider la demande de tâche et la mettre en attente";
            this.cmdAcceptRequestMainTask.Click += new System.EventHandler(this.cmdValidateMainTask_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 30);
            // 
            // dgvMain
            // 
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.Location = new System.Drawing.Point(3, 33);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.Size = new System.Drawing.Size(874, 414);
            this.dgvMain.TabIndex = 2;
            this.dgvMain.SelectionChanged += new System.EventHandler(this.dgvMain_SelectionChanged);
            this.dgvMain.DoubleClick += new System.EventHandler(this.dgvMain_DoubleClick);
            // 
            // cmdSetTaskPriority
            // 
            this.cmdSetTaskPriority.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdSetTaskPriority.Image = global::EquinoxeExtendPlugin.Properties.Resources.priority24;
            this.cmdSetTaskPriority.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSetTaskPriority.Name = "cmdSetTaskPriority";
            this.cmdSetTaskPriority.Size = new System.Drawing.Size(33, 27);
            this.cmdSetTaskPriority.Text = "Saisir la priorité de la tâche";
            this.cmdSetTaskPriority.Click += new System.EventHandler(this.cmdSetTaskPriority_Click);
            // 
            // ucMainTaskManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ucMainTaskManager";
            this.Size = new System.Drawing.Size(880, 470);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tlpHeader.ResumeLayout(false);
            this.tlpHeader.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private Library.Control.UserControls.ucNavigator ucNavigator;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdAddMainTask;
        private System.Windows.Forms.ToolStripButton cmdUpdateMainTask;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdAcceptRequestMainTask;
        private System.Windows.Forms.ToolStripButton cmdCancelTask;
        private System.Windows.Forms.ToolStripButton cmdUpPriority;
        private System.Windows.Forms.ToolStripButton cmdDownPriority;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton cmdImportFromProjectExcelFile;
        private System.Windows.Forms.ToolStripButton cmdSetTaskPriority;
    }
}

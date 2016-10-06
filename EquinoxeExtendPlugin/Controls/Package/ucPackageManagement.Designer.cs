namespace EquinoxeExtendPlugin.Controls.Task
{
    partial class ucPackageManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPackageManagement));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.fraSearch = new System.Windows.Forms.GroupBox();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.cboPackageSearch = new System.Windows.Forms.ComboBox();
            this.cmdPackageSearch = new System.Windows.Forms.Button();
            this.splFooter = new System.Windows.Forms.SplitContainer();
            this.tlpLeft = new System.Windows.Forms.TableLayoutPanel();
            this.fraPackage = new System.Windows.Forms.GroupBox();
            this.tlpPackage = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdAddPackage = new System.Windows.Forms.ToolStripButton();
            this.cmdDeletePackage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDeployToDev = new System.Windows.Forms.ToolStripButton();
            this.cmdDeployToStaging = new System.Windows.Forms.ToolStripButton();
            this.cmdDeployToProduction = new System.Windows.Forms.ToolStripButton();
            this.cmdRestoreFromBackup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdLockUnlock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvPackage = new System.Windows.Forms.DataGridView();
            this.fraMainTask = new System.Windows.Forms.GroupBox();
            this.dgvMainTask = new System.Windows.Forms.DataGridView();
            this.tlpRight = new System.Windows.Forms.TableLayoutPanel();
            this.fraProjectTask = new System.Windows.Forms.GroupBox();
            this.dgvSubTask = new System.Windows.Forms.DataGridView();
            this.fraDeployement = new System.Windows.Forms.GroupBox();
            this.dgvDeployement = new System.Windows.Forms.DataGridView();
            this.tlpMain.SuspendLayout();
            this.fraSearch.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splFooter)).BeginInit();
            this.splFooter.Panel1.SuspendLayout();
            this.splFooter.Panel2.SuspendLayout();
            this.splFooter.SuspendLayout();
            this.tlpLeft.SuspendLayout();
            this.fraPackage.SuspendLayout();
            this.tlpPackage.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackage)).BeginInit();
            this.fraMainTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainTask)).BeginInit();
            this.tlpRight.SuspendLayout();
            this.fraProjectTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubTask)).BeginInit();
            this.fraDeployement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeployement)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.fraSearch, 0, 0);
            this.tlpMain.Controls.Add(this.splFooter, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(1050, 575);
            this.tlpMain.TabIndex = 0;
            // 
            // fraSearch
            // 
            this.fraSearch.Controls.Add(this.tlpHeader);
            this.fraSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraSearch.Location = new System.Drawing.Point(3, 3);
            this.fraSearch.Name = "fraSearch";
            this.fraSearch.Size = new System.Drawing.Size(1044, 50);
            this.fraSearch.TabIndex = 1;
            this.fraSearch.TabStop = false;
            this.fraSearch.Text = "Recherche";
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Controls.Add(this.cboPackageSearch, 0, 0);
            this.tlpHeader.Controls.Add(this.cmdPackageSearch, 1, 0);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeader.Location = new System.Drawing.Point(3, 16);
            this.tlpHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHeader.Size = new System.Drawing.Size(1038, 31);
            this.tlpHeader.TabIndex = 3;
            // 
            // cboPackageSearch
            // 
            this.cboPackageSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPackageSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPackageSearch.FormattingEnabled = true;
            this.cboPackageSearch.Location = new System.Drawing.Point(3, 3);
            this.cboPackageSearch.Name = "cboPackageSearch";
            this.cboPackageSearch.Size = new System.Drawing.Size(158, 21);
            this.cboPackageSearch.TabIndex = 0;
            // 
            // cmdPackageSearch
            // 
            this.cmdPackageSearch.Location = new System.Drawing.Point(167, 3);
            this.cmdPackageSearch.Name = "cmdPackageSearch";
            this.cmdPackageSearch.Size = new System.Drawing.Size(75, 23);
            this.cmdPackageSearch.TabIndex = 1;
            this.cmdPackageSearch.Text = "Rechercher";
            this.cmdPackageSearch.UseVisualStyleBackColor = true;
            this.cmdPackageSearch.Click += new System.EventHandler(this.cmdPackageSearch_Click);
            // 
            // splFooter
            // 
            this.splFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splFooter.Location = new System.Drawing.Point(3, 59);
            this.splFooter.Name = "splFooter";
            // 
            // splFooter.Panel1
            // 
            this.splFooter.Panel1.Controls.Add(this.tlpLeft);
            // 
            // splFooter.Panel2
            // 
            this.splFooter.Panel2.Controls.Add(this.tlpRight);
            this.splFooter.Size = new System.Drawing.Size(1044, 513);
            this.splFooter.SplitterDistance = 630;
            this.splFooter.TabIndex = 3;
            // 
            // tlpLeft
            // 
            this.tlpLeft.ColumnCount = 1;
            this.tlpLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLeft.Controls.Add(this.fraPackage, 0, 0);
            this.tlpLeft.Controls.Add(this.fraMainTask, 0, 1);
            this.tlpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLeft.Location = new System.Drawing.Point(0, 0);
            this.tlpLeft.Name = "tlpLeft";
            this.tlpLeft.RowCount = 2;
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLeft.Size = new System.Drawing.Size(628, 511);
            this.tlpLeft.TabIndex = 0;
            // 
            // fraPackage
            // 
            this.fraPackage.Controls.Add(this.tlpPackage);
            this.fraPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraPackage.Location = new System.Drawing.Point(3, 3);
            this.fraPackage.Name = "fraPackage";
            this.fraPackage.Size = new System.Drawing.Size(622, 249);
            this.fraPackage.TabIndex = 1;
            this.fraPackage.TabStop = false;
            this.fraPackage.Text = "Packages";
            // 
            // tlpPackage
            // 
            this.tlpPackage.ColumnCount = 1;
            this.tlpPackage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPackage.Controls.Add(this.toolStrip1, 0, 0);
            this.tlpPackage.Controls.Add(this.dgvPackage, 0, 1);
            this.tlpPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPackage.Location = new System.Drawing.Point(3, 16);
            this.tlpPackage.Name = "tlpPackage";
            this.tlpPackage.RowCount = 2;
            this.tlpPackage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpPackage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPackage.Size = new System.Drawing.Size(616, 230);
            this.tlpPackage.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(29, 29);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdAddPackage,
            this.cmdDeletePackage,
            this.toolStripSeparator1,
            this.cmdDeployToDev,
            this.cmdDeployToStaging,
            this.cmdDeployToProduction,
            this.cmdRestoreFromBackup,
            this.toolStripSeparator2,
            this.cmdLockUnlock,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(616, 30);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdAddPackage
            // 
            this.cmdAddPackage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdAddPackage.Image = global::EquinoxeExtendPlugin.Properties.Resources.add_icone;
            this.cmdAddPackage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAddPackage.Name = "cmdAddPackage";
            this.cmdAddPackage.Size = new System.Drawing.Size(33, 27);
            this.cmdAddPackage.Text = "Créer un package";
            this.cmdAddPackage.Click += new System.EventHandler(this.cmdAddPackage_Click);
            // 
            // cmdDeletePackage
            // 
            this.cmdDeletePackage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDeletePackage.Image = global::EquinoxeExtendPlugin.Properties.Resources.delete_icone;
            this.cmdDeletePackage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDeletePackage.Name = "cmdDeletePackage";
            this.cmdDeletePackage.Size = new System.Drawing.Size(33, 27);
            this.cmdDeletePackage.Text = "toolStripButton1";
            this.cmdDeletePackage.ToolTipText = "Supprimer un package";
            this.cmdDeletePackage.Click += new System.EventHandler(this.cmdDeletePackage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // cmdDeployToDev
            // 
            this.cmdDeployToDev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDeployToDev.Image = global::EquinoxeExtendPlugin.Properties.Resources.Gear_icon24;
            this.cmdDeployToDev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDeployToDev.Name = "cmdDeployToDev";
            this.cmdDeployToDev.Size = new System.Drawing.Size(33, 27);
            this.cmdDeployToDev.Text = "(Ré) Ouvrir Package";
            this.cmdDeployToDev.Click += new System.EventHandler(this.cmdDeployToDev_Click);
            // 
            // cmdDeployToStaging
            // 
            this.cmdDeployToStaging.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDeployToStaging.Image = ((System.Drawing.Image)(resources.GetObject("cmdDeployToStaging.Image")));
            this.cmdDeployToStaging.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDeployToStaging.Name = "cmdDeployToStaging";
            this.cmdDeployToStaging.Size = new System.Drawing.Size(33, 27);
            this.cmdDeployToStaging.Text = "Déploier vers Préprod";
            this.cmdDeployToStaging.Click += new System.EventHandler(this.cmdDeployToStaging_Click);
            // 
            // cmdDeployToProduction
            // 
            this.cmdDeployToProduction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDeployToProduction.Enabled = false;
            this.cmdDeployToProduction.Image = global::EquinoxeExtendPlugin.Properties.Resources.accept;
            this.cmdDeployToProduction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDeployToProduction.Name = "cmdDeployToProduction";
            this.cmdDeployToProduction.Size = new System.Drawing.Size(33, 27);
            this.cmdDeployToProduction.Text = "Déploier en Production";
            this.cmdDeployToProduction.Click += new System.EventHandler(this.cmdDeployToProduction_Click);
            // 
            // cmdRestoreFromBackup
            // 
            this.cmdRestoreFromBackup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdRestoreFromBackup.Enabled = false;
            this.cmdRestoreFromBackup.Image = global::EquinoxeExtendPlugin.Properties.Resources.undo_icon;
            this.cmdRestoreFromBackup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRestoreFromBackup.Name = "cmdRestoreFromBackup";
            this.cmdRestoreFromBackup.Size = new System.Drawing.Size(33, 27);
            this.cmdRestoreFromBackup.Text = "Restorer production depuis backup";
            this.cmdRestoreFromBackup.Click += new System.EventHandler(this.cmdRestoreFromBackup_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // cmdLockUnlock
            // 
            this.cmdLockUnlock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdLockUnlock.Image = global::EquinoxeExtendPlugin.Properties.Resources._lock;
            this.cmdLockUnlock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdLockUnlock.Name = "cmdLockUnlock";
            this.cmdLockUnlock.Size = new System.Drawing.Size(33, 27);
            this.cmdLockUnlock.Text = "Verrouiller/Déverrouiller l\'ajout de tâche";
            this.cmdLockUnlock.Click += new System.EventHandler(this.cmdLockUnlock_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 30);
            // 
            // dgvPackage
            // 
            this.dgvPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPackage.Location = new System.Drawing.Point(3, 33);
            this.dgvPackage.Name = "dgvPackage";
            this.dgvPackage.Size = new System.Drawing.Size(610, 194);
            this.dgvPackage.TabIndex = 4;
            this.dgvPackage.SelectionChanged += new System.EventHandler(this.dgvPackage_SelectionChanged);
            // 
            // fraMainTask
            // 
            this.fraMainTask.Controls.Add(this.dgvMainTask);
            this.fraMainTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraMainTask.Location = new System.Drawing.Point(3, 258);
            this.fraMainTask.Name = "fraMainTask";
            this.fraMainTask.Size = new System.Drawing.Size(622, 250);
            this.fraMainTask.TabIndex = 1;
            this.fraMainTask.TabStop = false;
            this.fraMainTask.Text = "Tâches";
            // 
            // dgvMainTask
            // 
            this.dgvMainTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMainTask.Location = new System.Drawing.Point(3, 16);
            this.dgvMainTask.Name = "dgvMainTask";
            this.dgvMainTask.Size = new System.Drawing.Size(616, 231);
            this.dgvMainTask.TabIndex = 5;
            // 
            // tlpRight
            // 
            this.tlpRight.ColumnCount = 1;
            this.tlpRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRight.Controls.Add(this.fraProjectTask, 0, 1);
            this.tlpRight.Controls.Add(this.fraDeployement, 0, 0);
            this.tlpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRight.Location = new System.Drawing.Point(0, 0);
            this.tlpRight.Name = "tlpRight";
            this.tlpRight.RowCount = 2;
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpRight.Size = new System.Drawing.Size(408, 511);
            this.tlpRight.TabIndex = 0;
            // 
            // fraProjectTask
            // 
            this.fraProjectTask.Controls.Add(this.dgvSubTask);
            this.fraProjectTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraProjectTask.Location = new System.Drawing.Point(3, 258);
            this.fraProjectTask.Name = "fraProjectTask";
            this.fraProjectTask.Size = new System.Drawing.Size(402, 250);
            this.fraProjectTask.TabIndex = 2;
            this.fraProjectTask.TabStop = false;
            this.fraProjectTask.Text = "Sous tâches";
            // 
            // dgvSubTask
            // 
            this.dgvSubTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubTask.Location = new System.Drawing.Point(3, 16);
            this.dgvSubTask.Name = "dgvSubTask";
            this.dgvSubTask.Size = new System.Drawing.Size(396, 231);
            this.dgvSubTask.TabIndex = 6;
            // 
            // fraDeployement
            // 
            this.fraDeployement.Controls.Add(this.dgvDeployement);
            this.fraDeployement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraDeployement.Location = new System.Drawing.Point(3, 3);
            this.fraDeployement.Name = "fraDeployement";
            this.fraDeployement.Size = new System.Drawing.Size(402, 249);
            this.fraDeployement.TabIndex = 1;
            this.fraDeployement.TabStop = false;
            this.fraDeployement.Text = "Déploiements";
            // 
            // dgvDeployement
            // 
            this.dgvDeployement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeployement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDeployement.Location = new System.Drawing.Point(3, 16);
            this.dgvDeployement.Name = "dgvDeployement";
            this.dgvDeployement.Size = new System.Drawing.Size(396, 230);
            this.dgvDeployement.TabIndex = 7;
            // 
            // ucPackageManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ucPackageManagement";
            this.Size = new System.Drawing.Size(1050, 575);
            this.tlpMain.ResumeLayout(false);
            this.fraSearch.ResumeLayout(false);
            this.tlpHeader.ResumeLayout(false);
            this.splFooter.Panel1.ResumeLayout(false);
            this.splFooter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splFooter)).EndInit();
            this.splFooter.ResumeLayout(false);
            this.tlpLeft.ResumeLayout(false);
            this.fraPackage.ResumeLayout(false);
            this.tlpPackage.ResumeLayout(false);
            this.tlpPackage.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackage)).EndInit();
            this.fraMainTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainTask)).EndInit();
            this.tlpRight.ResumeLayout(false);
            this.fraProjectTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubTask)).EndInit();
            this.fraDeployement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeployement)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdAddPackage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdDeployToStaging;
        private System.Windows.Forms.ToolStripButton cmdDeployToProduction;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.ComboBox cboPackageSearch;
        private System.Windows.Forms.Button cmdPackageSearch;
        private System.Windows.Forms.DataGridView dgvPackage;
        private System.Windows.Forms.DataGridView dgvMainTask;
        private System.Windows.Forms.DataGridView dgvSubTask;
        private System.Windows.Forms.DataGridView dgvDeployement;
        private System.Windows.Forms.GroupBox fraProjectTask;
        private System.Windows.Forms.GroupBox fraDeployement;
        private System.Windows.Forms.GroupBox fraMainTask;
        private System.Windows.Forms.GroupBox fraPackage;
        private System.Windows.Forms.TableLayoutPanel tlpPackage;
        private System.Windows.Forms.GroupBox fraSearch;
        private System.Windows.Forms.SplitContainer splFooter;
        private System.Windows.Forms.TableLayoutPanel tlpLeft;
        private System.Windows.Forms.TableLayoutPanel tlpRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cmdRestoreFromBackup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton cmdDeployToDev;
        private System.Windows.Forms.ToolStripButton cmdLockUnlock;
        private System.Windows.Forms.ToolStripButton cmdDeletePackage;
    }
}

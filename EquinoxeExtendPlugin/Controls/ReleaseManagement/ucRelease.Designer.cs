namespace EquinoxeExtendPlugin.Controls.ReleaseManagement
{
    partial class ucRelease
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
            this.ucTaskManager = new EquinoxeExtendPlugin.Controls.ReleaseManagement.ucTaskManager();
            this.ucPackageManagement = new EquinoxeExtendPlugin.Controls.Task.ucPackageManagement();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.pagTasks = new System.Windows.Forms.TabPage();
            this.pagPackages = new System.Windows.Forms.TabPage();
            this.tabMain.SuspendLayout();
            this.pagTasks.SuspendLayout();
            this.pagPackages.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucTaskManager
            // 
            this.ucTaskManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTaskManager.Location = new System.Drawing.Point(3, 3);
            this.ucTaskManager.Name = "ucTaskManager";
            this.ucTaskManager.Size = new System.Drawing.Size(1267, 797);
            this.ucTaskManager.TabIndex = 0;
            // 
            // ucPackageManagement
            // 
            this.ucPackageManagement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPackageManagement.Location = new System.Drawing.Point(3, 3);
            this.ucPackageManagement.Name = "ucPackageManagement";
            this.ucPackageManagement.Size = new System.Drawing.Size(1267, 717);
            this.ucPackageManagement.TabIndex = 0;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.pagTasks);
            this.tabMain.Controls.Add(this.pagPackages);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1281, 829);
            this.tabMain.TabIndex = 2;
            // 
            // pagTasks
            // 
            this.pagTasks.BackColor = System.Drawing.SystemColors.Control;
            this.pagTasks.Controls.Add(this.ucTaskManager);
            this.pagTasks.Location = new System.Drawing.Point(4, 22);
            this.pagTasks.Name = "pagTasks";
            this.pagTasks.Padding = new System.Windows.Forms.Padding(3);
            this.pagTasks.Size = new System.Drawing.Size(1273, 803);
            this.pagTasks.TabIndex = 0;
            this.pagTasks.Text = "Tâches";
            // 
            // pagPackages
            // 
            this.pagPackages.BackColor = System.Drawing.SystemColors.Control;
            this.pagPackages.Controls.Add(this.ucPackageManagement);
            this.pagPackages.Location = new System.Drawing.Point(4, 22);
            this.pagPackages.Name = "pagPackages";
            this.pagPackages.Padding = new System.Windows.Forms.Padding(3);
            this.pagPackages.Size = new System.Drawing.Size(1273, 723);
            this.pagPackages.TabIndex = 1;
            this.pagPackages.Text = "Packages";
            // 
            // ucRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Name = "ucRelease";
            this.Size = new System.Drawing.Size(1281, 829);
            this.tabMain.ResumeLayout(false);
            this.pagTasks.ResumeLayout(false);
            this.pagPackages.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ucTaskManager ucTaskManager;
        private Task.ucPackageManagement ucPackageManagement;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage pagTasks;
        private System.Windows.Forms.TabPage pagPackages;
    }
}

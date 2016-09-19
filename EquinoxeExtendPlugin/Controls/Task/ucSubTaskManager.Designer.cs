namespace EquinoxeExtendPlugin.Controls.Task
{
    partial class ucSubTaskManager
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
            this.tplMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvSubTasks = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdAddSubTask = new System.Windows.Forms.ToolStripButton();
            this.cmdEditSubTask = new System.Windows.Forms.ToolStripButton();
            this.cmdDeleteSubTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tplMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubTasks)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tplMain
            // 
            this.tplMain.ColumnCount = 1;
            this.tplMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.Controls.Add(this.dgvSubTasks, 0, 1);
            this.tplMain.Controls.Add(this.toolStrip1, 0, 0);
            this.tplMain.Controls.Add(this.statusStrip1, 0, 2);
            this.tplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMain.Location = new System.Drawing.Point(0, 0);
            this.tplMain.Name = "tplMain";
            this.tplMain.RowCount = 3;
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplMain.Size = new System.Drawing.Size(454, 290);
            this.tplMain.TabIndex = 0;
            // 
            // dgvSubTasks
            // 
            this.dgvSubTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSubTasks.Location = new System.Drawing.Point(3, 33);
            this.dgvSubTasks.Name = "dgvSubTasks";
            this.dgvSubTasks.Size = new System.Drawing.Size(448, 234);
            this.dgvSubTasks.TabIndex = 0;
            this.dgvSubTasks.SelectionChanged += new System.EventHandler(this.dgvProjectTasks_SelectionChanged);
            this.dgvSubTasks.DoubleClick += new System.EventHandler(this.dgvProjectTasks_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(29, 29);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdAddSubTask,
            this.cmdEditSubTask,
            this.cmdDeleteSubTask,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(454, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdAddSubTask
            // 
            this.cmdAddSubTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdAddSubTask.Image = global::EquinoxeExtendPlugin.Properties.Resources.add_icone;
            this.cmdAddSubTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdAddSubTask.Name = "cmdAddSubTask";
            this.cmdAddSubTask.Size = new System.Drawing.Size(33, 27);
            this.cmdAddSubTask.Text = "Ajouter sous tâche";
            this.cmdAddSubTask.Click += new System.EventHandler(this.cmdAddProjectTask_Click);
            // 
            // cmdEditSubTask
            // 
            this.cmdEditSubTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdEditSubTask.Image = global::EquinoxeExtendPlugin.Properties.Resources.edit_icon;
            this.cmdEditSubTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdEditSubTask.Name = "cmdEditSubTask";
            this.cmdEditSubTask.Size = new System.Drawing.Size(33, 27);
            this.cmdEditSubTask.Text = "Modifier sous tâches";
            this.cmdEditSubTask.Click += new System.EventHandler(this.cmdEditProjectTask_Click);
            // 
            // cmdDeleteSubTask
            // 
            this.cmdDeleteSubTask.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdDeleteSubTask.Image = global::EquinoxeExtendPlugin.Properties.Resources.delete_icone;
            this.cmdDeleteSubTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDeleteSubTask.Name = "cmdDeleteSubTask";
            this.cmdDeleteSubTask.Size = new System.Drawing.Size(33, 27);
            this.cmdDeleteSubTask.Text = "Supprimer sous tâche";
            this.cmdDeleteSubTask.Click += new System.EventHandler(this.cmdDeleteProjectTask_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 270);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(454, 20);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(17, 15);
            this.lblMessage.Text = "--";
            // 
            // ucSubTaskManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tplMain);
            this.Name = "ucSubTaskManager";
            this.Size = new System.Drawing.Size(454, 290);
            this.tplMain.ResumeLayout(false);
            this.tplMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubTasks)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tplMain;
        private System.Windows.Forms.DataGridView dgvSubTasks;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdAddSubTask;
        private System.Windows.Forms.ToolStripButton cmdDeleteSubTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.ToolStripButton cmdEditSubTask;
    }
}

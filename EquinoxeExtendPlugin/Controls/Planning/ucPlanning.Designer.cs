namespace EquinoxeExtendPlugin.Controls.Planning
{
    partial class ucPlanning
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
            this.chtGanttChart = new Braincase.GanttChart.Chart();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.chtGanttChart, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.5012F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.4988F));
            this.tlpMain.Size = new System.Drawing.Size(832, 559);
            this.tlpMain.TabIndex = 0;
            // 
            // chtGanttChart
            // 
            this.chtGanttChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chtGanttChart.FullDateStringFormat = null;
            this.chtGanttChart.Location = new System.Drawing.Point(6, 137);
            this.chtGanttChart.Margin = new System.Windows.Forms.Padding(6);
            this.chtGanttChart.Name = "chtGanttChart";
            this.chtGanttChart.Size = new System.Drawing.Size(820, 416);
            this.chtGanttChart.TabIndex = 0;
            // 
            // ucPlanning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ucPlanning";
            this.Size = new System.Drawing.Size(832, 559);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private Braincase.GanttChart.Chart chtGanttChart;
    }
}

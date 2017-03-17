namespace EquinoxeExtendPlugin.Controls.Task
{
    partial class ucSubTaskEdit
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
            this.components = new System.ComponentModel.Container();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblDevelopper = new System.Windows.Forms.Label();
            this.cboDevelopper = new System.Windows.Forms.ComboBox();
            this.lblProgression = new System.Windows.Forms.Label();
            this.numProgression = new Library.Control.UserControls.ucNumericUpDown();
            this.lblDuration = new System.Windows.Forms.Label();
            this.numDuration = new Library.Control.UserControls.ucNumericUpDown();
            this.lblComments = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.RichTextBox();
            this.tlpFooter = new System.Windows.Forms.TableLayoutPanel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtDesignation = new System.Windows.Forms.TextBox();
            this.lblProject = new System.Windows.Forms.Label();
            this.tlpProject = new System.Windows.Forms.TableLayoutPanel();
            this.cmdDeleteProject = new System.Windows.Forms.Button();
            this.cboProject = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProgression)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.tlpFooter.SuspendLayout();
            this.tlpProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlpMain.Controls.Add(this.lblDevelopper, 0, 2);
            this.tlpMain.Controls.Add(this.cboDevelopper, 1, 2);
            this.tlpMain.Controls.Add(this.lblProgression, 0, 3);
            this.tlpMain.Controls.Add(this.numProgression, 1, 3);
            this.tlpMain.Controls.Add(this.lblDuration, 0, 4);
            this.tlpMain.Controls.Add(this.numDuration, 1, 4);
            this.tlpMain.Controls.Add(this.lblComments, 0, 5);
            this.tlpMain.Controls.Add(this.txtComments, 0, 6);
            this.tlpMain.Controls.Add(this.tlpFooter, 0, 7);
            this.tlpMain.Controls.Add(this.txtDesignation, 1, 0);
            this.tlpMain.Controls.Add(this.lblProject, 0, 1);
            this.tlpMain.Controls.Add(this.tlpProject, 1, 1);
            this.tlpMain.Controls.Add(this.label1, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 8;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlpMain.Size = new System.Drawing.Size(385, 309);
            this.tlpMain.TabIndex = 0;
            // 
            // lblDevelopper
            // 
            this.lblDevelopper.AutoSize = true;
            this.lblDevelopper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDevelopper.Location = new System.Drawing.Point(3, 50);
            this.lblDevelopper.Name = "lblDevelopper";
            this.lblDevelopper.Size = new System.Drawing.Size(104, 25);
            this.lblDevelopper.TabIndex = 4;
            this.lblDevelopper.Text = "Développeur";
            this.lblDevelopper.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDevelopper
            // 
            this.cboDevelopper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDevelopper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDevelopper.FormattingEnabled = true;
            this.cboDevelopper.Location = new System.Drawing.Point(113, 53);
            this.cboDevelopper.Name = "cboDevelopper";
            this.cboDevelopper.Size = new System.Drawing.Size(252, 21);
            this.cboDevelopper.TabIndex = 5;
            // 
            // lblProgression
            // 
            this.lblProgression.AutoSize = true;
            this.lblProgression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProgression.Location = new System.Drawing.Point(3, 75);
            this.lblProgression.Name = "lblProgression";
            this.lblProgression.Size = new System.Drawing.Size(104, 25);
            this.lblProgression.TabIndex = 6;
            this.lblProgression.Text = "Avancement";
            this.lblProgression.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numProgression
            // 
            this.numProgression.Location = new System.Drawing.Point(113, 78);
            this.numProgression.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numProgression.Name = "numProgression";
            this.numProgression.Size = new System.Drawing.Size(120, 20);
            this.numProgression.TabIndex = 7;
            this.numProgression.ThousandsSeparator = true;
            this.numProgression.ValueType = Library.Control.UserControls.ucNumericUpDown.NumericUpDownTypeEnum.IntegerPositiveOnly;
            this.numProgression.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numProgression_KeyDown);
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDuration.Location = new System.Drawing.Point(3, 100);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(104, 25);
            this.lblDuration.TabIndex = 2;
            this.lblDuration.Text = "Charge (jours)";
            this.lblDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numDuration
            // 
            this.numDuration.Location = new System.Drawing.Point(113, 103);
            this.numDuration.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(120, 20);
            this.numDuration.TabIndex = 3;
            this.numDuration.ThousandsSeparator = true;
            this.numDuration.ValueType = Library.Control.UserControls.ucNumericUpDown.NumericUpDownTypeEnum.IntegerPositiveOnly;
            this.numDuration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numDuration_KeyDown);
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComments.Location = new System.Drawing.Point(3, 125);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(104, 25);
            this.lblComments.TabIndex = 8;
            this.lblComments.Text = "Commentaires";
            this.lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtComments
            // 
            this.tlpMain.SetColumnSpan(this.txtComments, 2);
            this.txtComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComments.Location = new System.Drawing.Point(3, 153);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(362, 114);
            this.txtComments.TabIndex = 9;
            this.txtComments.Text = "";
            // 
            // tlpFooter
            // 
            this.tlpFooter.ColumnCount = 3;
            this.tlpMain.SetColumnSpan(this.tlpFooter, 2);
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFooter.Controls.Add(this.cmdOk, 1, 0);
            this.tlpFooter.Controls.Add(this.cmdCancel, 2, 0);
            this.tlpFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFooter.Location = new System.Drawing.Point(3, 273);
            this.tlpFooter.Name = "tlpFooter";
            this.tlpFooter.RowCount = 1;
            this.tlpFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFooter.Size = new System.Drawing.Size(362, 33);
            this.tlpFooter.TabIndex = 10;
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(165, 3);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(94, 27);
            this.cmdOk.TabIndex = 0;
            this.cmdOk.Text = "OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdCancel.Location = new System.Drawing.Point(265, 3);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(94, 27);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtDesignation
            // 
            this.txtDesignation.Location = new System.Drawing.Point(113, 3);
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.Size = new System.Drawing.Size(252, 20);
            this.txtDesignation.TabIndex = 12;
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProject.Location = new System.Drawing.Point(3, 25);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(104, 25);
            this.lblProject.TabIndex = 13;
            this.lblProject.Text = "Projet";
            this.lblProject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tlpProject
            // 
            this.tlpProject.ColumnCount = 2;
            this.tlpProject.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProject.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpProject.Controls.Add(this.cmdDeleteProject, 1, 0);
            this.tlpProject.Controls.Add(this.cboProject, 0, 0);
            this.tlpProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProject.Location = new System.Drawing.Point(110, 25);
            this.tlpProject.Margin = new System.Windows.Forms.Padding(0);
            this.tlpProject.Name = "tlpProject";
            this.tlpProject.RowCount = 1;
            this.tlpProject.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProject.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpProject.Size = new System.Drawing.Size(258, 25);
            this.tlpProject.TabIndex = 14;
            // 
            // cmdDeleteProject
            // 
            this.cmdDeleteProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDeleteProject.Location = new System.Drawing.Point(228, 0);
            this.cmdDeleteProject.Margin = new System.Windows.Forms.Padding(0);
            this.cmdDeleteProject.Name = "cmdDeleteProject";
            this.cmdDeleteProject.Size = new System.Drawing.Size(30, 25);
            this.cmdDeleteProject.TabIndex = 13;
            this.cmdDeleteProject.UseVisualStyleBackColor = true;
            this.cmdDeleteProject.Click += new System.EventHandler(this.cmdDeleteProject_Click);
            // 
            // cboProject
            // 
            this.cboProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProject.FormattingEnabled = true;
            this.cboProject.Location = new System.Drawing.Point(3, 3);
            this.cboProject.Name = "cboProject";
            this.cboProject.Size = new System.Drawing.Size(222, 21);
            this.cboProject.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Désignation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ucSubTaskEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tlpMain);
            this.Name = "ucSubTaskEdit";
            this.Size = new System.Drawing.Size(385, 309);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProgression)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.tlpFooter.ResumeLayout(false);
            this.tlpProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ComboBox cboProject;
        private System.Windows.Forms.Label lblDuration;
        private Library.Control.UserControls.ucNumericUpDown numDuration;
        private System.Windows.Forms.Label lblDevelopper;
        private System.Windows.Forms.ComboBox cboDevelopper;
        private System.Windows.Forms.Label lblProgression;
        private Library.Control.UserControls.ucNumericUpDown numProgression;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.RichTextBox txtComments;
        private System.Windows.Forms.TableLayoutPanel tlpFooter;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtDesignation;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.TableLayoutPanel tlpProject;
        private System.Windows.Forms.Button cmdDeleteProject;
        private System.Windows.Forms.Label label1;
    }
}

namespace EquinoxeExtendPlugin.Controls.Task
{
    partial class ucMainTaskEdit
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
            this.cboRequestUser = new System.Windows.Forms.ComboBox();
            this.lblMainTaskId = new System.Windows.Forms.Label();
            this.txtMainTaskId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTaskType = new System.Windows.Forms.Label();
            this.cboTaskType = new System.Windows.Forms.ComboBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.numPriority = new Library.Control.UserControls.ucNumericUpDown();
            this.lblPackage = new System.Windows.Forms.Label();
            this.lblCreationUser = new System.Windows.Forms.Label();
            this.lblRequestUser = new System.Windows.Forms.Label();
            this.cboCreationUser = new System.Windows.Forms.ComboBox();
            this.lblProductLines = new System.Windows.Forms.Label();
            this.chlProductLines = new System.Windows.Forms.CheckedListBox();
            this.lblObjectifDate = new System.Windows.Forms.Label();
            this.dtpObjectifDate = new System.Windows.Forms.DateTimePicker();
            this.lblProjectNumber = new System.Windows.Forms.Label();
            this.cboProjectNumber = new System.Windows.Forms.ComboBox();
            this.tlpPackage = new System.Windows.Forms.TableLayoutPanel();
            this.cboPackage = new System.Windows.Forms.ComboBox();
            this.cmdDeletePackage = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.tlpFooter = new System.Windows.Forms.TableLayoutPanel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.pagGeneral = new System.Windows.Forms.TabPage();
            this.pagDescription = new System.Windows.Forms.TabPage();
            this.tlpDescription = new System.Windows.Forms.TableLayoutPanel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.RichTextBox();
            this.lblComments = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.RichTextBox();
            this.pagDates = new System.Windows.Forms.TabPage();
            this.tlpDates = new System.Windows.Forms.TableLayoutPanel();
            this.lblCreationDate = new System.Windows.Forms.Label();
            this.lblOpenedDate = new System.Windows.Forms.Label();
            this.lblCompletedDate = new System.Windows.Forms.Label();
            this.txtCreationDate = new System.Windows.Forms.TextBox();
            this.txtOpenedDate = new System.Windows.Forms.TextBox();
            this.txtCompletedDate = new System.Windows.Forms.TextBox();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriority)).BeginInit();
            this.tlpPackage.SuspendLayout();
            this.tlpFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.pagGeneral.SuspendLayout();
            this.pagDescription.SuspendLayout();
            this.tlpDescription.SuspendLayout();
            this.pagDates.SuspendLayout();
            this.tlpDates.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.81356F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.18644F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpMain.Controls.Add(this.cboRequestUser, 1, 9);
            this.tlpMain.Controls.Add(this.lblMainTaskId, 0, 0);
            this.tlpMain.Controls.Add(this.txtMainTaskId, 1, 0);
            this.tlpMain.Controls.Add(this.txtName, 1, 2);
            this.tlpMain.Controls.Add(this.lblName, 0, 2);
            this.tlpMain.Controls.Add(this.lblTaskType, 0, 3);
            this.tlpMain.Controls.Add(this.cboTaskType, 1, 3);
            this.tlpMain.Controls.Add(this.lblPriority, 0, 6);
            this.tlpMain.Controls.Add(this.numPriority, 1, 6);
            this.tlpMain.Controls.Add(this.lblPackage, 0, 7);
            this.tlpMain.Controls.Add(this.lblCreationUser, 0, 8);
            this.tlpMain.Controls.Add(this.lblRequestUser, 0, 9);
            this.tlpMain.Controls.Add(this.cboCreationUser, 1, 8);
            this.tlpMain.Controls.Add(this.lblProductLines, 0, 10);
            this.tlpMain.Controls.Add(this.chlProductLines, 1, 10);
            this.tlpMain.Controls.Add(this.lblObjectifDate, 0, 5);
            this.tlpMain.Controls.Add(this.dtpObjectifDate, 1, 5);
            this.tlpMain.Controls.Add(this.lblProjectNumber, 0, 4);
            this.tlpMain.Controls.Add(this.cboProjectNumber, 1, 4);
            this.tlpMain.Controls.Add(this.tlpPackage, 1, 7);
            this.tlpMain.Controls.Add(this.lblStatus, 0, 1);
            this.tlpMain.Controls.Add(this.txtStatus, 1, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(3, 3);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 11;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMain.Size = new System.Drawing.Size(334, 417);
            this.tlpMain.TabIndex = 0;
            // 
            // cboRequestUser
            // 
            this.cboRequestUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRequestUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRequestUser.FormattingEnabled = true;
            this.cboRequestUser.Location = new System.Drawing.Point(83, 228);
            this.cboRequestUser.Name = "cboRequestUser";
            this.cboRequestUser.Size = new System.Drawing.Size(192, 21);
            this.cboRequestUser.TabIndex = 15;
            // 
            // lblMainTaskId
            // 
            this.lblMainTaskId.AutoSize = true;
            this.lblMainTaskId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMainTaskId.Location = new System.Drawing.Point(3, 0);
            this.lblMainTaskId.Name = "lblMainTaskId";
            this.lblMainTaskId.Size = new System.Drawing.Size(74, 25);
            this.lblMainTaskId.TabIndex = 0;
            this.lblMainTaskId.Text = "Tâche Id";
            this.lblMainTaskId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMainTaskId
            // 
            this.txtMainTaskId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMainTaskId.Location = new System.Drawing.Point(83, 3);
            this.txtMainTaskId.Name = "txtMainTaskId";
            this.txtMainTaskId.ReadOnly = true;
            this.txtMainTaskId.Size = new System.Drawing.Size(192, 20);
            this.txtMainTaskId.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(83, 53);
            this.txtName.MaxLength = 300;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(192, 20);
            this.txtName.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(3, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(74, 25);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Nom";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTaskType
            // 
            this.lblTaskType.AutoSize = true;
            this.lblTaskType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTaskType.Location = new System.Drawing.Point(3, 75);
            this.lblTaskType.Name = "lblTaskType";
            this.lblTaskType.Size = new System.Drawing.Size(74, 25);
            this.lblTaskType.TabIndex = 4;
            this.lblTaskType.Text = "Type";
            this.lblTaskType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTaskType
            // 
            this.cboTaskType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTaskType.FormattingEnabled = true;
            this.cboTaskType.Location = new System.Drawing.Point(83, 78);
            this.cboTaskType.Name = "cboTaskType";
            this.cboTaskType.Size = new System.Drawing.Size(192, 21);
            this.cboTaskType.TabIndex = 5;
            this.cboTaskType.SelectedIndexChanged += new System.EventHandler(this.cboTaskType_SelectedIndexChanged);
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPriority.Location = new System.Drawing.Point(3, 150);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(74, 25);
            this.lblPriority.TabIndex = 8;
            this.lblPriority.Text = "Priorité";
            this.lblPriority.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numPriority
            // 
            this.numPriority.Location = new System.Drawing.Point(83, 153);
            this.numPriority.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPriority.Name = "numPriority";
            this.numPriority.Size = new System.Drawing.Size(120, 20);
            this.numPriority.TabIndex = 9;
            this.numPriority.ThousandsSeparator = true;
            this.numPriority.ValueType = Library.Control.UserControls.ucNumericUpDown.NumericUpDownTypeEnum.IntegerPositiveOnly;
            // 
            // lblPackage
            // 
            this.lblPackage.AutoSize = true;
            this.lblPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPackage.Location = new System.Drawing.Point(3, 175);
            this.lblPackage.Name = "lblPackage";
            this.lblPackage.Size = new System.Drawing.Size(74, 25);
            this.lblPackage.TabIndex = 10;
            this.lblPackage.Text = "Package";
            this.lblPackage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreationUser
            // 
            this.lblCreationUser.AutoSize = true;
            this.lblCreationUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreationUser.Location = new System.Drawing.Point(3, 200);
            this.lblCreationUser.Name = "lblCreationUser";
            this.lblCreationUser.Size = new System.Drawing.Size(74, 25);
            this.lblCreationUser.TabIndex = 12;
            this.lblCreationUser.Text = "Créateur";
            this.lblCreationUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRequestUser
            // 
            this.lblRequestUser.AutoSize = true;
            this.lblRequestUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequestUser.Location = new System.Drawing.Point(3, 225);
            this.lblRequestUser.Name = "lblRequestUser";
            this.lblRequestUser.Size = new System.Drawing.Size(74, 25);
            this.lblRequestUser.TabIndex = 13;
            this.lblRequestUser.Text = "Demandeur";
            this.lblRequestUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCreationUser
            // 
            this.cboCreationUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboCreationUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCreationUser.Enabled = false;
            this.cboCreationUser.FormattingEnabled = true;
            this.cboCreationUser.Location = new System.Drawing.Point(83, 203);
            this.cboCreationUser.Name = "cboCreationUser";
            this.cboCreationUser.Size = new System.Drawing.Size(192, 21);
            this.cboCreationUser.TabIndex = 14;
            // 
            // lblProductLines
            // 
            this.lblProductLines.AutoSize = true;
            this.lblProductLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductLines.Location = new System.Drawing.Point(3, 250);
            this.lblProductLines.Name = "lblProductLines";
            this.lblProductLines.Size = new System.Drawing.Size(74, 167);
            this.lblProductLines.TabIndex = 24;
            this.lblProductLines.Text = "Gammes";
            this.lblProductLines.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chlProductLines
            // 
            this.chlProductLines.CheckOnClick = true;
            this.chlProductLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chlProductLines.FormattingEnabled = true;
            this.chlProductLines.Location = new System.Drawing.Point(83, 253);
            this.chlProductLines.Name = "chlProductLines";
            this.chlProductLines.Size = new System.Drawing.Size(192, 161);
            this.chlProductLines.TabIndex = 25;
            // 
            // lblObjectifDate
            // 
            this.lblObjectifDate.AutoSize = true;
            this.lblObjectifDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblObjectifDate.Location = new System.Drawing.Point(3, 125);
            this.lblObjectifDate.Name = "lblObjectifDate";
            this.lblObjectifDate.Size = new System.Drawing.Size(74, 25);
            this.lblObjectifDate.TabIndex = 26;
            this.lblObjectifDate.Text = "Objectif";
            this.lblObjectifDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpObjectifDate
            // 
            this.dtpObjectifDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpObjectifDate.Location = new System.Drawing.Point(83, 128);
            this.dtpObjectifDate.Name = "dtpObjectifDate";
            this.dtpObjectifDate.ShowCheckBox = true;
            this.dtpObjectifDate.Size = new System.Drawing.Size(192, 20);
            this.dtpObjectifDate.TabIndex = 27;
            // 
            // lblProjectNumber
            // 
            this.lblProjectNumber.AutoSize = true;
            this.lblProjectNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProjectNumber.Location = new System.Drawing.Point(3, 100);
            this.lblProjectNumber.Name = "lblProjectNumber";
            this.lblProjectNumber.Size = new System.Drawing.Size(74, 25);
            this.lblProjectNumber.TabIndex = 28;
            this.lblProjectNumber.Text = "N° projet";
            this.lblProjectNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboProjectNumber
            // 
            this.cboProjectNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboProjectNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProjectNumber.FormattingEnabled = true;
            this.cboProjectNumber.Location = new System.Drawing.Point(83, 103);
            this.cboProjectNumber.Name = "cboProjectNumber";
            this.cboProjectNumber.Size = new System.Drawing.Size(192, 21);
            this.cboProjectNumber.TabIndex = 29;
            // 
            // tlpPackage
            // 
            this.tlpPackage.ColumnCount = 2;
            this.tlpPackage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPackage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpPackage.Controls.Add(this.cboPackage, 0, 0);
            this.tlpPackage.Controls.Add(this.cmdDeletePackage, 1, 0);
            this.tlpPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPackage.Location = new System.Drawing.Point(80, 175);
            this.tlpPackage.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPackage.Name = "tlpPackage";
            this.tlpPackage.RowCount = 1;
            this.tlpPackage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPackage.Size = new System.Drawing.Size(198, 25);
            this.tlpPackage.TabIndex = 30;
            // 
            // cboPackage
            // 
            this.cboPackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPackage.FormattingEnabled = true;
            this.cboPackage.Location = new System.Drawing.Point(3, 3);
            this.cboPackage.Name = "cboPackage";
            this.cboPackage.Size = new System.Drawing.Size(167, 21);
            this.cboPackage.TabIndex = 11;
            // 
            // cmdDeletePackage
            // 
            this.cmdDeletePackage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDeletePackage.Image = global::EquinoxeExtendPlugin.Properties.Resources.delete_icone;
            this.cmdDeletePackage.Location = new System.Drawing.Point(173, 0);
            this.cmdDeletePackage.Margin = new System.Windows.Forms.Padding(0);
            this.cmdDeletePackage.Name = "cmdDeletePackage";
            this.cmdDeletePackage.Size = new System.Drawing.Size(25, 25);
            this.cmdDeletePackage.TabIndex = 12;
            this.cmdDeletePackage.UseVisualStyleBackColor = true;
            this.cmdDeletePackage.Click += new System.EventHandler(this.cmdDeletePackage_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(3, 25);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(74, 25);
            this.lblStatus.TabIndex = 31;
            this.lblStatus.Text = "Statut";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStatus
            // 
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatus.Location = new System.Drawing.Point(83, 28);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(192, 20);
            this.txtStatus.TabIndex = 32;
            // 
            // tlpFooter
            // 
            this.tlpFooter.ColumnCount = 3;
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpFooter.Controls.Add(this.cmdCancel, 2, 0);
            this.tlpFooter.Controls.Add(this.cmdOk, 1, 0);
            this.tlpFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFooter.Location = new System.Drawing.Point(0, 455);
            this.tlpFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tlpFooter.Name = "tlpFooter";
            this.tlpFooter.RowCount = 1;
            this.tlpFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFooter.Size = new System.Drawing.Size(354, 30);
            this.tlpFooter.TabIndex = 22;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdCancel.Location = new System.Drawing.Point(257, 3);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(94, 24);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(157, 3);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(94, 24);
            this.cmdOk.TabIndex = 1;
            this.cmdOk.Text = "OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tlpFooter, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(354, 485);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.pagGeneral);
            this.tabMain.Controls.Add(this.pagDescription);
            this.tabMain.Controls.Add(this.pagDates);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(3, 3);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(348, 449);
            this.tabMain.TabIndex = 0;
            // 
            // pagGeneral
            // 
            this.pagGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.pagGeneral.Controls.Add(this.tlpMain);
            this.pagGeneral.Location = new System.Drawing.Point(4, 22);
            this.pagGeneral.Name = "pagGeneral";
            this.pagGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.pagGeneral.Size = new System.Drawing.Size(340, 423);
            this.pagGeneral.TabIndex = 0;
            this.pagGeneral.Text = "Général";
            // 
            // pagDescription
            // 
            this.pagDescription.Controls.Add(this.tlpDescription);
            this.pagDescription.Location = new System.Drawing.Point(4, 22);
            this.pagDescription.Name = "pagDescription";
            this.pagDescription.Padding = new System.Windows.Forms.Padding(3);
            this.pagDescription.Size = new System.Drawing.Size(340, 423);
            this.pagDescription.TabIndex = 1;
            this.pagDescription.Text = "Commentaires";
            this.pagDescription.UseVisualStyleBackColor = true;
            // 
            // tlpDescription
            // 
            this.tlpDescription.ColumnCount = 3;
            this.tlpDescription.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpDescription.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDescription.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpDescription.Controls.Add(this.lblDescription, 0, 0);
            this.tlpDescription.Controls.Add(this.txtDescription, 0, 1);
            this.tlpDescription.Controls.Add(this.lblComments, 0, 2);
            this.tlpDescription.Controls.Add(this.txtComments, 0, 3);
            this.tlpDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDescription.Location = new System.Drawing.Point(3, 3);
            this.tlpDescription.Name = "tlpDescription";
            this.tlpDescription.RowCount = 4;
            this.tlpDescription.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDescription.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDescription.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDescription.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDescription.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDescription.Size = new System.Drawing.Size(334, 417);
            this.tlpDescription.TabIndex = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Location = new System.Drawing.Point(3, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(94, 20);
            this.lblDescription.TabIndex = 19;
            this.lblDescription.Text = "Description";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            this.tlpDescription.SetColumnSpan(this.txtDescription, 2);
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(3, 23);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(313, 182);
            this.txtDescription.TabIndex = 20;
            this.txtDescription.Text = "";
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblComments.Location = new System.Drawing.Point(3, 208);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(94, 20);
            this.lblComments.TabIndex = 21;
            this.lblComments.Text = "Commentaires";
            this.lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtComments
            // 
            this.tlpDescription.SetColumnSpan(this.txtComments, 2);
            this.txtComments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComments.Location = new System.Drawing.Point(3, 231);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(313, 183);
            this.txtComments.TabIndex = 22;
            this.txtComments.Text = "";
            // 
            // pagDates
            // 
            this.pagDates.Controls.Add(this.tlpDates);
            this.pagDates.Location = new System.Drawing.Point(4, 22);
            this.pagDates.Name = "pagDates";
            this.pagDates.Size = new System.Drawing.Size(340, 423);
            this.pagDates.TabIndex = 2;
            this.pagDates.Text = "Dates";
            this.pagDates.UseVisualStyleBackColor = true;
            // 
            // tlpDates
            // 
            this.tlpDates.ColumnCount = 2;
            this.tlpDates.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.64706F));
            this.tlpDates.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.35294F));
            this.tlpDates.Controls.Add(this.lblCreationDate, 0, 0);
            this.tlpDates.Controls.Add(this.lblOpenedDate, 0, 1);
            this.tlpDates.Controls.Add(this.lblCompletedDate, 0, 2);
            this.tlpDates.Controls.Add(this.txtCreationDate, 1, 0);
            this.tlpDates.Controls.Add(this.txtOpenedDate, 1, 1);
            this.tlpDates.Controls.Add(this.txtCompletedDate, 1, 2);
            this.tlpDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDates.Location = new System.Drawing.Point(0, 0);
            this.tlpDates.Name = "tlpDates";
            this.tlpDates.RowCount = 4;
            this.tlpDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDates.Size = new System.Drawing.Size(340, 423);
            this.tlpDates.TabIndex = 0;
            // 
            // lblCreationDate
            // 
            this.lblCreationDate.AutoSize = true;
            this.lblCreationDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreationDate.Location = new System.Drawing.Point(3, 0);
            this.lblCreationDate.Name = "lblCreationDate";
            this.lblCreationDate.Size = new System.Drawing.Size(88, 25);
            this.lblCreationDate.TabIndex = 0;
            this.lblCreationDate.Text = "Date création";
            this.lblCreationDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOpenedDate
            // 
            this.lblOpenedDate.AutoSize = true;
            this.lblOpenedDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOpenedDate.Location = new System.Drawing.Point(3, 25);
            this.lblOpenedDate.Name = "lblOpenedDate";
            this.lblOpenedDate.Size = new System.Drawing.Size(88, 25);
            this.lblOpenedDate.TabIndex = 1;
            this.lblOpenedDate.Text = "Date ouverture";
            this.lblOpenedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCompletedDate
            // 
            this.lblCompletedDate.AutoSize = true;
            this.lblCompletedDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCompletedDate.Location = new System.Drawing.Point(3, 50);
            this.lblCompletedDate.Name = "lblCompletedDate";
            this.lblCompletedDate.Size = new System.Drawing.Size(88, 25);
            this.lblCompletedDate.TabIndex = 2;
            this.lblCompletedDate.Text = "Date clôture";
            this.lblCompletedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCreationDate
            // 
            this.txtCreationDate.Location = new System.Drawing.Point(97, 3);
            this.txtCreationDate.Name = "txtCreationDate";
            this.txtCreationDate.ReadOnly = true;
            this.txtCreationDate.Size = new System.Drawing.Size(100, 20);
            this.txtCreationDate.TabIndex = 3;
            // 
            // txtOpenedDate
            // 
            this.txtOpenedDate.Location = new System.Drawing.Point(97, 28);
            this.txtOpenedDate.Name = "txtOpenedDate";
            this.txtOpenedDate.ReadOnly = true;
            this.txtOpenedDate.Size = new System.Drawing.Size(100, 20);
            this.txtOpenedDate.TabIndex = 4;
            // 
            // txtCompletedDate
            // 
            this.txtCompletedDate.Location = new System.Drawing.Point(97, 53);
            this.txtCompletedDate.Name = "txtCompletedDate";
            this.txtCompletedDate.ReadOnly = true;
            this.txtCompletedDate.Size = new System.Drawing.Size(100, 20);
            this.txtCompletedDate.TabIndex = 5;
            // 
            // ucMainTaskEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ucMainTaskEdit";
            this.Size = new System.Drawing.Size(354, 485);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPriority)).EndInit();
            this.tlpPackage.ResumeLayout(false);
            this.tlpFooter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.pagGeneral.ResumeLayout(false);
            this.pagDescription.ResumeLayout(false);
            this.tlpDescription.ResumeLayout(false);
            this.tlpDescription.PerformLayout();
            this.pagDates.ResumeLayout(false);
            this.tlpDates.ResumeLayout(false);
            this.tlpDates.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblMainTaskId;
        private System.Windows.Forms.TextBox txtMainTaskId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblTaskType;
        private System.Windows.Forms.Label lblPriority;
        private Library.Control.UserControls.ucNumericUpDown numPriority;
        private System.Windows.Forms.Label lblPackage;
        private System.Windows.Forms.ComboBox cboPackage;
        private System.Windows.Forms.Label lblCreationUser;
        private System.Windows.Forms.Label lblRequestUser;
        private System.Windows.Forms.ComboBox cboRequestUser;
        private System.Windows.Forms.ComboBox cboCreationUser;
        private System.Windows.Forms.TableLayoutPanel tlpFooter;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox cboTaskType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage pagGeneral;
        private System.Windows.Forms.TabPage pagDescription;
        private System.Windows.Forms.TableLayoutPanel tlpDescription;
        private System.Windows.Forms.Label lblProductLines;
        private System.Windows.Forms.CheckedListBox chlProductLines;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.RichTextBox txtDescription;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.RichTextBox txtComments;
        private System.Windows.Forms.Label lblObjectifDate;
        private System.Windows.Forms.DateTimePicker dtpObjectifDate;
        private System.Windows.Forms.TabPage pagDates;
        private System.Windows.Forms.TableLayoutPanel tlpDates;
        private System.Windows.Forms.Label lblCreationDate;
        private System.Windows.Forms.Label lblOpenedDate;
        private System.Windows.Forms.Label lblCompletedDate;
        private System.Windows.Forms.TextBox txtCreationDate;
        private System.Windows.Forms.TextBox txtOpenedDate;
        private System.Windows.Forms.TextBox txtCompletedDate;
        private System.Windows.Forms.Label lblProjectNumber;
        private System.Windows.Forms.ComboBox cboProjectNumber;
        private System.Windows.Forms.TableLayoutPanel tlpPackage;
        private System.Windows.Forms.Button cmdDeletePackage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtStatus;
    }
}

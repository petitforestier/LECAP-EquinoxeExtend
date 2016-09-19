namespace EquinoxeExtendPlugin.Controls.ReleaseManagement
{
    partial class ucTaskManager
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
            this.fraMainTasks = new System.Windows.Forms.GroupBox();
            this.tlpMainTask = new System.Windows.Forms.TableLayoutPanel();
            this.fraMainTask = new System.Windows.Forms.GroupBox();
            this.fraProjectTasks = new System.Windows.Forms.GroupBox();
            this.tlpProjectTask = new System.Windows.Forms.TableLayoutPanel();
            this.fraProjectTask = new System.Windows.Forms.GroupBox();
            this.tlpHeader = new System.Windows.Forms.TableLayoutPanel();
            this.fraCriteriaSearch = new System.Windows.Forms.GroupBox();
            this.tlpCriteriaSearch = new System.Windows.Forms.TableLayoutPanel();
            this.lblMainTaskStatusSearch = new System.Windows.Forms.Label();
            this.cboMainTaskStatusSearch = new System.Windows.Forms.ComboBox();
            this.lblOrderBy = new System.Windows.Forms.Label();
            this.cboOrderBy = new System.Windows.Forms.ComboBox();
            this.lblProject = new System.Windows.Forms.Label();
            this.cboProject = new System.Windows.Forms.ComboBox();
            this.lblProductLine = new System.Windows.Forms.Label();
            this.cboProductLine = new System.Windows.Forms.ComboBox();
            this.cboMainTaskType = new System.Windows.Forms.ComboBox();
            this.lblMainTaskType = new System.Windows.Forms.Label();
            this.cmdCriteriaSearch = new System.Windows.Forms.Button();
            this.lblDeveloppeur = new System.Windows.Forms.Label();
            this.cboDevelopper = new System.Windows.Forms.ComboBox();
            this.fraMainTaskSearch = new System.Windows.Forms.GroupBox();
            this.tlpMainTaskSearch = new System.Windows.Forms.TableLayoutPanel();
            this.lblMainTaskId = new System.Windows.Forms.Label();
            this.cmdMainTaskSearch = new System.Windows.Forms.Button();
            this.txtMaintaskId = new Library.Control.UserControls.ucTextBox();
            this.ucMainTaskManager = new EquinoxeExtendPlugin.Controls.Task.ucMainTaskManager();
            this.ucMainTaskEdit = new EquinoxeExtendPlugin.Controls.Task.ucMainTaskEdit();
            this.ucSubTaskManager = new EquinoxeExtendPlugin.Controls.Task.ucSubTaskManager();
            this.ucSubTaskEdit = new EquinoxeExtendPlugin.Controls.Task.ucSubTaskEdit();
            this.tlpMain.SuspendLayout();
            this.fraMainTasks.SuspendLayout();
            this.tlpMainTask.SuspendLayout();
            this.fraMainTask.SuspendLayout();
            this.fraProjectTasks.SuspendLayout();
            this.tlpProjectTask.SuspendLayout();
            this.fraProjectTask.SuspendLayout();
            this.tlpHeader.SuspendLayout();
            this.fraCriteriaSearch.SuspendLayout();
            this.tlpCriteriaSearch.SuspendLayout();
            this.fraMainTaskSearch.SuspendLayout();
            this.tlpMainTaskSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.fraMainTasks, 0, 1);
            this.tlpMain.Controls.Add(this.fraProjectTasks, 0, 2);
            this.tlpMain.Controls.Add(this.tlpHeader, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpMain.Size = new System.Drawing.Size(1600, 900);
            this.tlpMain.TabIndex = 0;
            // 
            // fraMainTasks
            // 
            this.fraMainTasks.Controls.Add(this.tlpMainTask);
            this.fraMainTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraMainTasks.Location = new System.Drawing.Point(3, 93);
            this.fraMainTasks.Name = "fraMainTasks";
            this.fraMainTasks.Size = new System.Drawing.Size(1594, 504);
            this.fraMainTasks.TabIndex = 5;
            this.fraMainTasks.TabStop = false;
            this.fraMainTasks.Text = "Tâches";
            // 
            // tlpMainTask
            // 
            this.tlpMainTask.ColumnCount = 2;
            this.tlpMainTask.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainTask.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 364F));
            this.tlpMainTask.Controls.Add(this.ucMainTaskManager, 0, 0);
            this.tlpMainTask.Controls.Add(this.fraMainTask, 1, 0);
            this.tlpMainTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainTask.Location = new System.Drawing.Point(3, 16);
            this.tlpMainTask.Name = "tlpMainTask";
            this.tlpMainTask.RowCount = 1;
            this.tlpMainTask.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainTask.Size = new System.Drawing.Size(1588, 485);
            this.tlpMainTask.TabIndex = 0;
            // 
            // fraMainTask
            // 
            this.fraMainTask.Controls.Add(this.ucMainTaskEdit);
            this.fraMainTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraMainTask.Location = new System.Drawing.Point(1227, 3);
            this.fraMainTask.Name = "fraMainTask";
            this.fraMainTask.Size = new System.Drawing.Size(358, 479);
            this.fraMainTask.TabIndex = 4;
            this.fraMainTask.TabStop = false;
            this.fraMainTask.Text = "Tâche";
            // 
            // fraProjectTasks
            // 
            this.fraProjectTasks.Controls.Add(this.tlpProjectTask);
            this.fraProjectTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraProjectTasks.Location = new System.Drawing.Point(3, 603);
            this.fraProjectTasks.Name = "fraProjectTasks";
            this.fraProjectTasks.Size = new System.Drawing.Size(1594, 294);
            this.fraProjectTasks.TabIndex = 6;
            this.fraProjectTasks.TabStop = false;
            this.fraProjectTasks.Text = "Sous tâches";
            // 
            // tlpProjectTask
            // 
            this.tlpProjectTask.ColumnCount = 2;
            this.tlpProjectTask.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProjectTask.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 364F));
            this.tlpProjectTask.Controls.Add(this.ucSubTaskManager, 0, 0);
            this.tlpProjectTask.Controls.Add(this.fraProjectTask, 1, 0);
            this.tlpProjectTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProjectTask.Location = new System.Drawing.Point(3, 16);
            this.tlpProjectTask.Name = "tlpProjectTask";
            this.tlpProjectTask.RowCount = 1;
            this.tlpProjectTask.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProjectTask.Size = new System.Drawing.Size(1588, 275);
            this.tlpProjectTask.TabIndex = 0;
            // 
            // fraProjectTask
            // 
            this.fraProjectTask.BackColor = System.Drawing.SystemColors.Control;
            this.fraProjectTask.Controls.Add(this.ucSubTaskEdit);
            this.fraProjectTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraProjectTask.Location = new System.Drawing.Point(1227, 3);
            this.fraProjectTask.Name = "fraProjectTask";
            this.fraProjectTask.Size = new System.Drawing.Size(358, 269);
            this.fraProjectTask.TabIndex = 4;
            this.fraProjectTask.TabStop = false;
            this.fraProjectTask.Text = "Sous tâche";
            // 
            // tlpHeader
            // 
            this.tlpHeader.ColumnCount = 2;
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.29392F));
            this.tlpHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.70608F));
            this.tlpHeader.Controls.Add(this.fraCriteriaSearch, 1, 0);
            this.tlpHeader.Controls.Add(this.fraMainTaskSearch, 0, 0);
            this.tlpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHeader.Location = new System.Drawing.Point(3, 3);
            this.tlpHeader.Name = "tlpHeader";
            this.tlpHeader.RowCount = 1;
            this.tlpHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHeader.Size = new System.Drawing.Size(1594, 84);
            this.tlpHeader.TabIndex = 8;
            // 
            // fraCriteriaSearch
            // 
            this.fraCriteriaSearch.Controls.Add(this.tlpCriteriaSearch);
            this.fraCriteriaSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraCriteriaSearch.Location = new System.Drawing.Point(310, 3);
            this.fraCriteriaSearch.Name = "fraCriteriaSearch";
            this.fraCriteriaSearch.Size = new System.Drawing.Size(1281, 78);
            this.fraCriteriaSearch.TabIndex = 7;
            this.fraCriteriaSearch.TabStop = false;
            this.fraCriteriaSearch.Text = "Rechercher tâche(s) par critères";
            // 
            // tlpCriteriaSearch
            // 
            this.tlpCriteriaSearch.ColumnCount = 8;
            this.tlpCriteriaSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tlpCriteriaSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tlpCriteriaSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tlpCriteriaSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.tlpCriteriaSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tlpCriteriaSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 191F));
            this.tlpCriteriaSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tlpCriteriaSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCriteriaSearch.Controls.Add(this.lblMainTaskStatusSearch, 0, 0);
            this.tlpCriteriaSearch.Controls.Add(this.cboMainTaskStatusSearch, 1, 0);
            this.tlpCriteriaSearch.Controls.Add(this.lblOrderBy, 0, 1);
            this.tlpCriteriaSearch.Controls.Add(this.cboOrderBy, 1, 1);
            this.tlpCriteriaSearch.Controls.Add(this.lblProject, 2, 0);
            this.tlpCriteriaSearch.Controls.Add(this.cboProject, 3, 0);
            this.tlpCriteriaSearch.Controls.Add(this.lblProductLine, 2, 1);
            this.tlpCriteriaSearch.Controls.Add(this.cboProductLine, 3, 1);
            this.tlpCriteriaSearch.Controls.Add(this.cboMainTaskType, 5, 0);
            this.tlpCriteriaSearch.Controls.Add(this.lblMainTaskType, 4, 0);
            this.tlpCriteriaSearch.Controls.Add(this.cmdCriteriaSearch, 6, 1);
            this.tlpCriteriaSearch.Controls.Add(this.lblDeveloppeur, 4, 1);
            this.tlpCriteriaSearch.Controls.Add(this.cboDevelopper, 5, 1);
            this.tlpCriteriaSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCriteriaSearch.Location = new System.Drawing.Point(3, 16);
            this.tlpCriteriaSearch.Name = "tlpCriteriaSearch";
            this.tlpCriteriaSearch.RowCount = 2;
            this.tlpCriteriaSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpCriteriaSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpCriteriaSearch.Size = new System.Drawing.Size(1275, 59);
            this.tlpCriteriaSearch.TabIndex = 2;
            // 
            // lblMainTaskStatusSearch
            // 
            this.lblMainTaskStatusSearch.AutoSize = true;
            this.lblMainTaskStatusSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMainTaskStatusSearch.Location = new System.Drawing.Point(3, 0);
            this.lblMainTaskStatusSearch.Name = "lblMainTaskStatusSearch";
            this.lblMainTaskStatusSearch.Size = new System.Drawing.Size(65, 30);
            this.lblMainTaskStatusSearch.TabIndex = 0;
            this.lblMainTaskStatusSearch.Text = "Statut";
            this.lblMainTaskStatusSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMainTaskStatusSearch
            // 
            this.cboMainTaskStatusSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMainTaskStatusSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMainTaskStatusSearch.FormattingEnabled = true;
            this.cboMainTaskStatusSearch.Location = new System.Drawing.Point(74, 3);
            this.cboMainTaskStatusSearch.Name = "cboMainTaskStatusSearch";
            this.cboMainTaskStatusSearch.Size = new System.Drawing.Size(165, 21);
            this.cboMainTaskStatusSearch.TabIndex = 2;
            // 
            // lblOrderBy
            // 
            this.lblOrderBy.AutoSize = true;
            this.lblOrderBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderBy.Location = new System.Drawing.Point(3, 30);
            this.lblOrderBy.Name = "lblOrderBy";
            this.lblOrderBy.Size = new System.Drawing.Size(65, 29);
            this.lblOrderBy.TabIndex = 4;
            this.lblOrderBy.Text = "Ranger par";
            this.lblOrderBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboOrderBy
            // 
            this.cboOrderBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrderBy.FormattingEnabled = true;
            this.cboOrderBy.Location = new System.Drawing.Point(74, 33);
            this.cboOrderBy.Name = "cboOrderBy";
            this.cboOrderBy.Size = new System.Drawing.Size(165, 21);
            this.cboOrderBy.TabIndex = 5;
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProject.Location = new System.Drawing.Point(245, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(45, 30);
            this.lblProject.TabIndex = 6;
            this.lblProject.Text = "Projet";
            this.lblProject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboProject
            // 
            this.cboProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProject.FormattingEnabled = true;
            this.cboProject.Location = new System.Drawing.Point(296, 3);
            this.cboProject.Name = "cboProject";
            this.cboProject.Size = new System.Drawing.Size(231, 21);
            this.cboProject.TabIndex = 7;
            this.cboProject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboProject_KeyDown);
            // 
            // lblProductLine
            // 
            this.lblProductLine.AutoSize = true;
            this.lblProductLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductLine.Location = new System.Drawing.Point(245, 30);
            this.lblProductLine.Name = "lblProductLine";
            this.lblProductLine.Size = new System.Drawing.Size(45, 29);
            this.lblProductLine.TabIndex = 8;
            this.lblProductLine.Text = "Gamme";
            this.lblProductLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboProductLine
            // 
            this.cboProductLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboProductLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductLine.FormattingEnabled = true;
            this.cboProductLine.Location = new System.Drawing.Point(296, 33);
            this.cboProductLine.Name = "cboProductLine";
            this.cboProductLine.Size = new System.Drawing.Size(231, 21);
            this.cboProductLine.TabIndex = 9;
            this.cboProductLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboProductLine_KeyDown);
            // 
            // cboMainTaskType
            // 
            this.cboMainTaskType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMainTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMainTaskType.FormattingEnabled = true;
            this.cboMainTaskType.Location = new System.Drawing.Point(608, 3);
            this.cboMainTaskType.Name = "cboMainTaskType";
            this.cboMainTaskType.Size = new System.Drawing.Size(185, 21);
            this.cboMainTaskType.TabIndex = 10;
            this.cboMainTaskType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboMainTaskType_KeyDown);
            // 
            // lblMainTaskType
            // 
            this.lblMainTaskType.AutoSize = true;
            this.lblMainTaskType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMainTaskType.Location = new System.Drawing.Point(533, 0);
            this.lblMainTaskType.Name = "lblMainTaskType";
            this.lblMainTaskType.Size = new System.Drawing.Size(69, 30);
            this.lblMainTaskType.TabIndex = 11;
            this.lblMainTaskType.Text = "Type";
            this.lblMainTaskType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdCriteriaSearch
            // 
            this.cmdCriteriaSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCriteriaSearch.Location = new System.Drawing.Point(817, 33);
            this.cmdCriteriaSearch.Name = "cmdCriteriaSearch";
            this.cmdCriteriaSearch.Size = new System.Drawing.Size(74, 23);
            this.cmdCriteriaSearch.TabIndex = 3;
            this.cmdCriteriaSearch.Text = "Rechercher";
            this.cmdCriteriaSearch.UseVisualStyleBackColor = true;
            this.cmdCriteriaSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // lblDeveloppeur
            // 
            this.lblDeveloppeur.AutoSize = true;
            this.lblDeveloppeur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDeveloppeur.Location = new System.Drawing.Point(533, 30);
            this.lblDeveloppeur.Name = "lblDeveloppeur";
            this.lblDeveloppeur.Size = new System.Drawing.Size(69, 29);
            this.lblDeveloppeur.TabIndex = 12;
            this.lblDeveloppeur.Text = "Développeur";
            this.lblDeveloppeur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDevelopper
            // 
            this.cboDevelopper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDevelopper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDevelopper.FormattingEnabled = true;
            this.cboDevelopper.Location = new System.Drawing.Point(608, 33);
            this.cboDevelopper.Name = "cboDevelopper";
            this.cboDevelopper.Size = new System.Drawing.Size(185, 21);
            this.cboDevelopper.TabIndex = 13;
            this.cboDevelopper.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboDevelopper_KeyDown);
            // 
            // fraMainTaskSearch
            // 
            this.fraMainTaskSearch.Controls.Add(this.tlpMainTaskSearch);
            this.fraMainTaskSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraMainTaskSearch.Location = new System.Drawing.Point(3, 3);
            this.fraMainTaskSearch.Name = "fraMainTaskSearch";
            this.fraMainTaskSearch.Size = new System.Drawing.Size(301, 78);
            this.fraMainTaskSearch.TabIndex = 8;
            this.fraMainTaskSearch.TabStop = false;
            this.fraMainTaskSearch.Text = "Rechercher une tâche";
            // 
            // tlpMainTaskSearch
            // 
            this.tlpMainTaskSearch.ColumnCount = 2;
            this.tlpMainTaskSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tlpMainTaskSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainTaskSearch.Controls.Add(this.lblMainTaskId, 0, 0);
            this.tlpMainTaskSearch.Controls.Add(this.cmdMainTaskSearch, 1, 1);
            this.tlpMainTaskSearch.Controls.Add(this.txtMaintaskId, 1, 0);
            this.tlpMainTaskSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainTaskSearch.Location = new System.Drawing.Point(3, 16);
            this.tlpMainTaskSearch.Name = "tlpMainTaskSearch";
            this.tlpMainTaskSearch.RowCount = 2;
            this.tlpMainTaskSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.84746F));
            this.tlpMainTaskSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.15254F));
            this.tlpMainTaskSearch.Size = new System.Drawing.Size(295, 59);
            this.tlpMainTaskSearch.TabIndex = 0;
            // 
            // lblMainTaskId
            // 
            this.lblMainTaskId.AutoSize = true;
            this.lblMainTaskId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMainTaskId.Location = new System.Drawing.Point(3, 0);
            this.lblMainTaskId.Name = "lblMainTaskId";
            this.lblMainTaskId.Size = new System.Drawing.Size(57, 30);
            this.lblMainTaskId.TabIndex = 0;
            this.lblMainTaskId.Text = "N° Tâche";
            this.lblMainTaskId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdMainTaskSearch
            // 
            this.cmdMainTaskSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMainTaskSearch.Location = new System.Drawing.Point(218, 33);
            this.cmdMainTaskSearch.Name = "cmdMainTaskSearch";
            this.cmdMainTaskSearch.Size = new System.Drawing.Size(74, 23);
            this.cmdMainTaskSearch.TabIndex = 4;
            this.cmdMainTaskSearch.Text = "Rechercher";
            this.cmdMainTaskSearch.UseVisualStyleBackColor = true;
            this.cmdMainTaskSearch.Click += new System.EventHandler(this.cmdMainTaskSearch_Click);
            // 
            // txtMaintaskId
            // 
            this.txtMaintaskId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaintaskId.Location = new System.Drawing.Point(66, 3);
            this.txtMaintaskId.Name = "txtMaintaskId";
            this.txtMaintaskId.Size = new System.Drawing.Size(226, 20);
            this.txtMaintaskId.TabIndex = 5;
            this.txtMaintaskId.ValueType = Library.Control.UserControls.ucTextBox.TextBoxTypeEnum.IntegerPositiveOnly;
            this.txtMaintaskId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaintaskId_KeyDown);
            // 
            // ucMainTaskManager
            // 
            this.ucMainTaskManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMainTaskManager.Location = new System.Drawing.Point(3, 3);
            this.ucMainTaskManager.Name = "ucMainTaskManager";
            this.ucMainTaskManager.Size = new System.Drawing.Size(1218, 479);
            this.ucMainTaskManager.TabIndex = 4;
            // 
            // ucMainTaskEdit
            // 
            this.ucMainTaskEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMainTaskEdit.Location = new System.Drawing.Point(3, 16);
            this.ucMainTaskEdit.Name = "ucMainTaskEdit";
            this.ucMainTaskEdit.Size = new System.Drawing.Size(352, 460);
            this.ucMainTaskEdit.TabIndex = 3;
            // 
            // ucSubTaskManager
            // 
            this.ucSubTaskManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSubTaskManager.Location = new System.Drawing.Point(3, 3);
            this.ucSubTaskManager.Name = "ucSubTaskManager";
            this.ucSubTaskManager.Size = new System.Drawing.Size(1218, 269);
            this.ucSubTaskManager.TabIndex = 3;
            // 
            // ucSubTaskEdit
            // 
            this.ucSubTaskEdit.BackColor = System.Drawing.SystemColors.Control;
            this.ucSubTaskEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSubTaskEdit.Location = new System.Drawing.Point(3, 16);
            this.ucSubTaskEdit.Name = "ucSubTaskEdit";
            this.ucSubTaskEdit.Size = new System.Drawing.Size(352, 250);
            this.ucSubTaskEdit.TabIndex = 4;
            // 
            // ucTaskManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "ucTaskManager";
            this.Size = new System.Drawing.Size(1600, 900);
            this.tlpMain.ResumeLayout(false);
            this.fraMainTasks.ResumeLayout(false);
            this.tlpMainTask.ResumeLayout(false);
            this.fraMainTask.ResumeLayout(false);
            this.fraProjectTasks.ResumeLayout(false);
            this.tlpProjectTask.ResumeLayout(false);
            this.fraProjectTask.ResumeLayout(false);
            this.tlpHeader.ResumeLayout(false);
            this.fraCriteriaSearch.ResumeLayout(false);
            this.tlpCriteriaSearch.ResumeLayout(false);
            this.tlpCriteriaSearch.PerformLayout();
            this.fraMainTaskSearch.ResumeLayout(false);
            this.tlpMainTaskSearch.ResumeLayout(false);
            this.tlpMainTaskSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpCriteriaSearch;
        private System.Windows.Forms.Label lblMainTaskStatusSearch;
        private System.Windows.Forms.ComboBox cboMainTaskStatusSearch;
        private Task.ucMainTaskEdit ucMainTaskEdit;
        private Task.ucSubTaskManager ucSubTaskManager;
        private Task.ucMainTaskManager ucMainTaskManager;
        private System.Windows.Forms.GroupBox fraMainTasks;
        private System.Windows.Forms.TableLayoutPanel tlpMainTask;
        private System.Windows.Forms.GroupBox fraProjectTasks;
        private System.Windows.Forms.TableLayoutPanel tlpProjectTask;
        private Task.ucSubTaskEdit ucSubTaskEdit;
        private System.Windows.Forms.GroupBox fraCriteriaSearch;
        private System.Windows.Forms.Button cmdCriteriaSearch;
        private System.Windows.Forms.GroupBox fraMainTask;
        private System.Windows.Forms.GroupBox fraProjectTask;
        private System.Windows.Forms.Label lblOrderBy;
        private System.Windows.Forms.ComboBox cboOrderBy;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.ComboBox cboProject;
        private System.Windows.Forms.Label lblProductLine;
        private System.Windows.Forms.ComboBox cboProductLine;
        private System.Windows.Forms.ComboBox cboMainTaskType;
        private System.Windows.Forms.Label lblMainTaskType;
        private System.Windows.Forms.TableLayoutPanel tlpHeader;
        private System.Windows.Forms.GroupBox fraMainTaskSearch;
        private System.Windows.Forms.TableLayoutPanel tlpMainTaskSearch;
        private System.Windows.Forms.Label lblMainTaskId;
        private System.Windows.Forms.Button cmdMainTaskSearch;
        private Library.Control.UserControls.ucTextBox txtMaintaskId;
        private System.Windows.Forms.Label lblDeveloppeur;
        private System.Windows.Forms.ComboBox cboDevelopper;
    }
}

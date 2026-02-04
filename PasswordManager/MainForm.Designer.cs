namespace PasswordManager
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.treeCategories = new System.Windows.Forms.TreeView();
            this.btnAddCat = new System.Windows.Forms.Button();
            this.gridEntries = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCopyPass = new System.Windows.Forms.ToolStripButton();
            this.btnCopyLogin = new System.Windows.Forms.ToolStripButton();
            this.btnShowPass = new System.Windows.Forms.ToolStripButton();
            this.sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEntries)).BeginInit();
            this.panelTop.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
           
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 42);
            this.splitMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitMain.Name = "splitMain";
           
            this.splitMain.Panel1.Controls.Add(this.treeCategories);
            this.splitMain.Panel1.Controls.Add(this.btnAddCat);
          
            this.splitMain.Panel2.Controls.Add(this.gridEntries);
            this.splitMain.Panel2.Controls.Add(this.panelTop);
            this.splitMain.Size = new System.Drawing.Size(1333, 712);
            this.splitMain.SplitterDistance = 333;
            this.splitMain.SplitterWidth = 7;
            this.splitMain.TabIndex = 0;
           
            this.treeCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCategories.Location = new System.Drawing.Point(0, 0);
            this.treeCategories.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeCategories.Name = "treeCategories";
            this.treeCategories.Size = new System.Drawing.Size(333, 635);
            this.treeCategories.TabIndex = 0;
            this.treeCategories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCategories_AfterSelect);
            
            this.btnAddCat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddCat.Location = new System.Drawing.Point(0, 635);
            this.btnAddCat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddCat.Name = "btnAddCat";
            this.btnAddCat.Size = new System.Drawing.Size(333, 77);
            this.btnAddCat.TabIndex = 1;
            this.btnAddCat.Text = "Добавить категорию";
            this.btnAddCat.UseVisualStyleBackColor = true;
            this.btnAddCat.Click += new System.EventHandler(this.btnAddCat_Click);
            
            this.gridEntries.AllowUserToAddRows = false;
            this.gridEntries.AllowUserToDeleteRows = false;
            this.gridEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEntries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colName,
            this.colCategory,
            this.colLogin,
            this.colPassword,
            this.colNotes});
            this.gridEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEntries.Location = new System.Drawing.Point(0, 62);
            this.gridEntries.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridEntries.MultiSelect = false;
            this.gridEntries.Name = "gridEntries";
            this.gridEntries.ReadOnly = true;
            this.gridEntries.RowHeadersVisible = false;
            this.gridEntries.RowHeadersWidth = 51;
            this.gridEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridEntries.Size = new System.Drawing.Size(993, 650);
            this.gridEntries.TabIndex = 0;
            this.gridEntries.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridEntries_CellDoubleClick);
            this.gridEntries.SelectionChanged += new System.EventHandler(this.gridEntries_SelectionChanged);
            
            this.colId.DataPropertyName = "Id";
            this.colId.HeaderText = "ID";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
           
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Название";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
          
            this.colCategory.DataPropertyName = "Category";
            this.colCategory.HeaderText = "Категория";
            this.colCategory.MinimumWidth = 6;
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
           
            this.colLogin.DataPropertyName = "Login";
            this.colLogin.HeaderText = "Логин";
            this.colLogin.MinimumWidth = 6;
            this.colLogin.Name = "colLogin";
            this.colLogin.ReadOnly = true;
         
            this.colPassword.DataPropertyName = "Password";
            this.colPassword.HeaderText = "Пароль";
            this.colPassword.MinimumWidth = 6;
            this.colPassword.Name = "colPassword";
            this.colPassword.ReadOnly = true;
           
            this.colNotes.DataPropertyName = "Notes";
            this.colNotes.HeaderText = "Заметки";
            this.colNotes.MinimumWidth = 6;
            this.colNotes.Name = "colNotes";
            this.colNotes.ReadOnly = true;
            
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.lblSearch);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(993, 62);
            this.panelTop.TabIndex = 1;
            
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(750, 17);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(225, 31);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(87, 19);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(645, 22);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(13, 22);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(52, 16);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Поиск:";
            
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnEdit,
            this.btnDelete,
            this.sep1,
            this.btnCopyPass,
            this.btnCopyLogin,
            this.btnShowPass,
            this.sep2,
            this.btnExport});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip.Size = new System.Drawing.Size(1333, 42);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(91, 39);
            this.btnAdd.Text = "Добавить";
            this.btnAdd.ToolTipText = "Добавить новую запись";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEdit.Enabled = false;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(93, 39);
            this.btnEdit.Text = "Изменить";
            this.btnEdit.ToolTipText = "Изменить выбранную запись";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
           
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDelete.Enabled = false;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(77, 39);
            this.btnDelete.Text = "Удалить";
            this.btnDelete.ToolTipText = "Удалить выбранную запись";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
         
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(6, 42);
            
            this.btnCopyPass.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCopyPass.Enabled = false;
            this.btnCopyPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCopyPass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopyPass.Name = "btnCopyPass";
            this.btnCopyPass.Size = new System.Drawing.Size(175, 39);
            this.btnCopyPass.Text = "Копировать пароль";
            this.btnCopyPass.ToolTipText = "Копировать пароль выбранной записи";
            this.btnCopyPass.Click += new System.EventHandler(this.btnCopyPass_Click);
          
            this.btnCopyLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCopyLogin.Enabled = false;
            this.btnCopyLogin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCopyLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopyLogin.Name = "btnCopyLogin";
            this.btnCopyLogin.Size = new System.Drawing.Size(164, 39);
            this.btnCopyLogin.Text = "Копировать логин";
            this.btnCopyLogin.ToolTipText = "Копировать логин выбранной записи";
            this.btnCopyLogin.Click += new System.EventHandler(this.btnCopyLogin_Click);
           
            this.btnShowPass.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnShowPass.Enabled = false;
            this.btnShowPass.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnShowPass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowPass.Name = "btnShowPass";
            this.btnShowPass.Size = new System.Drawing.Size(149, 39);
            this.btnShowPass.Text = "Показать пароль";
            this.btnShowPass.ToolTipText = "Показать/скрыть пароль выбранной записи";
            this.btnShowPass.Click += new System.EventHandler(this.btnShowPass_Click);
           
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(6, 42);
           
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(73, 39);
            this.btnExport.Text = "Экспорт";
            this.btnExport.ToolTipText = "Экспортировать данные";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
           
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusBar.Location = new System.Drawing.Point(0, 754);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusBar.Size = new System.Drawing.Size(1333, 26);
            this.statusBar.TabIndex = 2;
          
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(69, 20);
            this.lblStatus.Text = "Готово...";
           
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 780);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusBar);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Менеджер паролей";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridEntries)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TreeView treeCategories;
        private System.Windows.Forms.Button btnAddCat;
        private System.Windows.Forms.DataGridView gridEntries;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripButton btnCopyPass;
        private System.Windows.Forms.ToolStripButton btnCopyLogin;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLogin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ToolStripButton btnShowPass;
        private System.Windows.Forms.ToolStripSeparator sep2;
        private System.Windows.Forms.ToolStripButton btnExport;
    }
}
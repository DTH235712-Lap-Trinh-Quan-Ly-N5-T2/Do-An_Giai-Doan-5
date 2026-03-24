namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmProjects
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop        = new Panel();
            this.lblHeader       = new Label();
            this.panelFilter     = new Panel();
            this.txtSearch       = new TextBox();
            this.cboFilterStatus = new ComboBox();
            this.btnRefresh      = new Button();
            this.panelToolbar    = new Panel();
            this.btnAdd          = new Button();
            this.btnEdit         = new Button();
            this.btnDelete       = new Button();
            this.btnStatus       = new Button();
            this.btnMembers      = new Button();
            this.btnDetail       = new Button();
            this.lblCount        = new Label();
            this.dgvProjects     = new DataGridView();
            this.panelStatus     = new Panel();
            this.lblStatus       = new Label();

            this.colId        = new DataGridViewTextBoxColumn();
            this.colName      = new DataGridViewTextBoxColumn();
            this.colCustomer  = new DataGridViewTextBoxColumn();
            this.colOwner     = new DataGridViewTextBoxColumn();
            this.colProjStatus = new DataGridViewTextBoxColumn();
            this.colMembers   = new DataGridViewTextBoxColumn();
            this.colDeadline  = new DataGridViewTextBoxColumn();
            this.colBudget    = new DataGridViewTextBoxColumn();
            this.colStartDate = new DataGridViewTextBoxColumn();

            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dgvProjects).BeginInit();
            this.SuspendLayout();

            // ── Header ───────────────────────────────────────
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 52;
            this.panelTop.Controls.Add(this.lblHeader);

            this.lblHeader.AutoSize = false;
            this.lblHeader.Dock = DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Text = "  📁  Quản lý Dự án";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── Filter bar ───────────────────────────────────
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelFilter.Dock = DockStyle.Top;
            this.panelFilter.Height = 50;
            this.panelFilter.Controls.AddRange(new Control[] { txtSearch, cboFilterStatus, btnRefresh });

            this.txtSearch.BorderStyle = BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(14, 12);
            this.txtSearch.PlaceholderText = "🔍  Tìm theo tên dự án, khách hàng, PM...";
            this.txtSearch.Size = new System.Drawing.Size(340, 30);
            this.txtSearch.TextChanged += txtSearch_TextChanged;

            this.cboFilterStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboFilterStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboFilterStatus.Location = new System.Drawing.Point(364, 12);
            this.cboFilterStatus.Size = new System.Drawing.Size(160, 30);
            this.cboFilterStatus.Items.AddRange(new object[] { "Tất cả trạng thái", "NotStarted", "InProgress", "OnHold", "Completed", "Cancelled" });
            this.cboFilterStatus.SelectedIndex = 0;
            this.cboFilterStatus.SelectedIndexChanged += cboFilterStatus_SelectedIndexChanged;

            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI Emoji", 11F);
            this.btnRefresh.Location = new System.Drawing.Point(534, 12);
            this.btnRefresh.Size = new System.Drawing.Size(38, 30);
            this.btnRefresh.Text = "🔄";
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.Click += btnRefresh_Click;

            // ── Toolbar ──────────────────────────────────────
            this.panelToolbar.BackColor = System.Drawing.Color.White;
            this.panelToolbar.Dock = DockStyle.Top;
            this.panelToolbar.Height = 50;
            this.panelToolbar.Controls.AddRange(new Control[]
            { btnAdd, btnEdit, btnDelete, btnStatus, btnMembers, btnDetail, lblCount });

            int bx = 14; int bw = 110; int bg = 6; int bh = 34; int by = 9;

            // Thêm mới
            SetToolButton(btnAdd, "➕ Thêm mới", System.Drawing.Color.FromArgb(37, 99, 235), bx, by, bw, bh);
            btnAdd.Click += btnAdd_Click;
            bx += bw + bg;

            // Sửa
            SetToolButton(btnEdit, "✏️ Sửa", System.Drawing.Color.FromArgb(5, 150, 105), bx, by, 80, bh);
            btnEdit.Enabled = false;
            btnEdit.Click += btnEdit_Click;
            bx += 80 + bg;

            // Xóa
            SetToolButton(btnDelete, "🗑️ Xóa", System.Drawing.Color.FromArgb(220, 38, 38), bx, by, 80, bh);
            btnDelete.Enabled = false;
            btnDelete.Click += btnDelete_Click;
            bx += 80 + bg;

            // Đổi trạng thái
            SetToolButton(btnStatus, "🔄 Trạng thái", System.Drawing.Color.FromArgb(245, 158, 11), bx, by, 120, bh);
            btnStatus.Enabled = false;
            btnStatus.Click += btnStatus_Click;
            bx += 120 + bg;

            // Thành viên
            SetToolButton(btnMembers, "👥 Thành viên", System.Drawing.Color.FromArgb(124, 58, 237), bx, by, 120, bh);
            btnMembers.Enabled = false;
            btnMembers.Click += btnMembers_Click;
            bx += 120 + bg;

            // Chi tiết (mọi role đều thấy)
            SetToolButton(btnDetail, "📋 Chi tiết", System.Drawing.Color.FromArgb(71, 85, 105), bx, by, 100, bh);
            btnDetail.Enabled = false;
            btnDetail.Click += btnDetail_Click;
            bx += 100 + bg;

            this.lblCount.AutoSize = false;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblCount.Location = new System.Drawing.Point(bx, by);
            this.lblCount.Size = new System.Drawing.Size(140, bh);
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── DataGridView ─────────────────────────────────
            this.dgvProjects.AllowUserToAddRows = false;
            this.dgvProjects.AllowUserToDeleteRows = false;
            this.dgvProjects.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjects.BorderStyle = BorderStyle.None;
            this.dgvProjects.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProjects.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjects.Dock = DockStyle.Fill;
            this.dgvProjects.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvProjects.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dgvProjects.MultiSelect = false;
            this.dgvProjects.ReadOnly = true;
            this.dgvProjects.RowHeadersVisible = false;
            this.dgvProjects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjects.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.dgvProjects.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvProjects.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvProjects.EnableHeadersVisualStyles = false;
            this.dgvProjects.RowTemplate.Height = 36;
            this.dgvProjects.DefaultCellStyle.Padding = new Padding(4, 0, 0, 0);
            this.dgvProjects.SelectionChanged += dgvProjects_SelectionChanged;
            this.dgvProjects.CellDoubleClick += dgvProjects_CellDoubleClick;

            // Columns — thêm cột Thành viên
            this.colId.Name = "colId"; this.colId.HeaderText = "ID"; this.colId.Visible = false;
            this.colName.Name = "colName"; this.colName.HeaderText = "Tên dự án"; this.colName.Width = 200;
            this.colCustomer.Name = "colCustomer"; this.colCustomer.HeaderText = "Khách hàng"; this.colCustomer.Width = 140;
            this.colOwner.Name = "colOwner"; this.colOwner.HeaderText = "PM"; this.colOwner.Width = 120;
            this.colProjStatus.Name = "colProjStatus"; this.colProjStatus.HeaderText = "Trạng thái"; this.colProjStatus.Width = 130;
            this.colMembers.Name = "colMembers"; this.colMembers.HeaderText = "Thành viên"; this.colMembers.Width = 90;
            this.colDeadline.Name = "colDeadline"; this.colDeadline.HeaderText = "Deadline"; this.colDeadline.Width = 95;
            this.colBudget.Name = "colBudget"; this.colBudget.HeaderText = "Ngân sách"; this.colBudget.Width = 110;
            this.colStartDate.Name = "colStartDate"; this.colStartDate.HeaderText = "Bắt đầu"; this.colStartDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.dgvProjects.Columns.AddRange(new DataGridViewColumn[]
            { colId, colName, colCustomer, colOwner, colProjStatus, colMembers, colDeadline, colBudget, colStartDate });

            // ── Status bar ───────────────────────────────────
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.panelStatus.Dock = DockStyle.Bottom;
            this.panelStatus.Height = 28;
            this.panelStatus.Controls.Add(this.lblStatus);

            this.lblStatus.AutoSize = false;
            this.lblStatus.Dock = DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblStatus.Padding = new Padding(12, 0, 0, 0);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ── Form ─────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvProjects);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "frmProjects";
            this.Text = "Quản lý Dự án";
            this.StartPosition = FormStartPosition.Manual;

            this.panelTop.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dgvProjects).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>Helper tạo toolbar button đồng bộ style.</summary>
        private void SetToolButton(Button btn, string text, System.Drawing.Color bgColor, int x, int y, int w, int h)
        {
            btn.BackColor = bgColor;
            btn.Cursor = Cursors.Hand;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btn.ForeColor = System.Drawing.Color.White;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(w, h);
            btn.Text = text;
        }

        private Panel panelTop, panelFilter, panelToolbar, panelStatus;
        private Label lblHeader, lblCount, lblStatus;
        private TextBox txtSearch;
        private ComboBox cboFilterStatus;
        private Button btnRefresh, btnAdd, btnEdit, btnDelete, btnStatus, btnMembers, btnDetail;
        private DataGridView dgvProjects;
        private DataGridViewTextBoxColumn colId, colName, colCustomer, colOwner, colProjStatus, colMembers, colDeadline, colBudget, colStartDate;
    }
}

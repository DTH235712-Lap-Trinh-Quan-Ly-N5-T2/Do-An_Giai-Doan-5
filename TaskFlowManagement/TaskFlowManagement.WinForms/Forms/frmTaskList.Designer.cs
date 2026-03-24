namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmTaskList
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Khởi tạo tất cả control ──────────────────────────
            panelTop        = new Panel();
            panelBottom     = new Panel();
            panelPaging     = new Panel();
            dgvTasks        = new DataGridView();
            txtSearch       = new TextBox();
            cboStatusFilter  = new ComboBox();
            cboProjectFilter = new ComboBox();
            btnAddNew       = new Button();
            btnEdit         = new Button();
            btnDelete       = new Button();
            btnRefresh      = new Button();
            lblCount        = new Label();
            lblStatus       = new Label();
            btnPrev         = new Button();
            lblPage         = new Label();
            btnNext         = new Button();

            // ── Cột DataGridView ──────────────────────────────────
            colId       = new DataGridViewTextBoxColumn();
            colTitle    = new DataGridViewTextBoxColumn();
            colProject  = new DataGridViewTextBoxColumn();
            colAssignee = new DataGridViewTextBoxColumn();
            colPriority = new DataGridViewTextBoxColumn();
            colStatus   = new DataGridViewTextBoxColumn();
            colProgress = new DataGridViewTextBoxColumn();
            colDueDate  = new DataGridViewTextBoxColumn();

            panelTop.SuspendLayout();
            panelBottom.SuspendLayout();
            panelPaging.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelTop — thanh công cụ phía trên
            // ════════════════════════════════════════════════════
            panelTop.Dock    = DockStyle.Top;
            panelTop.Height  = 56;
            panelTop.Padding = new Padding(10, 10, 10, 6);
            panelTop.Controls.AddRange(new Control[]
            {
                txtSearch, cboProjectFilter, cboStatusFilter,
                btnAddNew, btnEdit, btnDelete, btnRefresh, lblCount
            });

            // ── txtSearch ─────────────────────────────────────
            txtSearch.Location    = new Point(10, 14);
            txtSearch.Size        = new Size(200, 28);
            txtSearch.Font        = new Font("Segoe UI", 10F);
            txtSearch.PlaceholderText = "🔍  Tìm kiếm...";
            txtSearch.TextChanged += txtSearch_TextChanged;

            // ── cboProjectFilter ──────────────────────────────
            cboProjectFilter.Location         = new Point(220, 13);
            cboProjectFilter.Size             = new Size(180, 28);
            cboProjectFilter.Font             = new Font("Segoe UI", 10F);
            cboProjectFilter.DropDownStyle    = ComboBoxStyle.DropDownList;
            cboProjectFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            // ── cboStatusFilter ───────────────────────────────
            cboStatusFilter.Location         = new Point(410, 13);
            cboStatusFilter.Size             = new Size(170, 28);
            cboStatusFilter.Font             = new Font("Segoe UI", 10F);
            cboStatusFilter.DropDownStyle    = ComboBoxStyle.DropDownList;
            cboStatusFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            // ── btnAddNew ─────────────────────────────────────
            btnAddNew.Location  = new Point(598, 11);
            btnAddNew.Size      = new Size(95, 30);
            btnAddNew.Text      = "➕ Thêm mới";
            btnAddNew.Font      = new Font("Segoe UI", 9.5F);
            btnAddNew.Click    += btnAddNew_Click;

            // ── btnEdit ───────────────────────────────────────
            btnEdit.Location  = new Point(700, 11);
            btnEdit.Size      = new Size(80, 30);
            btnEdit.Text      = "✏️ Sửa";
            btnEdit.Font      = new Font("Segoe UI", 9.5F);
            btnEdit.Enabled   = false;
            btnEdit.Click    += btnEdit_Click;

            // ── btnDelete ─────────────────────────────────────
            btnDelete.Location  = new Point(787, 11);
            btnDelete.Size      = new Size(80, 30);
            btnDelete.Text      = "🗑 Xóa";
            btnDelete.Font      = new Font("Segoe UI", 9.5F);
            btnDelete.Enabled   = false;
            btnDelete.Click    += btnDelete_Click;

            // ── btnRefresh ────────────────────────────────────
            btnRefresh.Location  = new Point(874, 11);
            btnRefresh.Size      = new Size(80, 30);
            btnRefresh.Text      = "🔄 Làm mới";
            btnRefresh.Font      = new Font("Segoe UI", 9.5F);
            btnRefresh.Click    += btnRefresh_Click;

            // ── lblCount ──────────────────────────────────────
            lblCount.Location  = new Point(965, 16);
            lblCount.Size      = new Size(130, 22);
            lblCount.Text      = "0 công việc";
            lblCount.Font      = new Font("Segoe UI", 9.5F);
            lblCount.TextAlign = ContentAlignment.MiddleRight;

            // ════════════════════════════════════════════════════
            // panelBottom — thanh trạng thái phía dưới
            // ════════════════════════════════════════════════════
            panelBottom.Dock   = DockStyle.Bottom;
            panelBottom.Height = 32;
            panelBottom.Controls.AddRange(new Control[] { lblStatus, panelPaging });

            // ── lblStatus ─────────────────────────────────────
            lblStatus.Location  = new Point(10, 7);
            lblStatus.Size      = new Size(600, 20);
            lblStatus.Text      = "Sẵn sàng";
            lblStatus.Font      = new Font("Segoe UI", 9F);

            // ── panelPaging (nhóm nút phân trang) ────────────
            panelPaging.Dock = DockStyle.Right;
            panelPaging.Width = 220;
            panelPaging.Controls.AddRange(new Control[] { btnPrev, lblPage, btnNext });

            btnPrev.Location = new Point(5, 4);
            btnPrev.Size     = new Size(60, 24);
            btnPrev.Text     = "◀ Trước";
            btnPrev.Font     = new Font("Segoe UI", 8.5F);
            btnPrev.Enabled  = false;
            btnPrev.Click   += btnPrev_Click;

            lblPage.Location  = new Point(70, 7);
            lblPage.Size      = new Size(90, 18);
            lblPage.Text      = "Trang 1 / 1";
            lblPage.Font      = new Font("Segoe UI", 9F);
            lblPage.TextAlign = ContentAlignment.MiddleCenter;

            btnNext.Location = new Point(165, 4);
            btnNext.Size     = new Size(50, 24);
            btnNext.Text     = "Sau ▶";
            btnNext.Font     = new Font("Segoe UI", 8.5F);
            btnNext.Enabled  = false;
            btnNext.Click   += btnNext_Click;

            // ════════════════════════════════════════════════════
            // dgvTasks — DataGridView chính
            // ════════════════════════════════════════════════════
            dgvTasks.Dock                  = DockStyle.Fill;
            dgvTasks.AutoGenerateColumns   = false;
            dgvTasks.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.MultiSelect           = false;
            dgvTasks.ReadOnly              = true;
            dgvTasks.AllowUserToAddRows    = false;
            dgvTasks.AllowUserToDeleteRows = false;
            dgvTasks.RowHeadersVisible     = false;
            dgvTasks.Font                  = new Font("Segoe UI", 9.5F);
            dgvTasks.ColumnHeadersHeight   = 36;
            dgvTasks.RowTemplate.Height    = 30;
            dgvTasks.SelectionChanged     += dgvTasks_SelectionChanged;
            dgvTasks.CellDoubleClick      += dgvTasks_CellDoubleClick;

            // ── Định nghĩa cột ────────────────────────────────

            // colId — ẩn, chỉ dùng để tra Id khi xử lý sự kiện
            colId.Name            = "colId";
            colId.HeaderText      = "ID";
            colId.Width           = 50;
            colId.Visible         = false;

            // colTitle — cột chính, tự động lấp đầy không gian còn lại
            colTitle.Name         = "colTitle";
            colTitle.HeaderText   = "Tiêu đề công việc";
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTitle.MinimumWidth = 200;

            colProject.Name       = "colProject";
            colProject.HeaderText = "Dự án";
            colProject.Width      = 150;

            colAssignee.Name       = "colAssignee";
            colAssignee.HeaderText = "Người thực hiện";
            colAssignee.Width      = 140;

            colPriority.Name       = "colPriority";
            colPriority.HeaderText = "Ưu tiên";
            colPriority.Width      = 85;

            colStatus.Name       = "colStatus";
            colStatus.HeaderText = "Trạng thái";
            colStatus.Width      = 110;

            colProgress.Name        = "colProgress";
            colProgress.HeaderText  = "%";
            colProgress.Width       = 55;
            colProgress.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            colDueDate.Name       = "colDueDate";
            colDueDate.HeaderText = "Hạn chót";
            colDueDate.Width      = 95;

            dgvTasks.Columns.AddRange(new DataGridViewColumn[]
            {
                colId, colTitle, colProject, colAssignee,
                colPriority, colStatus, colProgress, colDueDate
            });

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.Text            = "Quản lý công việc";
            this.Size            = new Size(1100, 650);
            this.MinimumSize     = new Size(900, 500);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.Font            = new Font("Segoe UI", 9.5F);

            this.Controls.Add(dgvTasks);       // Fill — đặt trước Panel để không bị che
            this.Controls.Add(panelTop);       // Dock Top
            this.Controls.Add(panelBottom);    // Dock Bottom

            panelTop.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            panelPaging.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            this.ResumeLayout(false);
        }

        // ── Khai báo biến — theo quy ước Designer để cuối file ──
        private Panel                      panelTop;
        private Panel                      panelBottom;
        private Panel                      panelPaging;
        private DataGridView               dgvTasks;
        private TextBox                    txtSearch;
        private ComboBox                   cboStatusFilter;
        private ComboBox                   cboProjectFilter;
        private Button                     btnAddNew;
        private Button                     btnEdit;
        private Button                     btnDelete;
        private Button                     btnRefresh;
        private Label                      lblCount;
        private Label                      lblStatus;
        private Button                     btnPrev;
        private Label                      lblPage;
        private Button                     btnNext;
        private DataGridViewTextBoxColumn  colId;
        private DataGridViewTextBoxColumn  colTitle;
        private DataGridViewTextBoxColumn  colProject;
        private DataGridViewTextBoxColumn  colAssignee;
        private DataGridViewTextBoxColumn  colPriority;
        private DataGridViewTextBoxColumn  colStatus;
        private DataGridViewTextBoxColumn  colProgress;
        private DataGridViewTextBoxColumn  colDueDate;
    }
}

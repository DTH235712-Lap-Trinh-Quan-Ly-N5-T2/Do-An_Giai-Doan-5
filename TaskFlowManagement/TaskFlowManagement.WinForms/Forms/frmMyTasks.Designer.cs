namespace TaskFlowManagement.WinForms.Forms
{
    partial class frmMyTasks
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
            panelTop     = new Panel();
            panelBottom  = new Panel();
            tabControl   = new TabControl();
            tabMyTasks   = new TabPage();
            tabReview    = new TabPage();
            tabTesting   = new TabPage();
            dgvMyTasks   = new DataGridView();
            dgvReview    = new DataGridView();
            dgvTesting   = new DataGridView();
            lblUser      = new Label();
            lblStatus    = new Label();
            btnRefresh   = new Button();

            panelTop.SuspendLayout();
            panelBottom.SuspendLayout();
            tabControl.SuspendLayout();
            tabMyTasks.SuspendLayout();
            tabReview.SuspendLayout();
            tabTesting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMyTasks).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTesting).BeginInit();
            this.SuspendLayout();

            // ════════════════════════════════════════════════════
            // panelTop — thanh thông tin user + nút Làm mới
            // ════════════════════════════════════════════════════
            panelTop.Dock    = DockStyle.Top;
            panelTop.Height  = 46;
            panelTop.Padding = new Padding(10, 8, 10, 6);

            // ── lblUser ───────────────────────────────────────
            lblUser.Location  = new Point(10, 13);
            lblUser.Size      = new Size(700, 22);
            lblUser.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUser.ForeColor = Color.FromArgb(37, 99, 235);
            lblUser.Text      = "Đang tải...";

            // ── btnRefresh ────────────────────────────────────
            btnRefresh.Location  = new Point(800, 8);
            btnRefresh.Size      = new Size(100, 30);
            btnRefresh.Text      = "🔄 Làm mới";
            btnRefresh.Font      = new Font("Segoe UI", 9.5F);
            btnRefresh.Click    += btnRefresh_Click;

            panelTop.Controls.AddRange(new Control[] { lblUser, btnRefresh });

            // ════════════════════════════════════════════════════
            // panelBottom — thanh trạng thái phía dưới
            // ════════════════════════════════════════════════════
            panelBottom.Dock   = DockStyle.Bottom;
            panelBottom.Height = 30;

            // ── lblStatus ─────────────────────────────────────
            lblStatus.Location  = new Point(10, 7);
            lblStatus.Size      = new Size(700, 20);
            lblStatus.Font      = new Font("Segoe UI", 9F);
            lblStatus.Text      = "Sẵn sàng";

            panelBottom.Controls.Add(lblStatus);

            // ════════════════════════════════════════════════════
            // Tạo cấu hình cột dùng chung cho cả 3 DataGridView
            // Gọi helper để tránh lặp code
            // ════════════════════════════════════════════════════
            ConfigureTaskGrid(dgvMyTasks);
            ConfigureTaskGrid(dgvReview);
            ConfigureTaskGrid(dgvTesting);

            // Gắn event double-click cho cả 3 grid
            dgvMyTasks.CellDoubleClick += dgv_CellDoubleClick;
            dgvReview.CellDoubleClick  += dgv_CellDoubleClick;
            dgvTesting.CellDoubleClick += dgv_CellDoubleClick;

            // ════════════════════════════════════════════════════
            // Tab Pages
            // ════════════════════════════════════════════════════

            // ── tabMyTasks ────────────────────────────────────
            tabMyTasks.Text    = "📋 Được giao (0)";
            tabMyTasks.Padding = new Padding(3);
            tabMyTasks.Controls.Add(dgvMyTasks);
            dgvMyTasks.Dock = DockStyle.Fill;

            // ── tabReview ─────────────────────────────────────
            tabReview.Text    = "🔍 Review (0)";
            tabReview.Padding = new Padding(3);
            tabReview.Controls.Add(dgvReview);
            dgvReview.Dock = DockStyle.Fill;

            // ── tabTesting ────────────────────────────────────
            tabTesting.Text    = "🧪 Testing (0)";
            tabTesting.Padding = new Padding(3);
            tabTesting.Controls.Add(dgvTesting);
            dgvTesting.Dock = DockStyle.Fill;

            // ════════════════════════════════════════════════════
            // TabControl
            // ════════════════════════════════════════════════════
            tabControl.Dock     = DockStyle.Fill;
            tabControl.Font     = new Font("Segoe UI", 10F);
            tabControl.ItemSize = new Size(160, 30); // Tab rộng hơn để đủ chỗ cho icon + text
            tabControl.TabPages.AddRange(new TabPage[] { tabMyTasks, tabReview, tabTesting });

            // ════════════════════════════════════════════════════
            // Form
            // ════════════════════════════════════════════════════
            this.Text          = "Công việc của tôi";
            this.Size          = new Size(1000, 620);
            this.MinimumSize   = new Size(800, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font          = new Font("Segoe UI", 9.5F);

            this.Controls.Add(tabControl);  // Fill — đặt trước Panel
            this.Controls.Add(panelTop);    // Dock Top
            this.Controls.Add(panelBottom); // Dock Bottom

            panelTop.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabMyTasks.ResumeLayout(false);
            tabReview.ResumeLayout(false);
            tabTesting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMyTasks).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReview).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTesting).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Cấu hình DataGridView theo chuẩn chung của GD4.
        /// Gọi cho cả 3 grid để tránh lặp code.
        /// Tất cả cột đều dùng tên prefix "col" để BindGrid() tham chiếu.
        /// </summary>
        private static void ConfigureTaskGrid(DataGridView dgv)
        {
            dgv.AutoGenerateColumns   = false;
            dgv.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect           = false;
            dgv.ReadOnly              = true;
            dgv.AllowUserToAddRows    = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible     = false;
            dgv.Font                  = new Font("Segoe UI", 9.5F);
            dgv.ColumnHeadersHeight   = 36;
            dgv.RowTemplate.Height    = 30;

            // ── Định nghĩa cột ────────────────────────────────
            var colId = new DataGridViewTextBoxColumn
            {
                Name        = "colId",
                HeaderText  = "ID",
                Width       = 50,
                Visible     = false  // ẩn, chỉ dùng để tra Id khi xử lý sự kiện
            };
            var colTitle = new DataGridViewTextBoxColumn
            {
                Name         = "colTitle",
                HeaderText   = "Tiêu đề công việc",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 200
            };
            var colProject = new DataGridViewTextBoxColumn
            {
                Name       = "colProject",
                HeaderText = "Dự án",
                Width      = 140
            };
            var colPriority = new DataGridViewTextBoxColumn
            {
                Name       = "colPriority",
                HeaderText = "Ưu tiên",
                Width      = 85
            };
            var colStatus = new DataGridViewTextBoxColumn
            {
                Name       = "colStatus",
                HeaderText = "Trạng thái",
                Width      = 110
            };
            var colProgress = new DataGridViewTextBoxColumn
            {
                Name       = "colProgress",
                HeaderText = "%",
                Width      = 55,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            var colDueDate = new DataGridViewTextBoxColumn
            {
                Name       = "colDueDate",
                HeaderText = "Hạn chót",
                Width      = 95
            };

            dgv.Columns.AddRange(new DataGridViewColumn[]
            {
                colId, colTitle, colProject, colPriority, colStatus, colProgress, colDueDate
            });
        }

        // ── Khai báo biến — theo quy ước Designer để cuối file ──
        private Panel      panelTop;
        private Panel      panelBottom;
        private TabControl tabControl;
        private TabPage    tabMyTasks;
        private TabPage    tabReview;
        private TabPage    tabTesting;
        private DataGridView dgvMyTasks;
        private DataGridView dgvReview;
        private DataGridView dgvTesting;
        private Label      lblUser;
        private Label      lblStatus;
        private Button     btnRefresh;
    }
}

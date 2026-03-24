using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmTaskList : Form
    {
        // ────────────────────────────────────────────────────────
        // DEPENDENCIES & STATE
        // ────────────────────────────────────────────────────────
        private readonly ITaskService _taskService = null!;
        private readonly IProjectService _projectService = null!;
        private readonly IUserService _userService = null!;

        private List<TaskItem> _currentItems = new();
        private TaskItem? _selectedTask;
        private int _currentPage = 1;
        private int _totalCount = 0;
        private const int PAGE_SIZE = 20;

        // Debounce: tránh gọi DB liên tục khi user đang gõ
        private readonly System.Windows.Forms.Timer _debounceTimer = new() { Interval = 500 };

        // Cache lookup — load 1 lần khi mở form
        private List<Status> _statuses = new();
        private List<Project> _projects = new();

        // ────────────────────────────────────────────────────────
        // CONSTRUCTORS
        // ────────────────────────────────────────────────────────

        // Constructor mặc định — BẮT BUỘC để tab [Design] không lỗi
        public frmTaskList()
        {
            InitializeComponent();
            _debounceTimer.Tick += DebounceTimer_Tick;
        }

        // Constructor DI — ServiceProvider gọi cái này khi chạy thật.
        // FIX: Thêm tham số IUserService userService vào signature (thiếu ở version trước)
        public frmTaskList(ITaskService taskService, IProjectService projectService, IUserService userService)
            : this()
        {
            _taskService = taskService;
            _projectService = projectService;
            _userService = userService;   // FIX: gán đúng tham số vào field
        }

        // ────────────────────────────────────────────────────────
        // FORM LOAD
        // ────────────────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyRolePermissions();
            await LoadLookupsAsync();
            await LoadDataAsync();
        }

        // Ẩn nút CRUD nếu là Developer (chỉ được xem)
        private void ApplyRolePermissions()
        {
            bool canEdit = AppSession.IsManager;
            btnAddNew.Visible = canEdit;
            btnEdit.Visible = canEdit;
            btnDelete.Visible = canEdit;
        }

        // ────────────────────────────────────────────────────────
        // LOAD LOOKUP → đổ vào ComboBox filter
        // ────────────────────────────────────────────────────────

        private async Task LoadLookupsAsync()
        {
            // Load song song 2 nguồn để tiết kiệm thời gian chờ
            var t1 = _taskService.GetAllStatusesAsync();
            var t2 = _projectService.GetProjectsForUserAsync(
                         AppSession.UserId, AppSession.IsManager);
            await Task.WhenAll(t1, t2);

            _statuses = t1.Result;
            _projects = t2.Result;

            // -- cboStatusFilter --
            cboStatusFilter.Items.Clear();
            cboStatusFilter.Items.Add(new ComboItem(0, "— Tất cả trạng thái —"));
            foreach (var s in _statuses)
                cboStatusFilter.Items.Add(new ComboItem(s.Id, s.Name));
            cboStatusFilter.SelectedIndex = 0;

            // -- cboProjectFilter --
            cboProjectFilter.Items.Clear();
            cboProjectFilter.Items.Add(new ComboItem(0, "— Tất cả dự án —"));
            foreach (var p in _projects)
                cboProjectFilter.Items.Add(new ComboItem(p.Id, p.Name));
            cboProjectFilter.SelectedIndex = 0;
        }

        // ────────────────────────────────────────────────────────
        // LOAD DATA CHÍNH → fill vào dgvTasks
        // ────────────────────────────────────────────────────────

        private async Task LoadDataAsync()
        {
            lblStatus.Text = "Đang tải...";

            try
            {
                var keyword = txtSearch.Text.Trim();
                var statusId = GetComboId(cboStatusFilter);
                var projectId = GetComboId(cboProjectFilter);

                // Developer chỉ thấy task được giao cho chính mình
                int? assignedToId = AppSession.IsManager ? null : AppSession.UserId;

                var (items, total) = await _taskService.GetPagedAsync(
                    page: _currentPage,
                    pageSize: PAGE_SIZE,
                    projectId: projectId > 0 ? projectId : null,
                    assignedToId: assignedToId,
                    statusId: statusId > 0 ? statusId : null,
                    keyword: string.IsNullOrEmpty(keyword) ? null : keyword);

                _currentItems = items;
                _totalCount = total;

                BindGrid(items);
                UpdatePagingLabel();
                lblStatus.Text = $"Hiển thị {items.Count} / {total} công việc";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi tải dữ liệu.";
                MessageBox.Show("Không thể tải dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Đổ danh sách vào DataGridView, tô màu theo trạng thái
        private void BindGrid(List<TaskItem> items)
        {
            dgvTasks.Rows.Clear();
            _selectedTask = null;
            RefreshButtonStates();

            foreach (var t in items)
            {
                var due = t.DueDate.HasValue
                    ? t.DueDate.Value.ToLocalTime().ToString("dd/MM/yyyy")
                    : "—";

                // Thứ tự khớp với cột Designer: colId|colTitle|colProject|
                // colAssignee|colPriority|colStatus|colProgress|colDueDate
                int idx = dgvTasks.Rows.Add(
                    t.Id,
                    t.Title,
                    t.Project?.Name ?? "—",
                    t.AssignedTo?.FullName ?? "—",
                    t.Priority?.Name ?? "—",
                    t.Status?.Name ?? "—",
                    $"{t.ProgressPercent}%",
                    due);

                ApplyRowColor(dgvTasks.Rows[idx], t);
            }

            lblCount.Text = $"{items.Count} công việc";
        }

        // Tô màu hàng theo trạng thái — tách riêng để dễ chỉnh màu sau
        private static void ApplyRowColor(DataGridViewRow row, TaskItem task)
        {
            // Quá hạn → đỏ đậm (ưu tiên cao nhất, kiểm tra trước tiên)
            if (task.DueDate.HasValue && task.DueDate.Value < DateTime.UtcNow && !task.IsCompleted)
            {
                row.DefaultCellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                return;
            }

            row.DefaultCellStyle.ForeColor = task.Status?.Name switch
            {
                "CLOSED" => Color.FromArgb(5, 150, 105),
                "RESOLVED" => Color.FromArgb(22, 163, 74),
                "FAILED" => Color.FromArgb(220, 38, 38),
                "IN-PROGRESS" => Color.FromArgb(37, 99, 235),
                "REVIEW-1" or "REVIEW-2" => Color.FromArgb(180, 83, 9),
                "IN-TEST" => Color.FromArgb(109, 40, 217),
                _ => Color.FromArgb(30, 41, 59),
            };
        }

        // ────────────────────────────────────────────────────────
        // DEBOUNCE SEARCH
        // ────────────────────────────────────────────────────────

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private async void DebounceTimer_Tick(object? sender, EventArgs e)
        {
            _debounceTimer.Stop();
            _currentPage = 1;
            await LoadDataAsync();
        }

        // ────────────────────────────────────────────────────────
        // FILTER COMBOBOX
        // ────────────────────────────────────────────────────────

        private async void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadDataAsync();
        }

        // ────────────────────────────────────────────────────────
        // PHÂN TRANG
        // ────────────────────────────────────────────────────────

        private void UpdatePagingLabel()
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)_totalCount / PAGE_SIZE));
            lblPage.Text = $"Trang {_currentPage} / {totalPages}";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage <= 1) return;
            _currentPage--;
            await LoadDataAsync();
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            int totalPages = Math.Max(1, (int)Math.Ceiling((double)_totalCount / PAGE_SIZE));
            if (_currentPage >= totalPages) return;
            _currentPage++;
            await LoadDataAsync();
        }

        // ────────────────────────────────────────────────────────
        // SELECTION
        // ────────────────────────────────────────────────────────

        private void dgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0)
            {
                _selectedTask = null;
                RefreshButtonStates();
                return;
            }

            var cell = dgvTasks.SelectedRows[0].Cells["colId"].Value;
            if (cell == null) return;

            int id = (int)cell;
            _selectedTask = _currentItems.FirstOrDefault(t => t.Id == id);
            RefreshButtonStates();
        }

        private void RefreshButtonStates()
        {
            bool sel = _selectedTask != null;
            btnEdit.Enabled = sel;
            btnDelete.Enabled = sel;
        }

        // ────────────────────────────────────────────────────────
        // CRUD
        // ────────────────────────────────────────────────────────

        // Thêm mới: taskId = null → frmTaskEdit biết là chế độ Thêm
        private async void btnAddNew_Click(object sender, EventArgs e)
        {
            using var dlg = new frmTaskEdit(_taskService, _projectService, _userService, null);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                await LoadDataAsync();
        }

        // Sửa: truyền Id của task đang được chọn
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedTask == null) return;
            using var dlg = new frmTaskEdit(_taskService, _projectService, _userService, _selectedTask.Id);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                await LoadDataAsync();
        }

        // Double-click hàng → mở form Sửa luôn
        private void dgvTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, e);
        }

        // Xóa: hỏi xác nhận → gọi Service → xử lý Result Pattern
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedTask == null) return;

            if (MessageBox.Show(
                    $"Xóa công việc \"{_selectedTask.Title}\"?\n\n⚠ Không thể hoàn tác.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            var (ok, msg) = await _taskService.DeleteTaskAsync(_selectedTask.Id);

            MessageBox.Show(msg,
                ok ? "Thành công" : "Không thể xóa",
                MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (ok) await LoadDataAsync();
        }

        // Xóa hết filter rồi load lại từ trang đầu
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboStatusFilter.SelectedIndex = 0;
            cboProjectFilter.SelectedIndex = 0;
            _currentPage = 1;
            await LoadDataAsync();
        }

        // ────────────────────────────────────────────────────────
        // HELPERS
        // ────────────────────────────────────────────────────────

        private static int GetComboId(ComboBox cbo)
            => cbo.SelectedItem is ComboItem ci ? ci.Id : 0;

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _debounceTimer.Stop();
            _debounceTimer.Dispose();
            base.OnFormClosed(e);
        }
    }

    // Helper record dùng chung cho mọi ComboBox trong GD4
    internal record ComboItem(int Id, string Label)
    {
        public override string ToString() => Label;
    }
}
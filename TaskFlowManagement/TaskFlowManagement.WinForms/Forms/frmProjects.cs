using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Màn hình danh sách dự án.
    /// Manager/Admin: tạo, sửa, xóa, đổi trạng thái, quản lý thành viên.
    /// Developer: chỉ xem dự án mình tham gia (read-only).
    /// </summary>
    public partial class frmProjects : Form
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly ICustomerRepository _customerRepo;
        private List<Project> _allProjects = new();
        private Project? _selectedProject;

        public frmProjects(IProjectService projectService, IUserService userService,
            ITaskService taskService, ICustomerRepository customerRepo)
        {
            _projectService = projectService;
            _userService    = userService;
            _taskService    = taskService;
            _customerRepo   = customerRepo;
            InitializeComponent();
            SetupPermissions();
        }

        /// <summary>Ẩn nút CRUD nếu Developer (chỉ xem, không chỉnh sửa).</summary>
        private void SetupPermissions()
        {
            bool canEdit = AppSession.IsManager;
            btnAdd.Visible    = canEdit;
            btnEdit.Visible   = canEdit;
            btnDelete.Visible = canEdit;
            btnStatus.Visible = canEdit;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadProjectsAsync();
        }

        /// <summary>Load danh sách dự án từ DB theo quyền user hiện tại.</summary>
        private async Task LoadProjectsAsync()
        {
            SetStatus("Đang tải...");
            _allProjects = await _projectService.GetProjectsForUserAsync(
                AppSession.UserId, AppSession.IsManager);

            // Load chi tiết members cho mỗi project để đếm thành viên
            for (int i = 0; i < _allProjects.Count; i++)
            {
                var detail = await _projectService.GetProjectDetailsAsync(_allProjects[i].Id);
                if (detail != null) _allProjects[i] = detail;
            }

            ApplyFilter();
            SetStatus($"Tổng: {_allProjects.Count} dự án");
        }

        /// <summary>Lọc danh sách theo từ khóa tìm kiếm và trạng thái.</summary>
        private void ApplyFilter()
        {
            var keyword = txtSearch.Text.Trim().ToLower();
            var statusFilter = cboFilterStatus.SelectedIndex > 0
                ? cboFilterStatus.SelectedItem!.ToString()! : "";

            var filtered = _allProjects.Where(p =>
            {
                bool matchKeyword = string.IsNullOrEmpty(keyword)
                    || p.Name.ToLower().Contains(keyword)
                    || (p.Customer?.CompanyName?.ToLower().Contains(keyword) ?? false)
                    || (p.Owner?.FullName?.ToLower().Contains(keyword) ?? false);

                bool matchStatus = string.IsNullOrEmpty(statusFilter)
                    || p.Status == statusFilter;

                return matchKeyword && matchStatus;
            }).ToList();

            BindGrid(filtered);
        }

        /// <summary>Hiển thị danh sách dự án lên DataGridView với tô màu theo trạng thái.</summary>
        private void BindGrid(List<Project> projects)
        {
            dgvProjects.Rows.Clear();
            foreach (var p in projects)
            {
                // Hiển thị trạng thái kèm icon
                var statusText = p.Status switch
                {
                    "NotStarted" => "📋 Chưa bắt đầu",
                    "InProgress" => "🔄 Đang thực hiện",
                    "OnHold"     => "⏸ Tạm dừng",
                    "Completed"  => "✅ Hoàn thành",
                    "Cancelled"  => "❌ Đã hủy",
                    _            => p.Status
                };

                var deadline = p.PlannedEndDate.HasValue
                    ? p.PlannedEndDate.Value.ToString("dd/MM/yyyy") : "—";

                var budget = p.Budget > 0
                    ? p.Budget.ToString("N0") + " ₫" : "—";

                // Đếm thành viên active
                var memberCount = p.Members?.Count(m => m.LeftAt == null) ?? 0;

                int idx = dgvProjects.Rows.Add(
                    p.Id, p.Name, p.Customer?.CompanyName ?? "—",
                    p.Owner?.FullName ?? "—", statusText,
                    $"{memberCount} người",
                    deadline, budget,
                    p.StartDate.ToString("dd/MM/yyyy"));

                // Tô màu theo trạng thái
                var row = dgvProjects.Rows[idx];
                if (p.Status == "Completed")
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
                else if (p.Status == "Cancelled")
                {
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
                    row.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
                }
                // Highlight đỏ nếu quá deadline mà chưa hoàn thành
                else if (p.PlannedEndDate.HasValue
                    && p.PlannedEndDate.Value < DateOnly.FromDateTime(DateTime.Now)
                    && p.Status != "Completed")
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            }
            lblCount.Text = $"{projects.Count} dự án";
        }

        /// <summary>Cập nhật trạng thái nút khi chọn dòng khác trên grid.</summary>
        private void dgvProjects_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProjects.SelectedRows.Count == 0) { _selectedProject = null; UpdateButtons(); return; }
            int id = (int)dgvProjects.SelectedRows[0].Cells["colId"].Value;
            _selectedProject = _allProjects.FirstOrDefault(p => p.Id == id);
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool sel = _selectedProject != null;
            btnEdit.Enabled    = sel;
            btnDelete.Enabled  = sel;
            btnMembers.Enabled = sel;
            btnStatus.Enabled  = sel;
            btnDetail.Enabled  = sel;
            btnKanban.Enabled  = sel;
        }

        // ── Thêm dự án mới ───────────────────────────────────
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using var dlg = new frmProjectEdit(_projectService, _userService, _customerRepo, null);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                await LoadProjectsAsync();
        }

        // ── Sửa dự án ────────────────────────────────────────
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;
            var detail = await _projectService.GetProjectDetailsAsync(_selectedProject.Id);
            if (detail == null) return;
            using var dlg = new frmProjectEdit(_projectService, _userService, _customerRepo, detail);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                await LoadProjectsAsync();
        }

        // ── Xóa dự án (chỉ khi chưa có task) ────────────────
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;
            if (MessageBox.Show(
                $"Xóa dự án \"{_selectedProject.Name}\"?\n\nChỉ xóa được nếu dự án chưa có công việc nào.",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            var (ok, msg) = await _projectService.DeleteProjectAsync(_selectedProject.Id);
            MessageBox.Show(msg, ok ? "Thành công" : "Không thể xóa",
                MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (ok) await LoadProjectsAsync();
        }

        // ── Đổi trạng thái dự án ─────────────────────────────
        private async void btnStatus_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;
            var menu = new ContextMenuStrip();
            var statuses = new[] { "NotStarted", "InProgress", "OnHold", "Completed", "Cancelled" };
            foreach (var s in statuses)
            {
                var status = s;
                var item = menu.Items.Add(status);
                item.Click += async (_, _) =>
                {
                    var (ok, msg) = await _projectService.ChangeStatusAsync(_selectedProject.Id, status);
                    if (ok) await LoadProjectsAsync();
                    else MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };
            }
            menu.Show(btnStatus, new System.Drawing.Point(0, btnStatus.Height));
        }

        // ── Quản lý thành viên ────────────────────────────────
        private async void btnMembers_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;
            using var dlg = new frmProjectMembers(_projectService, _userService, _selectedProject);
            dlg.ShowDialog(this);
            await LoadProjectsAsync();
        }

        // ── Xem Kanban board theo Project ─────────────────────
        private void btnKanban_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null)
            {
                MessageBox.Show("Vui lòng chọn một dự án để xem Kanban Board.",
                    "Chưa chọn dự án", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new frmKanban(_taskService, _projectService, _userService, _selectedProject.Id);
            dlg.ShowDialog(this);
        }

        // ── Chi tiết dự án (UC-15) ───────────────────────────
        private async void btnDetail_Click(object sender, EventArgs e)
        {
            if (_selectedProject == null) return;
            var detail = await _projectService.GetProjectDetailsAsync(_selectedProject.Id);
            if (detail == null) { MessageBox.Show("Không tìm thấy dự án.", "Lỗi"); return; }

            // Tổng hợp thông tin chi tiết
            var memberCount = detail.Members?.Count(m => m.LeftAt == null) ?? 0;
            var taskCount   = detail.Tasks?.Count ?? 0;
            var totalExpense = detail.Expenses?.Sum(ex => ex.Amount) ?? 0;
            var progress    = detail.ProgressPercent;

            var memberList = detail.Members?
                .Where(m => m.LeftAt == null)
                .Select(m => $"  👤 {m.User?.FullName ?? "?"} — {m.ProjectRole ?? "Developer"}")
                .ToList() ?? new();

            var info = $"📁  {detail.Name}\n" +
                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                $"Khách hàng:   {detail.Customer?.CompanyName ?? "—"}\n" +
                $"Quản lý:        {detail.Owner?.FullName ?? "—"}\n" +
                $"Trạng thái:    {detail.Status}\n" +
                $"Ngày bắt đầu: {detail.StartDate:dd/MM/yyyy}\n" +
                $"Deadline:       {(detail.PlannedEndDate.HasValue ? detail.PlannedEndDate.Value.ToString("dd/MM/yyyy") : "—")}\n" +
                $"Ngân sách:    {detail.Budget:N0} ₫\n" +
                $"Chi phí thực:  {totalExpense:N0} ₫\n" +
                $"Tiến độ:        {progress}%\n\n" +
                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                $"👥  Thành viên ({memberCount})\n" +
                (memberList.Count > 0 ? string.Join("\n", memberList) : "  Chưa có thành viên") +
                $"\n\n📋  Công việc: {taskCount} task" +
                (taskCount > 0 ? " (chi tiết sẽ có ở Giai đoạn 4)" : "");

            MessageBox.Show(info, "Chi tiết dự án", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ── Filter & Search ───────────────────────────────────
        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();
        private void cboFilterStatus_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();
        private async void btnRefresh_Click(object sender, EventArgs e) => await LoadProjectsAsync();

        /// <summary>Double-click mở form sửa (chỉ Manager/Admin).</summary>
        private void dgvProjects_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (AppSession.IsManager) btnEdit_Click(sender, e);
                else btnDetail_Click(sender, e); // Developer double-click → xem chi tiết
            }
        }

        private void SetStatus(string msg) => lblStatus.Text = msg;
    }
}

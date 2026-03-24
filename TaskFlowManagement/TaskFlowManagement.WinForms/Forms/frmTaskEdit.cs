using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Form Thêm mới / Sửa công việc (TaskItem).
    ///   taskId == null → chế độ Thêm mới
    ///   taskId có giá trị → chế độ Sửa
    ///
    /// DROPDOWN ASSIGNEE:
    ///   Load toàn bộ user active qua GetAllActiveUsersAsync() — không lọc theo ProjectMember.
    ///   Lý do: seed data có thể thiếu member trong ProjectMembers → lọc sẽ làm dropdown rỗng.
    ///   Admin/Manager tự chịu trách nhiệm chọn đúng người.
    ///
    /// PHÂN QUYỀN COMBOBOX (chế độ Sửa):
    ///   | Role                      | cboStatus | cboPriority | cboAssignee |
    ///   |---------------------------|-----------|-------------|-------------|
    ///   | Admin / Manager           |   ✅      |    ✅       |    ✅       |
    ///   | Developer (assignee)      |   ✅      |    ❌       |    ✅       |
    ///   | Developer (không assignee)|   ❌      |    ❌       |    ❌       |
    ///
    /// SO SÁNH ROLE: OrdinalIgnoreCase — DB seed "Admin"/"Manager"/"Developer"
    /// </summary>
    public partial class frmTaskEdit : Form
    {
        // ────────────────────────────────────────────────────
        // DEPENDENCIES & STATE
        // ────────────────────────────────────────────────────
        private readonly ITaskService    _taskService    = null!;
        private readonly IProjectService _projectService = null!;
        private readonly IUserService    _userService    = null!;

        private readonly int? _taskId;
        private TaskItem?     _editingTask;

        private List<Priority> _priorities = new();
        private List<Status>   _statuses   = new();
        private List<Category> _categories = new();
        private List<Project>  _projects   = new();
        private List<User>     _users      = new();

        // ────────────────────────────────────────────────────
        // CONSTRUCTORS
        // ────────────────────────────────────────────────────

        // Constructor mặc định — BẮT BUỘC để tab [Design] không lỗi
        public frmTaskEdit()
        {
            InitializeComponent();
        }

        // Constructor DI — dùng khi chạy thật
        public frmTaskEdit(
            ITaskService    taskService,
            IProjectService projectService,
            IUserService    userService,
            int?            taskId)
            : this()
        {
            _taskService    = taskService;
            _projectService = projectService;
            _userService    = userService;
            _taskId         = taskId;
        }

        // ────────────────────────────────────────────────────
        // FORM LOAD
        // ────────────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Text = _taskId.HasValue ? $"✏️ Sửa công việc (ID: {_taskId.Value})" : "➕ Thêm công việc mới";

            await LoadLookupsAsync();

            if (_taskId.HasValue)
                await LoadTaskForEditAsync(_taskId.Value);
            else
                SetDefaultsForNewTask();
        }

        // ────────────────────────────────────────────────────
        // LOAD LOOKUP DATA
        // ────────────────────────────────────────────────────

        private async Task LoadLookupsAsync()
        {
            try
            {
                // Load song song 5 nguồn
                var tPriorities = _taskService.GetAllPrioritiesAsync();
                var tStatuses   = _taskService.GetAllStatusesAsync();
                var tCategories = _taskService.GetAllCategoriesAsync();
                var tProjects   = _projectService.GetProjectsForUserAsync(
                                      AppSession.UserId, AppSession.IsManager);
                // Lấy TẤT CẢ user active — không lọc theo ProjectMember
                // để tránh dropdown rỗng khi seed data thiếu member
                var tUsers = _userService.GetAllActiveUsersAsync();

                await Task.WhenAll(tPriorities, tStatuses, tCategories, tProjects, tUsers);

                _priorities = tPriorities.Result;
                _statuses   = tStatuses.Result;
                _categories = tCategories.Result;
                _projects   = tProjects.Result;
                _users      = tUsers.Result;

                // cboPriority
                cboPriority.Items.Clear();
                cboPriority.Items.Add(new ComboItem(0, "— Chọn mức ưu tiên —"));
                foreach (var p in _priorities.OrderBy(p => p.Level))
                    cboPriority.Items.Add(new ComboItem(p.Id, p.Name));
                cboPriority.SelectedIndex = 0;

                // cboStatus — sắp theo DisplayOrder từ DB
                cboStatus.Items.Clear();
                cboStatus.Items.Add(new ComboItem(0, "— Chọn trạng thái —"));
                foreach (var s in _statuses.OrderBy(s => s.DisplayOrder))
                    cboStatus.Items.Add(new ComboItem(s.Id, s.Name));
                cboStatus.SelectedIndex = 0;

                // cboCategory
                cboCategory.Items.Clear();
                cboCategory.Items.Add(new ComboItem(0, "— Chọn phân loại —"));
                foreach (var c in _categories.OrderBy(c => c.Name))
                    cboCategory.Items.Add(new ComboItem(c.Id, c.Name));
                cboCategory.SelectedIndex = 0;

                // cboProject
                cboProject.Items.Clear();
                cboProject.Items.Add(new ComboItem(0, "— Chọn dự án —"));
                foreach (var proj in _projects)
                    cboProject.Items.Add(new ComboItem(proj.Id, proj.Name));
                cboProject.SelectedIndex = 0;

                // cboAssignee — toàn bộ user active (Developer + Manager đều có thể làm)
                cboAssignee.Items.Clear();
                cboAssignee.Items.Add(new ComboItem(0, "— Chưa gán —"));
                foreach (var u in _users.OrderBy(u => u.FullName))
                    cboAssignee.Items.Add(new ComboItem(u.Id, u.FullName));
                cboAssignee.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu dropdown:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ────────────────────────────────────────────────────
        // LOAD TASK ĐỂ SỬA
        // ────────────────────────────────────────────────────

        private async Task LoadTaskForEditAsync(int taskId)
        {
            _editingTask = await _taskService.GetByIdAsync(taskId);

            if (_editingTask == null)
            {
                MessageBox.Show("Không tìm thấy công việc này. Có thể đã bị xóa.",
                    "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            // ── Điền dữ liệu vào form ────────────────────────
            txtTitle.Text           = _editingTask.Title;
            txtDescription.Text     = _editingTask.Description ?? string.Empty;
            numProgress.Value       = _editingTask.ProgressPercent;
            numEstimatedHours.Value = (decimal)(_editingTask.EstimatedHours ?? 0);

            if (_editingTask.DueDate.HasValue)
            {
                chkHasDueDate.Checked = true;
                dtpDueDate.Value      = _editingTask.DueDate.Value.ToLocalTime();
                dtpDueDate.Enabled    = true;
            }
            else
            {
                chkHasDueDate.Checked = false;
                dtpDueDate.Enabled    = false;
            }

            // ── Chọn đúng item ComboBox ──────────────────────
            // ProjectId, PriorityId, StatusId, CategoryId: int (non-nullable) → truyền thẳng
            // AssignedToId: int? (nullable) → cần ?? 0
            SelectComboById(cboProject,  _editingTask.ProjectId);
            SelectComboById(cboPriority, _editingTask.PriorityId);
            SelectComboById(cboStatus,   _editingTask.StatusId);
            SelectComboById(cboCategory, _editingTask.CategoryId);
            SelectComboById(cboAssignee, _editingTask.AssignedToId ?? 0);

            // ── PHÂN QUYỀN ENABLE/DISABLE ────────────────────
            //
            // AppSession.IsManager trả true khi role là "Admin" HOẶC "Manager"
            // (kiểm tra ở AppSession — đã dùng Contains với IgnoreCase)
            //
            // Nhưng để chắc chắn, ta tự kiểm tra lại bằng OrdinalIgnoreCase:
            bool isManagerOrAbove = AppSession.Roles.Any(
                r => r.Equals("Admin",   StringComparison.OrdinalIgnoreCase) ||
                     r.Equals("Manager", StringComparison.OrdinalIgnoreCase));

            // Developer đang là Assignee của task này không?
            bool isAssignee = _editingTask.AssignedToId.HasValue
                           && _editingTask.AssignedToId.Value == AppSession.UserId;

            // cboStatus:   Admin/Manager tự do | Developer chỉ khi là assignee
            cboStatus.Enabled   = isManagerOrAbove || isAssignee;

            // cboPriority: CHỈ Admin/Manager — Developer không quyết định độ ưu tiên
            cboPriority.Enabled = isManagerOrAbove;

            // cboAssignee: Admin/Manager tự do | Developer chỉ khi là assignee
            cboAssignee.Enabled = isManagerOrAbove || isAssignee;
        }

        // ────────────────────────────────────────────────────
        // MẶC ĐỊNH KHI THÊM MỚI
        // ────────────────────────────────────────────────────

        private void SetDefaultsForNewTask()
        {
            // Seed: Priority Id=1 Low | Id=2 Medium | Id=3 High | Id=4 Critical
            // index 0="— Chọn —", index 1=Low, index 2=Medium → chọn Medium làm default
            if (cboPriority.Items.Count > 2)
                cboPriority.SelectedIndex = 2; // Medium

            // Seed: Status Id=1 "CREATED" — đầu tiên trong danh sách (DisplayOrder=0)
            // index 0="— Chọn —", index 1=CREATED
            if (cboStatus.Items.Count > 1)
                cboStatus.SelectedIndex = 1; // CREATED

            chkHasDueDate.Checked = false;
            dtpDueDate.Enabled    = false;
            dtpDueDate.Value      = DateTime.Now.AddDays(7);

            // Khi tạo mới: Enable tất cả (người tạo được quyết định hết)
            // Ngoại lệ: Developer không set Priority
            bool isManagerOrAbove = AppSession.Roles.Any(
                r => r.Equals("Admin",   StringComparison.OrdinalIgnoreCase) ||
                     r.Equals("Manager", StringComparison.OrdinalIgnoreCase));

            cboStatus.Enabled   = true;
            cboPriority.Enabled = isManagerOrAbove; // Developer không set Priority
            cboAssignee.Enabled = true;
        }

        // ────────────────────────────────────────────────────
        // SỰ KIỆN
        // ────────────────────────────────────────────────────

        private void chkHasDueDate_CheckedChanged(object sender, EventArgs e)
            => dtpDueDate.Enabled = chkHasDueDate.Checked;

        private void numProgress_ValueChanged(object sender, EventArgs e)
            => chkIsCompleted.Checked = numProgress.Value == 100;

        // ────────────────────────────────────────────────────
        // NÚT LƯU
        // ────────────────────────────────────────────────────

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            btnSave.Enabled = false;
            btnSave.Text    = "Đang lưu...";

            try
            {
                var task      = BuildTaskFromForm();
                var (ok, msg) = _taskId.HasValue
                    ? await _taskService.UpdateTaskAsync(task)
                    : await _taskService.CreateTaskAsync(task);

                if (ok)
                {
                    MessageBox.Show(msg, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(msg, "Không thể lưu",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không mong đợi:\n" + ex.Message,
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text    = "💾 Lưu";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ────────────────────────────────────────────────────
        // VALIDATE
        // ────────────────────────────────────────────────────

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề công việc.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }
            if (txtTitle.Text.Trim().Length > 200)
            {
                MessageBox.Show("Tiêu đề không được dài hơn 200 ký tự.",
                    "Tiêu đề quá dài", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }
            if (GetComboId(cboProject) <= 0)
            {
                MessageBox.Show("Vui lòng chọn dự án cho công việc.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboProject.Focus();
                return false;
            }
            if (GetComboId(cboPriority) <= 0)
            {
                MessageBox.Show("Vui lòng chọn mức độ ưu tiên.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPriority.Focus();
                return false;
            }
            if (GetComboId(cboStatus) <= 0)
            {
                MessageBox.Show("Vui lòng chọn trạng thái công việc.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboStatus.Focus();
                return false;
            }
            return true;
        }

        // ────────────────────────────────────────────────────
        // BUILD ENTITY TỪ FORM
        // ────────────────────────────────────────────────────

        private TaskItem BuildTaskFromForm()
        {
            // Sửa → dùng lại entity gốc (giữ CreatedAt, CreatedById, Reviewer, Tester...)
            // Thêm mới → entity rỗng
            var task = _editingTask ?? new TaskItem();

            task.Title       = txtTitle.Text.Trim();
            task.Description = string.IsNullOrWhiteSpace(txtDescription.Text)
                                   ? null
                                   : txtDescription.Text.Trim();

            // Các FK int (non-nullable) — gán trực tiếp từ ComboBox Id
            task.ProjectId  = GetComboId(cboProject);
            task.PriorityId = GetComboId(cboPriority);
            task.StatusId   = GetComboId(cboStatus);   // ← gán đây, UpdateTaskAsync sẽ
                                                        //   gọi UpdateStatusAsync riêng để chắc
            task.CategoryId = GetComboId(cboCategory);

            // AssignedToId là int? — Id=0 (placeholder "Chưa gán") → null
            var assigneeId    = GetComboId(cboAssignee);
            task.AssignedToId = assigneeId > 0 ? assigneeId : null;

            task.ProgressPercent = (byte)numProgress.Value;
            task.IsCompleted     = chkIsCompleted.Checked;
            task.EstimatedHours  = numEstimatedHours.Value > 0
                                       ? (decimal?)numEstimatedHours.Value
                                       : null;
            task.DueDate = chkHasDueDate.Checked
                               ? dtpDueDate.Value.ToUniversalTime()
                               : null;

            // Chỉ gán khi thêm mới — không ghi đè khi sửa
            if (!_taskId.HasValue)
            {
                task.CreatedById = AppSession.UserId;
                task.CreatedAt   = DateTime.UtcNow;
            }

            return task;
        }

        // ────────────────────────────────────────────────────
        // HELPERS
        // ────────────────────────────────────────────────────

        /// <summary>Lấy Id từ ComboBox; trả 0 nếu chưa chọn (placeholder).</summary>
        private static int GetComboId(ComboBox cbo)
            => cbo.SelectedItem is ComboItem ci ? ci.Id : 0;

        /// <summary>
        /// Chọn item trong ComboBox theo Id.
        /// Nhận int (non-nullable) — gọi với ?? 0 cho int? (AssignedToId).
        /// </summary>
        private static void SelectComboById(ComboBox cbo, int id)
        {
            if (id <= 0) return;
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                if (cbo.Items[i] is ComboItem ci && ci.Id == id)
                {
                    cbo.SelectedIndex = i;
                    return;
                }
            }
        }
    }
}

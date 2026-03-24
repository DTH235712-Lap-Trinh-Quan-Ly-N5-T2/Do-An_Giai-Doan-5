using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;

namespace TaskFlowManagement.WinForms.Forms
{
    /// <summary>
    /// Form "Công việc của tôi" — hiển thị task liên quan đến user đang đăng nhập.
    ///
    /// Chia 3 tab:
    ///   - Tab 1 "Được giao cho tôi" : task mà AssignedToId = UserId hiện tại
    ///   - Tab 2 "Tôi cần Review"    : task mà Reviewer1Id hoặc Reviewer2Id = UserId
    ///   - Tab 3 "Tôi cần Test"      : task mà TesterId = UserId
    ///
    /// Developer chỉ thấy tab 1.
    /// Manager/Admin thấy cả 3 tab.
    /// </summary>
    public partial class frmMyTasks : Form
    {
        // ────────────────────────────────────────────────────────
        // DEPENDENCIES
        // ────────────────────────────────────────────────────────
        private readonly ITaskService _taskService = null!;
        private readonly IProjectService _projectService = null!;  // FIX: thêm field để dùng trong double-click
        private readonly IUserService _userService = null!;   // FIX: thêm field để dùng trong double-click

        // ────────────────────────────────────────────────────────
        // CONSTRUCTORS
        // ────────────────────────────────────────────────────────

        /// <summary>
        /// Constructor mặc định — BẮT BUỘC để tab [Design] không lỗi.
        /// </summary>
        public frmMyTasks()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor DI — ServiceProvider gọi cái này khi chạy thật.
        /// FIX: Thêm IProjectService và IUserService để mở frmTaskEdit khi double-click.
        /// Không cần dùng form.Tag nữa — inject thẳng vào constructor sạch hơn.
        /// </summary>
        public frmMyTasks(ITaskService taskService, IProjectService projectService, IUserService userService)
            : this()
        {
            _taskService = taskService;
            _projectService = projectService;
            _userService = userService;
        }

        // ────────────────────────────────────────────────────────
        // FORM LOAD
        // ────────────────────────────────────────────────────────

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Text = $"📋 Công việc của tôi — {AppSession.FullName}";
            lblUser.Text = $"Đang hiển thị công việc của: {AppSession.FullName} ({AppSession.Username})";

            ApplyRolePermissions();
            await LoadAllTabsAsync();
        }

        // ────────────────────────────────────────────────────────
        // PHÂN QUYỀN THEO ROLE
        // ────────────────────────────────────────────────────────

        private void ApplyRolePermissions()
        {
            // Developer không thực hiện Review/Test → ẩn 2 tab đó
            bool canReviewOrTest = AppSession.IsManager;
            tabReview.Parent = canReviewOrTest ? tabControl : null;
            tabTesting.Parent = canReviewOrTest ? tabControl : null;
        }

        // ────────────────────────────────────────────────────────
        // LOAD DỮ LIỆU CẢ 3 TAB
        // ────────────────────────────────────────────────────────

        private async Task LoadAllTabsAsync()
        {
            lblStatus.Text = "Đang tải...";

            try
            {
                // Load song song 4 nguồn để tiết kiệm thời gian chờ
                var tMine = _taskService.GetMyTasksAsync(AppSession.UserId);
                var tReview1 = _taskService.GetTasksForReviewer1Async(AppSession.UserId);
                var tReview2 = _taskService.GetTasksForReviewer2Async(AppSession.UserId);
                var tTest = _taskService.GetTasksForTesterAsync(AppSession.UserId);

                await Task.WhenAll(tMine, tReview1, tReview2, tTest);

                // Gộp Review1 + Review2 vào 1 tab
                var reviewTasks = tReview1.Result.Concat(tReview2.Result).ToList();

                BindGrid(dgvMyTasks, tMine.Result);
                BindGrid(dgvReview, reviewTasks);
                BindGrid(dgvTesting, tTest.Result);

                // Cập nhật số lượng task lên tiêu đề tab
                tabMyTasks.Text = $"📋 Được giao ({tMine.Result.Count})";
                tabReview.Text = $"🔍 Review ({reviewTasks.Count})";
                tabTesting.Text = $"🧪 Testing ({tTest.Result.Count})";

                int total = tMine.Result.Count + reviewTasks.Count + tTest.Result.Count;
                lblStatus.Text = $"Tổng cộng {total} công việc liên quan đến bạn.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi tải dữ liệu.";
                MessageBox.Show("Không thể tải dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ────────────────────────────────────────────────────────
        // BIND DỮ LIỆU VÀO DATAGRIDVIEW
        // ────────────────────────────────────────────────────────

        private static void BindGrid(DataGridView dgv, List<TaskItem> items)
        {
            dgv.Rows.Clear();

            foreach (var t in items)
            {
                var due = t.DueDate.HasValue
                    ? t.DueDate.Value.ToLocalTime().ToString("dd/MM/yyyy")
                    : "—";

                // Thứ tự cột: colId | colTitle | colProject | colPriority | colStatus | colProgress | colDueDate
                int idx = dgv.Rows.Add(
                    t.Id,
                    t.Title,
                    t.Project?.Name ?? "—",
                    t.Priority?.Name ?? "—",
                    t.Status?.Name ?? "—",
                    $"{t.ProgressPercent}%",
                    due);

                ApplyRowColor(dgv.Rows[idx], t);
            }
        }

        private static void ApplyRowColor(DataGridViewRow row, TaskItem task)
        {
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
        // NÚT LÀM MỚI
        // ────────────────────────────────────────────────────────

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadAllTabsAsync();
        }

        // ────────────────────────────────────────────────────────
        // MỞ FORM SỬA KHI DOUBLE-CLICK
        // ────────────────────────────────────────────────────────

        /// <summary>
        /// Double-click vào bất kỳ DataGridView nào → mở frmTaskEdit để xem/sửa chi tiết.
        /// FIX: Dùng _projectService và _userService đã inject vào constructor,
        ///      không cần truyền qua form.Tag nữa → sạch hơn, không còn lỗi thiếu tham số.
        /// </summary>
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (sender is not DataGridView dgv) return;

            var cell = dgv.Rows[e.RowIndex].Cells["colId"].Value;
            if (cell == null) return;

            int taskId = (int)cell;

            // FIX: truyền đủ 4 tham số theo constructor mới của frmTaskEdit
            using var dlg = new frmTaskEdit(_taskService, _projectService, _userService, taskId);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                _ = LoadAllTabsAsync(); // Reload sau khi sửa xong
        }
    }
}
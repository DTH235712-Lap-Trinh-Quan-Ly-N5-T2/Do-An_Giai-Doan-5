using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces.Services;
using TaskFlowManagement.WinForms.Common;
using TaskFlowManagement.WinForms.Controls;

namespace TaskFlowManagement.WinForms.Forms
{
    public partial class frmKanban : Form
    {
        private const int StatusCreated = 1;
        private const int StatusAssigned = 2;
        private const int StatusInProgress = 3;
        private const int StatusFailed = 4;
        private const int StatusReview1 = 5;
        private const int StatusReview2 = 6;
        private const int StatusApproved = 7;
        private const int StatusInTest = 8;
        private const int StatusResolved = 9;
        private const int StatusClosed = 10;

        private readonly ITaskService? _taskService;
        private readonly IProjectService? _projectService;
        private readonly IUserService? _userService;
        private readonly int _projectId;

        // Constructor mặc định bắt buộc để WinForms Designer mở được.
        public frmKanban()
        {
            InitializeComponent();
            InitializeForm();
        }

        public frmKanban(ITaskService taskService, IProjectService projectService, IUserService userService, int projectId)
            : this()
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _projectId = projectId;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_taskService == null || DesignMode)
            {
                return;
            }

            await LoadTasksAsync();
        }

        private void InitializeForm()
        {
            Text = $"Kanban - Project #{_projectId}";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.WhiteSmoke;
            MinimumSize = new Size(1000, 650);
        }

        private async Task LoadTasksAsync()
        {
            ClearAllColumns();

            try
            {
                // Gọi thẳng Service đã có sẵn logic Sắp xếp (Sort)
                var tasks = await _taskService!.GetBoardTasksAsync(_projectId);

                foreach (var task in tasks)
                {
                    var card = new ucTaskCard
                    {
                        Margin = new Padding(6)
                    };

                    card.Bind(task);
                    card.StatusChanged += TaskCard_StatusChanged;
                    card.CardDoubleClicked += TaskCard_DoubleClicked;
                    MoveCardToStatusPanel(card, task.StatusId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải Kanban board:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ClearAllColumns()
        {
            foreach (var panel in GetAllColumns())
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is ucTaskCard card)
                    {
                        card.StatusChanged -= TaskCard_StatusChanged;
                    }
                }
                panel.Controls.Clear();
            }
        }

        private async Task<List<TaskItem>> GetProjectTasksAsync(int projectId)
        {
            const int pageSize = 200;
            var page = 1;
            var result = new List<TaskItem>();

            while (true)
            {
                var (items, totalCount) = await _taskService!.GetPagedAsync(
                    page: page,
                    pageSize: pageSize,
                    projectId: projectId);

                if (items.Count == 0)
                {
                    break;
                }

                result.AddRange(items);
                if (result.Count >= totalCount || items.Count < pageSize)
                {
                    break;
                }

                page++;
            }

            return result;
        }

        private async void TaskCard_StatusChanged(object? sender, StatusChangedEventArgs e)
        {
            if (sender is not ucTaskCard card)
            {
                return;
            }

            try
            {
                var (success, message) = await UpdateTaskStatusAsync(e.TaskId, e.NewStatusId);
                if (!success)
                {
                    MessageBox.Show(
                        message,
                        "Không thể chuyển trạng thái",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (card.Parent is FlowLayoutPanel currentParent)
                {
                    currentParent.Controls.Remove(card);
                }

                MoveCardToStatusPanel(card, e.NewStatusId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi cập nhật trạng thái:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task<(bool Success, string Message)> UpdateTaskStatusAsync(int taskId, int newStatusId)
        {
            return await _taskService!.UpdateStatusAsync(
                taskId,
                newStatusId,
                AppSession.UserId,
                AppSession.Roles);
        }

        private void MoveCardToStatusPanel(ucTaskCard card, int statusId)
        {
            var targetPanel = GetPanelByStatusId(statusId);
            if (targetPanel == null)
            {
                return;
            }

            targetPanel.Controls.Add(card);
        }

        private DoubleBufferedFlowLayoutPanel? GetPanelByStatusId(int statusId)
        {
            return statusId switch
            {
                StatusCreated or StatusAssigned => flpTodo,
                StatusInProgress => flpInProgress,
                StatusReview1 or StatusReview2 => flpReview,
                StatusInTest => flpTesting,
                StatusFailed => flpFailed,
                StatusApproved or StatusResolved or StatusClosed => flpDone,
                _ => null
            };
        }

        private IEnumerable<DoubleBufferedFlowLayoutPanel> GetAllColumns()
        {
            yield return flpTodo;
            yield return flpInProgress;
            yield return flpReview;
            yield return flpTesting;
            yield return flpFailed;
            yield return flpDone;
        }

        private async void TaskCard_DoubleClicked(object? sender, int taskId)
        {
            try
            {
                // TRUYỀN ĐỦ 4 THAM SỐ VÀO ĐÂY:
                using var detailForm = new frmTaskEdit(_taskService!, _projectService!, _userService!, taskId);

                var result = detailForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    await LoadTasksAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở chi tiết: " + ex.Message, "Lỗi");
            }
        }
    }

    public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
    {
        public DoubleBufferedFlowLayoutPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}

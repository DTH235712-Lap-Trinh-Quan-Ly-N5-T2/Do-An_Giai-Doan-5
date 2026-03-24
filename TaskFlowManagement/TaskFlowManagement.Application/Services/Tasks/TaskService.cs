using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;

// Namespace "Tasks" (số nhiều) vì "Task" trùng System.Threading.Tasks.Task
namespace TaskFlowManagement.Core.Services.Tasks
{
    /// <summary>
    /// Triển khai ITaskService cho GD4.
    ///
    /// THỰC TẾ SEED DATA (LookupSeeder.cs — thứ tự insert = Id):
    ///   Roles:      Id=1 "Admin" | Id=2 "Manager" | Id=3 "Developer"
    ///   Statuses:   Id=1 "CREATED" | Id=2 "ASSIGNED" | Id=3 "IN-PROGRESS" | Id=4 "FAILED"
    ///               Id=5 "REVIEW-1" | Id=6 "REVIEW-2" | Id=7 "APPROVED" | Id=8 "IN-TEST"
    ///               Id=9 "RESOLVED" | Id=10 "CLOSED"
    ///   Priorities: Id=1 "Low" | Id=2 "Medium" | Id=3 "High" | Id=4 "Critical"
    ///
    /// PHÂN QUYỀN UpdateStatusAsync:
    ///   Admin / Manager → bỏ qua workflow, đổi sang BẤT KỲ status nào
    ///   Developer       → chỉ được đổi task được giao cho mình (AssignedToId == UserId)
    ///
    /// SO SÁNH ROLE: dùng OrdinalIgnoreCase để tránh lỗi casing
    ///   DB seed: "Admin", "Manager", "Developer" (hoa chữ cái đầu)
    ///   AppSession.Roles có thể chứa bất kỳ casing nào → luôn dùng IgnoreCase
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;

        public TaskService(ITaskRepository taskRepo) => _taskRepo = taskRepo;

        // ══════════════════════════════════════════════════════
        // TRUY VẤN — giữ nguyên 100%
        // ══════════════════════════════════════════════════════

        public Task<TaskItem?> GetByIdAsync(int taskId)
            => _taskRepo.GetByIdWithDetailsAsync(taskId);

        public Task<(List<TaskItem> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize,
            int? projectId = null, int? assignedToId = null,
            int? statusId  = null, int? priorityId   = null,
            int? categoryId = null, string? keyword   = null)
            => _taskRepo.GetPagedAsync(page, pageSize,
                projectId, assignedToId, statusId, priorityId, categoryId, keyword);

        public Task<List<TaskItem>> GetMyTasksAsync(int userId)
            => _taskRepo.GetAssignedToUserAsync(userId);

        public Task<List<TaskItem>> GetTasksForReviewer1Async(int reviewerId)
            => _taskRepo.GetByReviewer1Async(reviewerId);

        public Task<List<TaskItem>> GetTasksForReviewer2Async(int reviewerId)
            => _taskRepo.GetByReviewer2Async(reviewerId);

        public Task<List<TaskItem>> GetTasksForTesterAsync(int testerId)
            => _taskRepo.GetByTesterAsync(testerId);

        public Task<List<TaskItem>> GetOverdueTasksAsync()
            => _taskRepo.GetOverdueAsync();

        public Task<List<TaskItem>> GetDueSoonTasksAsync(int days = 7)
            => _taskRepo.GetDueSoonAsync(days);

        public Task<Dictionary<string, int>> GetStatusSummaryAsync(int projectId)
            => _taskRepo.GetStatusSummaryByProjectAsync(projectId);

        // ══════════════════════════════════════════════════════
        // CRUD
        // ══════════════════════════════════════════════════════

        public async Task<(bool Success, string Message)> CreateTaskAsync(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                return (false, "Tiêu đề công việc không được để trống.");
            if (task.Title.Length > 200)
                return (false, "Tiêu đề không được vượt quá 200 ký tự.");
            if (task.ProjectId <= 0)
                return (false, "Vui lòng chọn dự án cho công việc.");
            if (task.PriorityId <= 0)
                return (false, "Vui lòng chọn mức độ ưu tiên.");
            if (task.StatusId <= 0)
                return (false, "Vui lòng chọn trạng thái.");
            if (task.CategoryId <= 0)
                return (false, "Vui lòng chọn loại công việc.");
            if (task.EstimatedHours.HasValue && task.EstimatedHours.Value <= 0)
                return (false, "Số giờ ước tính phải lớn hơn 0.");
            if (task.DueDate.HasValue && task.DueDate.Value < DateTime.UtcNow.AddDays(-1))
                return (false, "Hạn chót không được là ngày trong quá khứ.");

            task.Title       = task.Title.Trim();
            task.Description = string.IsNullOrWhiteSpace(task.Description)
                ? null : task.Description.Trim();

            await _taskRepo.AddAsync(task);
            return (true, $"Đã tạo công việc \"{task.Title}\" thành công.");
        }

        /// <summary>
        /// Cập nhật task — gọi 2 method Repository riêng biệt để tránh EF tracking conflict:
        ///
        ///   1. UpdateAsync(task)
        ///      Dùng Attach + State.Modified để cập nhật các trường nội dung
        ///      (Title, Description, AssignedToId, PriorityId, DueDate, v.v.)
        ///      Tuy nhiên khi entity load bằng AsNoTracking có navigation property
        ///      (task.Status, task.Priority là object), EF đôi khi không ghi FK đúng.
        ///
        ///   2. UpdateStatusAsync(task.Id, task.StatusId)
        ///      Dùng ExecuteUpdateAsync — sinh SQL "SET StatusId=? WHERE Id=?" trực tiếp,
        ///      không phụ thuộc vào tracking state → đảm bảo StatusId luôn được ghi đúng.
        ///
        /// Tương tự: AssignedToId cũng được UpdateAsync ghi đúng vì là FK thuần (int?),
        /// không có navigation conflict phức tạp như StatusId.
        /// </summary>
        public async Task<(bool Success, string Message)> UpdateTaskAsync(TaskItem task)
        {
            // 1. Validation cơ bản
            if (string.IsNullOrWhiteSpace(task.Title))
                return (false, "Tiêu đề công việc không được để trống.");

            try
            {
                // 2. Làm sạch dữ liệu
                task.Title = task.Title.Trim();
                task.Description = string.IsNullOrWhiteSpace(task.Description) ? null : task.Description.Trim();

                // 3. CẬP NHẬT TỔNG THỂ (Quan trọng)
                // Thay vì gọi 2 hàm tách biệt, hãy dùng UpdateAsync để EF tự quản lý 
                // hoặc kiểm tra lại hàm UpdateStatusAsync trong Repository của bạn.

                await _taskRepo.UpdateAsync(task);

                // Đảm bảo StatusId được cập nhật cuối cùng nếu UpdateAsync gặp vấn đề với Navigation Property
                await _taskRepo.UpdateStatusAsync(task.Id, task.StatusId);

                return (true, "Cập nhật công việc thành công.");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi khi lưu vào Database: " + ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> DeleteTaskAsync(int taskId)
        {
            var task = await _taskRepo.GetByIdWithDetailsAsync(taskId);
            if (task == null)
                return (false, "Không tìm thấy công việc.");
            if (task.SubTasks.Count > 0)
                return (false,
                    $"Không thể xóa vì công việc này có {task.SubTasks.Count} công việc con. " +
                    "Hãy xóa các công việc con trước.");

            await _taskRepo.DeleteAsync(taskId);
            return (true, $"Đã xóa công việc \"{task.Title}\".");
        }

        // ══════════════════════════════════════════════════════
        // CẬP NHẬT TIẾN ĐỘ
        // ══════════════════════════════════════════════════════

        public async Task<(bool Success, string Message)> UpdateProgressAsync(
            int taskId, byte progress)
        {
            if (progress > 100)
                return (false, "Tiến độ phải từ 0 đến 100.");

            // Lookup Id của RESOLVED từ DB — không hard-code
            // Theo seed: RESOLVED = DisplayOrder 8, thứ 9 trong list → Id=9
            var statuses       = await _taskRepo.GetAllStatusesAsync();
            var resolvedStatus = statuses.FirstOrDefault(
                s => s.Name.Equals("RESOLVED", StringComparison.OrdinalIgnoreCase));

            if (resolvedStatus == null)
                return (false, "Không tìm thấy trạng thái RESOLVED trong hệ thống.");

            await _taskRepo.UpdateProgressAsync(taskId, progress, resolvedStatus.Id);

            return progress == 100
                ? (true, "🎉 Công việc đã hoàn thành! Trạng thái chuyển sang RESOLVED.")
                : (true, $"Đã cập nhật tiến độ {progress}%.");
        }

        // ══════════════════════════════════════════════════════
        // CẬP NHẬT TRẠNG THÁI — PHÂN QUYỀN LINH HOẠT
        // ══════════════════════════════════════════════════════

        /// <summary>
        /// Chuyển trạng thái workflow với phân quyền linh hoạt.
        ///
        /// PHÂN QUYỀN:
        ///   Admin / Manager → được đổi sang BẤT KỲ trong 10 status, bỏ qua workflow.
        ///   Developer       → chỉ được đổi task được giao cho mình (AssignedToId == UserId).
        ///
        /// THAM SỐ statusId:
        ///   Truyền Id trực tiếp từ ComboBox (KHÔNG dùng tên string) để tránh lỗi
        ///   so khớp tên khi DB seed hoặc casing thay đổi.
        ///
        /// SO SÁNH ROLE (quan trọng):
        ///   DB seed: "Admin", "Manager", "Developer"
        ///   AppSession.Roles: List&lt;string&gt; — dùng OrdinalIgnoreCase để không lỗi
        ///   nếu source nào đó trả về "admin", "MANAGER", "developer"...
        /// </summary>
        public async Task<(bool Success, string Message)> UpdateStatusAsync(
            int           taskId,
            int           statusId,
            int           requesterId,
            IList<string> requesterRoles)
        {
            // ── Validate statusId ─────────────────────────────
            if (statusId <= 0)
                return (false, "Vui lòng chọn trạng thái hợp lệ.");

            var statuses = await _taskRepo.GetAllStatusesAsync();
            var target   = statuses.FirstOrDefault(s => s.Id == statusId);
            if (target == null)
                return (false, $"Trạng thái không tồn tại (Id={statusId}).");

            // ── Admin/Manager: bỏ qua mọi kiểm tra ──────────
            // So sánh OrdinalIgnoreCase: "Admin"/"admin"/"ADMIN" đều match
            bool isManagerOrAbove =
                requesterRoles.Any(r => r.Equals("Admin",   StringComparison.OrdinalIgnoreCase)) ||
                requesterRoles.Any(r => r.Equals("Manager", StringComparison.OrdinalIgnoreCase));

            if (isManagerOrAbove)
            {
                // ExecuteUpdateAsync: SET StatusId=? WHERE Id=? — không cần load entity
                await _taskRepo.UpdateStatusAsync(taskId, statusId);
                return (true, $"Đã chuyển trạng thái sang \"{target.Name}\".");
            }

            // ── Developer: phải load task để kiểm tra quyền ─
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null)
                return (false, "Không tìm thấy công việc.");

            bool isAssignee = task.AssignedToId.HasValue
                           && task.AssignedToId.Value == requesterId;

            if (!isAssignee)
                return (false,
                    "Bạn chỉ có thể thay đổi trạng thái công việc được giao cho mình.\n" +
                    "Liên hệ Manager nếu cần thay đổi task khác.");

            await _taskRepo.UpdateStatusAsync(taskId, statusId);
            return (true, $"Đã chuyển trạng thái sang \"{target.Name}\".");
        }

        // ══════════════════════════════════════════════════════
        // ASSIGN & TRANSITION
        // ══════════════════════════════════════════════════════

        public async Task<(bool Success, string Message)> AssignAndTransitionAsync(
            int taskId, string newStatus,
            int? reviewer1Id = null, int? reviewer2Id = null, int? testerId = null)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
                return (false, "Tên trạng thái không được để trống.");

            var statuses = await _taskRepo.GetAllStatusesAsync();
            // OrdinalIgnoreCase cho AssignAndTransition vì caller truyền tên string
            var target   = statuses.FirstOrDefault(
                s => s.Name.Equals(newStatus, StringComparison.OrdinalIgnoreCase));

            if (target == null)
                return (false, $"Trạng thái \"{newStatus}\" không hợp lệ.");

            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null)
                return (false, "Không tìm thấy công việc.");

            if (reviewer1Id.HasValue && reviewer1Id.Value <= 0)
                return (false, "Reviewer lần 1 không hợp lệ.");
            if (reviewer2Id.HasValue && reviewer2Id.Value <= 0)
                return (false, "Reviewer lần 2 không hợp lệ.");
            if (testerId.HasValue && testerId.Value <= 0)
                return (false, "Tester không hợp lệ.");

            await _taskRepo.AssignReviewerAsync(
                taskId, reviewer1Id, reviewer2Id, testerId, target.Id);

            var parts = new List<string>();
            if (reviewer1Id.HasValue) parts.Add("Reviewer 1");
            if (reviewer2Id.HasValue) parts.Add("Reviewer 2");
            if (testerId.HasValue)    parts.Add("Tester");

            var extra = parts.Count > 0 ? $" — đã gán {string.Join(", ", parts)}" : string.Empty;
            return (true, $"Chuyển sang {newStatus}{extra} thành công.");
        }

        // ══════════════════════════════════════════════════════
        // LOOKUP
        // ══════════════════════════════════════════════════════

        public Task<List<Status>>   GetAllStatusesAsync()   => _taskRepo.GetAllStatusesAsync();
        public Task<List<Priority>> GetAllPrioritiesAsync() => _taskRepo.GetAllPrioritiesAsync();
        public Task<List<Category>> GetAllCategoriesAsync() => _taskRepo.GetAllCategoriesAsync();
    }
}

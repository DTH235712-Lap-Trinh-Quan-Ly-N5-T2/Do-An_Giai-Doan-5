using Microsoft.EntityFrameworkCore;
using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Infrastructure.Data;

namespace TaskFlowManagement.Infrastructure.Repositories
{
    /// <summary>
    /// Triển khai ITaskRepository cho GD4.
    ///
    /// Kỹ thuật áp dụng:
    ///   - IDbContextFactory: mỗi method tạo DbContext riêng, dispose ngay sau khi xong
    ///   - AsNoTracking(): mặc định cho tất cả SELECT (WinForms không cần change tracking)
    ///   - ExecuteUpdateAsync(): update đúng cột cần thiết, không SELECT-then-UPDATE
    ///   - IQueryable filter chain: GetPagedAsync xây WHERE tích lũy trước khi execute
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TaskRepository(IDbContextFactory<AppDbContext> contextFactory)
            => _contextFactory = contextFactory;

        // ════════════════════════════════════════════════════════
        // CRUD CƠ BẢN
        // ════════════════════════════════════════════════════════

        /// <summary>Lấy task theo ID, kèm các navigation cơ bản để hiển thị danh sách.</summary>
        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Category)
                .Include(t => t.AssignedTo)
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Lấy task kèm TẤT CẢ navigation properties.
        /// Dùng cho frmTaskEdit — cần hiển thị SubTasks, Reviewer, Tester, Comments, Attachments.
        /// </summary>
        public async Task<TaskItem?> GetByIdWithDetailsAsync(int taskId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Category)
                .Include(t => t.Project)
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .Include(t => t.Reviewer1)
                .Include(t => t.Reviewer2)
                .Include(t => t.Tester)
                .Include(t => t.ParentTask)
                .Include(t => t.SubTasks).ThenInclude(s => s.Status)
                .Include(t => t.SubTasks).ThenInclude(s => s.AssignedTo)
                .Include(t => t.Comments).ThenInclude(c => c.User)
                .Include(t => t.Attachments).ThenInclude(a => a.UploadedBy)
                .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        /// <summary>
        /// Lấy tất cả task — chỉ dùng cho dataset nhỏ (export, báo cáo).
        /// Với UI danh sách → dùng GetPagedAsync.
        /// </summary>
        public async Task<List<TaskItem>> GetAllAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.AssignedTo)
                .Include(t => t.Project)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        /// <summary>Thêm task mới — set CreatedAt, UpdatedAt tự động.</summary>
        public async Task AddAsync(TaskItem task)
        {
            using var ctx = _contextFactory.CreateDbContext();
            task.CreatedAt = DateTime.UtcNow;
            task.UpdatedAt = DateTime.UtcNow;
            await ctx.TaskItems.AddAsync(task);
            await ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Cập nhật task — bảo vệ CreatedAt không bị ghi đè.
        /// Dùng Attach + State.Modified + IsModified = false cho cột cần bảo vệ.
        /// </summary>
        public async Task UpdateAsync(TaskItem task)
        {
            using var ctx = _contextFactory.CreateDbContext();

            // Đánh dấu toàn bộ đối tượng là Modified để EF sinh SQL UPDATE cho tất cả các cột
            ctx.Entry(task).State = EntityState.Modified;

            // Nếu bạn muốn bảo vệ cột CreatedAt không bị ghi đè (thường là null khi từ Form gửi về)
            ctx.Entry(task).Property(x => x.CreatedAt).IsModified = false;
            ctx.Entry(task).Property(x => x.CreatedById).IsModified = false;

            await ctx.SaveChangesAsync();
        }

        /// <summary>Xóa task (hard delete) — cascade xóa Comments, Attachments, TaskTags.</summary>
        public async Task DeleteAsync(int id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            await ctx.TaskItems.Where(t => t.Id == id).ExecuteDeleteAsync();
        }

        // ════════════════════════════════════════════════════════
        // PHÂN TRANG + LỌC ĐA TIÊU CHÍ
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// Phân trang + lọc (IQueryable filter chain).
        ///
        /// Kỹ thuật: xây IQueryable trước, WHERE tích lũy từng bước nếu có giá trị,
        /// chỉ Execute 1 lần duy nhất → SQL sinh ra đúng mệnh đề WHERE cần thiết.
        /// </summary>
        public async Task<(List<TaskItem> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            int? projectId = null,
            int? assignedToId = null,
            int? statusId = null,
            int? priorityId = null,
            int? categoryId = null,
            string? searchKeyword = null)
        {
            using var ctx = _contextFactory.CreateDbContext();

            // Bắt đầu với query base — chưa execute (deferred execution)
            var query = ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Category)
                .Include(t => t.AssignedTo)
                .Include(t => t.Project)
                .AsQueryable();

            // Tích lũy WHERE — chỉ khi có giá trị (optional filter)
            if (projectId.HasValue)
                query = query.Where(t => t.ProjectId == projectId.Value);

            if (assignedToId.HasValue)
                query = query.Where(t => t.AssignedToId == assignedToId.Value);

            if (statusId.HasValue)
                query = query.Where(t => t.StatusId == statusId.Value);

            if (priorityId.HasValue)
                query = query.Where(t => t.PriorityId == priorityId.Value);

            if (categoryId.HasValue)
                query = query.Where(t => t.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(searchKeyword))
                query = query.Where(t =>
                    t.Title.Contains(searchKeyword) ||
                    (t.Description != null && t.Description.Contains(searchKeyword)));

            // Đếm tổng trước khi phân trang (cho tính số trang UI)
            var totalCount = await query.CountAsync();

            // Phân trang
            var items = await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        // ════════════════════════════════════════════════════════
        // TRUY VẤN THEO NGƯỜI PHỤ TRÁCH
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// Task chưa hoàn thành được giao cho developer.
        /// Dùng cho màn hình "Công việc của tôi".
        /// </summary>
        public async Task<List<TaskItem>> GetAssignedToUserAsync(int userId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.Category)
                .Where(t => t.AssignedToId == userId && !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ThenByDescending(t => t.Priority.Level)
                .ToListAsync();
        }

        /// <summary>Task đang chờ review lần 1 (Status = REVIEW-1) bởi reviewer chỉ định.</summary>
        public async Task<List<TaskItem>> GetByReviewer1Async(int reviewer1Id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Where(t => t.Reviewer1Id == reviewer1Id &&
                            t.Status.Name == "REVIEW-1")
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        /// <summary>Task đang chờ review lần 2 (Status = REVIEW-2) bởi reviewer chỉ định.</summary>
        public async Task<List<TaskItem>> GetByReviewer2Async(int reviewer2Id)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Where(t => t.Reviewer2Id == reviewer2Id &&
                            t.Status.Name == "REVIEW-2")
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        /// <summary>Task đang chờ test (Status = IN-TEST) bởi tester chỉ định.</summary>
        public async Task<List<TaskItem>> GetByTesterAsync(int testerId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Where(t => t.TesterId == testerId &&
                            t.Status.Name == "IN-TEST")
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        // ════════════════════════════════════════════════════════
        // TRUY VẤN THEO DỰ ÁN
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// Tất cả task của 1 dự án.
        /// includeSubTasks = false → chỉ lấy task gốc (ParentTaskId == null).
        /// </summary>
        public async Task<List<TaskItem>> GetByProjectAsync(int projectId, bool includeSubTasks = false)
        {
            using var ctx = _contextFactory.CreateDbContext();

            var query = ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.AssignedTo)
                .Include(t => t.Category)
                .Where(t => t.ProjectId == projectId);

            if (!includeSubTasks)
                query = query.Where(t => t.ParentTaskId == null);

            return await query
                .OrderByDescending(t => t.Priority.Level)
                .ThenBy(t => t.DueDate)
                .ToListAsync();
        }

        // ════════════════════════════════════════════════════════
        // CẢNH BÁO / DASHBOARD
        // ════════════════════════════════════════════════════════

        /// <summary>Task đã quá hạn — dùng cho widget cảnh báo đỏ trên Dashboard.</summary>
        public async Task<List<TaskItem>> GetOverdueAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            var now = DateTime.UtcNow;
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Where(t => t.DueDate < now && !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        /// <summary>Task sắp đến hạn trong N ngày — widget nhắc nhở vàng trên Dashboard.</summary>
        public async Task<List<TaskItem>> GetDueSoonAsync(int days = 7)
        {
            using var ctx = _contextFactory.CreateDbContext();
            var now = DateTime.UtcNow;
            var threshold = now.AddDays(days);
            return await ctx.TaskItems
                .AsNoTracking()
                .Include(t => t.Project)
                .Include(t => t.AssignedTo)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Where(t => t.DueDate >= now && t.DueDate <= threshold && !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        // ════════════════════════════════════════════════════════
        // CẬP NHẬT TỐI ƯU (ExecuteUpdateAsync)
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// Cập nhật tiến độ % — tự động chuyển Status sang RESOLVED khi progress = 100.
        ///
        /// SQL sinh ra:
        ///   UPDATE TaskItems
        ///   SET ProgressPercent=?, IsCompleted=?, UpdatedAt=?, CompletedAt=?, StatusId=?
        ///   WHERE Id=?
        ///
        /// resolvedStatusId: ID của Status có Name = "RESOLVED" (truyền vào từ Service
        /// sau khi lookup từ DB, tránh hard-code ID).
        /// </summary>
        public async Task UpdateProgressAsync(int taskId, byte progressPercent, int resolvedStatusId)
        {
            using var ctx = _contextFactory.CreateDbContext();

            var isCompleted = progressPercent == 100;
            var completedAt = isCompleted ? (DateTime?)DateTime.UtcNow : null;

            // Call 1: luôn chạy — update progress, isCompleted, timestamp
            await ctx.TaskItems
                .Where(t => t.Id == taskId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(t => t.ProgressPercent, progressPercent)
                    .SetProperty(t => t.IsCompleted, isCompleted)
                    .SetProperty(t => t.UpdatedAt, DateTime.UtcNow)
                    .SetProperty(t => t.CompletedAt, completedAt));

            // Call 2: chỉ chạy khi hoàn thành — chuyển Status sang RESOLVED
            // Tách riêng vì EF Core không cho phép tham chiếu t.StatusId
            // trong phần value của SetProperty (không dịch được sang SQL SET clause)
            if (isCompleted)
                await ctx.TaskItems
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(t => t.StatusId, resolvedStatusId));
        }

        /// <summary>
        /// Chuyển trạng thái workflow — chỉ update StatusId + UpdatedAt.
        /// Dùng ExecuteUpdateAsync, không cần load entity.
        /// </summary>
        public async Task UpdateStatusAsync(int taskId, int statusId)
        {
            using var ctx = _contextFactory.CreateDbContext();

            await ctx.TaskItems
                .Where(t => t.Id == taskId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(t => t.StatusId, statusId)
                    .SetProperty(t => t.UpdatedAt, DateTime.UtcNow));
        }

        /// <summary>
        /// Gán reviewer/tester và chuyển trạng thái.
        ///
        /// Giới hạn EF Core: SetProperty không cho phép tham chiếu lại entity (t.Field)
        /// trong phần value → không thể viết conditional trong 1 ExecuteUpdateAsync.
        /// Fix: tách thành các call riêng biệt, chỉ gọi khi tham số có giá trị.
        ///   - 1 call bắt buộc: StatusId + UpdatedAt
        ///   - 0–3 call tùy chọn: Reviewer1Id / Reviewer2Id / TesterId
        /// </summary>
        public async Task AssignReviewerAsync(
            int taskId,
            int? reviewer1Id,
            int? reviewer2Id,
            int? testerId,
            int newStatusId)
        {
            using var ctx = _contextFactory.CreateDbContext();

            // Bắt buộc: chuyển trạng thái + cập nhật timestamp
            await ctx.TaskItems
                .Where(t => t.Id == taskId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(t => t.StatusId, newStatusId)
                    .SetProperty(t => t.UpdatedAt, DateTime.UtcNow));

            // Tùy chọn: chỉ set khi được truyền vào — không ghi đè giá trị cũ nếu null
            if (reviewer1Id.HasValue)
                await ctx.TaskItems
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(t => t.Reviewer1Id, reviewer1Id.Value));

            if (reviewer2Id.HasValue)
                await ctx.TaskItems
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(t => t.Reviewer2Id, reviewer2Id.Value));

            if (testerId.HasValue)
                await ctx.TaskItems
                    .Where(t => t.Id == taskId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(t => t.TesterId, testerId.Value));
        }

        // ════════════════════════════════════════════════════════
        // THỐNG KÊ
        // ════════════════════════════════════════════════════════

        /// <summary>
        /// GroupBy Status trực tiếp trên DB — không load data về memory.
        /// Kết quả: { "CREATED": 3, "IN-PROGRESS": 7, "CLOSED": 5, ... }
        /// </summary>
        public async Task<Dictionary<string, int>> GetStatusSummaryByProjectAsync(int projectId)
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.TaskItems
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .GroupBy(t => t.Status.Name)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

        // ════════════════════════════════════════════════════════
        // LOOKUP DATA CHO DROPDOWN
        // ════════════════════════════════════════════════════════

        /// <summary>Lấy tất cả Status (10 bước), sắp theo DisplayOrder — dùng cho dropdown.</summary>
        public async Task<List<Status>> GetAllStatusesAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Statuses
                .AsNoTracking()
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync();
        }

        /// <summary>Lấy tất cả Priority (4 mức), sắp theo Level — dùng cho dropdown.</summary>
        public async Task<List<Priority>> GetAllPrioritiesAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Priorities
                .AsNoTracking()
                .OrderBy(p => p.Level)
                .ToListAsync();
        }

        /// <summary>Lấy tất cả Category — dùng cho dropdown.</summary>
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}
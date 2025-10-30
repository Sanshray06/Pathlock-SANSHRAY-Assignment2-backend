namespace MiniProjectManager.API.Models.DTOs.Scheduler
{
    public class ScheduleResponseDto
    {
        public List<ScheduledTaskDto> ScheduledTasks { get; set; } = new();
        public int TotalTasks { get; set; }
        public int ScheduledTasksCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimatedCompletionDate { get; set; }
    }

    public class ScheduledTaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        public DateTime? OriginalDueDate { get; set; }
        public bool IsOverdue { get; set; }
        public int Priority { get; set; }
    }
}
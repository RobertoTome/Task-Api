using TaskApi.Models;

namespace TaskApi.DTOs
{
    public class TaskEventDto
    {
        public string Event { get; set; } = string.Empty; // "TaskCreated", "TaskUpdated", "TaskDeleted"
        public int TaskId { get; set; }
        public TaskItem? Task { get; set; }
        public string? Status { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
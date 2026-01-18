namespace TestApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TkStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public enum TkStatus
    {
        Pending = 0,
        InProgress = 1,
        Done = 2
    }
}
using TestApi.Models;
using TaskStatus = TestApi.Models.TkStatus;

namespace TestApi.Requests
{
    public class UpdateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
    }
}
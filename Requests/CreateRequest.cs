using TaskApi.Models;

namespace TaskApi.Requests
{
    public class CreateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public TkStatus Status { get; set; }
    }
}

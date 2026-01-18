using Microsoft.AspNetCore.Mvc;
using TaskApi.Filters;
using TaskApi.Requests;
using TaskApi.Services;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("/tasks/")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("Health", Name = "Health")]
        public IActionResult Health()
        {
            return Ok(new { message = "Service is online" });
        }

        [ApiKeyValidation]
        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create(CreateRequest request)
        {
            try
            {
                Models.TaskItem result = await _taskService.Create(request);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error processing request" });
            }
        }

        [ApiKeyValidation]
        [HttpGet("List", Name = "List")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<Models.TaskItem> result = await _taskService.GetList();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error processing request" });
            }
        }

        [ApiKeyValidation]
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Models.TaskItem? result = await _taskService.GetById(id);
                if (result == null) return NotFound("Not Found");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error processing request" });
            }
        }

        [ApiKeyValidation]
        [HttpPut("{id}", Name = "Update")]
        public async Task<IActionResult> Update(int id, UpdateRequest request)
        {
            try
            {
                Models.TaskItem? result = await _taskService.Update(id, request);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error processing request" });
            }
        }
        [ApiKeyValidation]
        [HttpDelete("{id}", Name = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _taskService.Delete(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error processing request" });
            }
        }
    }
}
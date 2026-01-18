using Microsoft.AspNetCore.Mvc;
using TestApi.Filters;
using TestApi.Requests;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("/tasks/")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("Health", Name = "Health")]
        public IActionResult Health()
        {
            return Ok(new { message = "Servicio online" });
        }

        [ApiKeyValidation]
        [HttpPost(Name = "Create")]
        public async Task<IActionResult> Create(CreateRequest request)
        {
            try
            {
                Models.TaskItem result = await _testService.Create(request);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error al procesar la solicitud" });
            }
        }

        [ApiKeyValidation]
        [HttpGet("List", Name = "List")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<Models.TaskItem> result = await _testService.GetList();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error al procesar la solicitud" });
            }
        }

        [ApiKeyValidation]
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Models.TaskItem? result = await _testService.GetById(id);
                if (result == null) return NotFound("Not Found");
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error al procesar la solicitud" });
            }
        }

        [ApiKeyValidation]
        [HttpPut("{id}", Name = "Update")]
        public async Task<IActionResult> Update(int id, UpdateRequest request)
        {
            try
            {
                Models.TaskItem? result = await _testService.Update(id, request);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error al procesar la solicitud" });
            }
        }
        [ApiKeyValidation]
        [HttpDelete("{id}", Name = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _testService.Delete(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch
            {
                return StatusCode(500, new { ok = false, message = "Error al procesar la solicitud" });
            }
        }
    }
}
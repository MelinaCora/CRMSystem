using Aplication.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CRMSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITaskStatusService _service;

        public TaskStatusController(ITaskStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return new JsonResult(result);
        }
    }
}

using Aplication.Interfaces;
using Aplication.Request;
using Aplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRMSystem.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectRequest request)
        {
            var result = await _service.CreateProject(request);
            return new JsonResult(result) { StatusCode = 201 };
        }
    }
}

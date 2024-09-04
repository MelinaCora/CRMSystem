using Aplication.Interfaces;
using Aplication.Request;
using Aplication.Responses;
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

        [HttpGet]
        public async Task<IActionResult> GetProjects(
            string projectName,
            int? campaignTypeId,
            int? clientId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result = await _service.GetProjectsAsync(projectName, campaignTypeId, clientId, pageNumber, pageSize);

            if (result.Items.Count == 0)
            {
                return NotFound("No projects found.");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _service.GetProjectByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

    }
}

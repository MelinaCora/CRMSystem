﻿using Aplication.Interfaces;
using Aplication.Request;
using Aplication.Responses;
using Aplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        //get all projects with filters and pagination
        [HttpGet]
        public async Task<IActionResult> GetProjects(
            string? projectName,
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
        //get project by ID
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
        //Add interaction
        [HttpPost("{id}/interactions")]
        public async Task<IActionResult> AddInteraction(Guid id, [FromBody] CreateInteractionRequest request)
        {
            if (id != request.ProjectId)
            {
                return BadRequest("Project ID mismatch.");
            }

            var result = await _service.AddInteractionAsync(request);

            if (result)
            {
                return Ok("Interaction added successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while adding the interaction.");
            }
        }
        //AddTask
        [HttpPost("{id}/tasks")]
        public async Task<IActionResult> AddTaskToProject(Guid id, [FromBody] TaskRequest request)
        {
                if (!ModelState.IsValid) //validacion
                {
                    return BadRequest(ModelState);
                }

                var result = await _service.AddTaskToProject(id, request);

                if (result)
                {
                    return Ok("Task added successfully.");
                }
                else
                {
                    return StatusCode(500, "An error occurred while adding the task.");
                }
        }

        [HttpPut("tasks/{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                var updatedTask = await _service.UpdateTaskAsync(id, request);

                
                return Ok(updatedTask);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Task not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

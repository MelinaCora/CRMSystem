﻿using Aplication.Exceptions;
using Aplication.Interfaces;
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
            try
            {
                var result = await _service.CreateProject(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (ProjectNameAlredyExistException ex)
            {
                // Retornar un error 400 con el mensaje de que el nombre del proyecto ya existe
                return BadRequest(new { message = ex.Message });
            }
            catch (ObjectNotFoundException ex)
            {
                // Retornar un error 400 con el mensaje de que no se encontró el objeto (tipo de campaña o cliente)
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Para cualquier otro error no manejado, retornar un error 500
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", details = ex.Message });
            }
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
            try
            {
                var project = await _service.GetProjectByIdAsync(id);                
                return Ok(project);
            }
            catch (ObjectNotFoundException ex)
            {
                
                return BadRequest(new { message = ex.Message });
            }
        }
        //Add interaction
        [HttpPost("{id}/interactions")]
        public async Task<IActionResult> AddInteraction(Guid id, [FromBody] CreateInteractionRequest request)
        {
            if (id != request.ProjectId)
            {
                return BadRequest("Project ID mismatch.");
            }

            try
            {
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
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //AddTask
        [HttpPost("{id}/tasks")]
        public async Task<IActionResult> AddTaskToProject(Guid id, [FromBody] TaskRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
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
            catch (ObjectNotFoundException ex)
            {
               return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("tasks/{Taskid}")]
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
            catch (ObjectNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        } 
    }
}

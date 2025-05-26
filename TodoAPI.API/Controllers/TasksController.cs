using Microsoft.AspNetCore.Mvc;
using TodoAPI.API.Models;
using TodoAPI.API.Service.Interfaces;

namespace TodoAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IService<TaskItem> _service;
        public TasksController(IService<TaskItem> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task != null) return Ok(task);
            return NotFound("La tarea Seleccionada no existe");
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(TaskItem task)
        {
            if(string.IsNullOrEmpty(task.Description) || 
               string.IsNullOrWhiteSpace(task.Description))
                return BadRequest("La descripción de la tarea no puede estar vacia");

            var taskAdded = await _service.CreateAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = taskAdded.Id }, taskAdded);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, TaskItem task)
        {
            if(id <= 0) return BadRequest("Id de la tarea invalido");
            if (string.IsNullOrEmpty(task.Description) || string.IsNullOrWhiteSpace(task.Description))
                return BadRequest("La descripción de la tarea no puede estar vacia");
            var existingTask = await _service.GetByIdAsync(id);
            if(existingTask.Id != id)
                return BadRequest("El Id de la tarea no coincide con el Id proporcionado en la URL");
            if (existingTask == null) return NotFound("La tarea Seleccionada no existe");
            await _service.UpdateAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0) return BadRequest("Id de la tarea invalido");
            var existingTask = await _service.GetByIdAsync(id);
            if (existingTask == null) return NotFound("La tarea Seleccionada no existe");
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

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
            var selectedTask = await _service.GetByIdAsync(id);
            if (selectedTask == null) return NotFound("La tarea seleccionada no existe");
            return Ok(selectedTask);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(TaskItem task)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var newTask = await _service.CreateAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = newTask.Id }, newTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, TaskItem task)
        {
            if (id <= 0) return BadRequest("Id de la tarea invalido");
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var foundedTask = await _service.GetByIdAsync(id);
            foundedTask.Description = task.Description;
            await _service.UpdateAsync(foundedTask);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            //if (id <= 0) return BadRequest("Id de la tarea invalido");
            var existingTask = await _service.GetByIdAsync(id);
            if (existingTask == null) return NotFound("La tarea Seleccionada no existe");
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

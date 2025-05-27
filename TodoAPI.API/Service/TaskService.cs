using TodoAPI.API.Context;
using TodoAPI.API.Models;
using TodoAPI.API.Repository.Interfaces;
using TodoAPI.API.Service.Interfaces;

namespace TodoAPI.API.Service
{
    public class TaskService : IService<TaskItem>
    {
        private readonly IRepository<TaskItem> _context;
        public TaskService(IRepository<TaskItem> context) => _context = context;
        public async Task<IEnumerable<TaskItem>> GetAllAsync() => await _context.GetAllAsync();
        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.GetByIdAsync(id);
        }
        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _context.AddAsync(task);
            return task;
        }
        public async Task<TaskItem> UpdateAsync(TaskItem task)
        {
            var foundedTask = await _context.GetByIdAsync(task.Id);
            
            foundedTask.Description = task.Description;
            _context.Update(task);
            await _context.Save();
            return task;
        }
        public async Task<TaskItem> DeleteAsync(int id)
        {
            var foundTask = await _context.GetByIdAsync(id);
            _context.Delete(foundTask);
            await _context.Save();
            return foundTask;
        }
    }
}

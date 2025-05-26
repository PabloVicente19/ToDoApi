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
            var taskItem = await _context.GetByIdAsync(id);
            return taskItem;
        }
        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _context.AddAsync(task);
            return task;
        }
        public Task<TaskItem> UpdateAsync(TaskItem task)
        {
            _context.UpdateAsync(task);
            return Task.FromResult(task);
        }
        public Task<TaskItem> DeleteAsync(int id)
        {
            _context.DeleteAsync(id);
            return Task.FromResult(new TaskItem { Id = id });
        }
    }
}

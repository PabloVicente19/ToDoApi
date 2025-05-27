using Microsoft.EntityFrameworkCore;
using TodoAPI.API.Context;
using TodoAPI.API.Models;
using TodoAPI.API.Repository.Interfaces;

namespace TodoAPI.API.Repository
{
    public class TaskRepository : IRepository<TaskItem>
    {
        private readonly TodoContext _context;
        public TaskRepository(TodoContext context) => _context = context;
        public async Task<IEnumerable<TaskItem>> GetAllAsync() => await _context.TaskItems.ToListAsync();
        public async Task<TaskItem> GetByIdAsync(int id)  
        {
            var task = await _context.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }
        public async Task AddAsync(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
        }
        public void Update(TaskItem task)
        {
            _context.TaskItems.Attach(task);
            _context.TaskItems.Entry(task).State = EntityState.Modified;
        }
        public void Delete(TaskItem taskItem)
        {
            _context.TaskItems.Remove(taskItem);
        }
        public async Task Save() => await _context.SaveChangesAsync();
    }
}

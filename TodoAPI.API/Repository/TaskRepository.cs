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
            var task = await _context.TaskItems.FindAsync(id);
            return task ?? null;
        }
        public async Task AddAsync(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
        }
        public void UpdateAsync(TaskItem task)
        {
            _context.TaskItems.Attach(task);
            _context.TaskItems.Entry(task).State = EntityState.Modified;
        }
        public void DeleteAsync(int id)
        {
            _context.TaskItems.Remove(_context.TaskItems.Find(id));
        }
        public async Task Save() => await _context.SaveChangesAsync();
    }
}

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TestApi.DTOs;
using TestApi.Hubs;
using TestApi.Models;
using TestApi.Requests;

namespace TestApi.Services
{
    public class TestService : ITestService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<TaskHub> _hubContext;

        public TestService(AppDbContext context, IHubContext<TaskHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<TaskItem> Create(CreateRequest request)
        {
            TaskItem task = new TaskItem
            {
                Message = request.Message,
                CreatedAt = DateTime.Now,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("TaskCreated", new TaskEventDto
            {
                Event = "TaskCreated",
                TaskId = task.Id,
                Task = task
            });

            return task;
        }

        public async Task<List<TaskItem>> GetList()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskItem?> Update(int id, UpdateRequest request)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.Message = request.Message;
                task.Title = request.Title;
                task.Description = request.Description;
                task.Status = request.Status;
                await _context.SaveChangesAsync();

                await _hubContext.Clients.All.SendAsync("TaskUpdated", new TaskEventDto
                {
                    Event = "TaskUpdated",
                    TaskId = task.Id,
                    Task = task
                    //Status = 0.ToString()
                });
            }
            return task;
        }

        public async Task<bool> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                await _hubContext.Clients.All.SendAsync("TaskDeleted", new TaskEventDto
                {
                    Event = "TaskDeleted",
                    TaskId = id
                });

                return true;
            }
            return false;
        }
    }
}
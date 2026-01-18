using TaskApi.Models;
using TaskApi.Requests;

namespace TaskApi.Services
{
    public interface ITaskService
    {
        Task<TaskItem> Create(CreateRequest r);
        Task<List<TaskItem>> GetList();
        Task<TaskItem?> GetById(int id);
        Task<TaskItem?> Update(int id, UpdateRequest r);
        Task<bool> Delete(int id);
    }
}
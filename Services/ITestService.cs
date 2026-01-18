using TestApi.Models;
using TestApi.Requests;

namespace TestApi.Services
{
    public interface ITestService
    {
        Task<TaskItem> Create(CreateRequest r);
        Task<List<TaskItem>> GetList();
        Task<TaskItem?> GetById(int id);
        Task<TaskItem?> Update(int id, UpdateRequest r);
        Task<bool> Delete(int id);
    }
}
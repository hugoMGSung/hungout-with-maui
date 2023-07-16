using TodoApp.Models;

namespace TodoApp.DataServices
{
    public interface IRestDataService
    {
        Task<List<Todo>> GetAllToDosAsync();
        Task AddToDoAsync(Todo data);
        Task DeleteToDoAsync(int id);
        Task UpdateToDoAsync(Todo data);
    }
}

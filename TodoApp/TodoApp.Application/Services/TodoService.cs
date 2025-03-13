using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Services;

public class TodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TodoItem>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<TodoItem?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task AddAsync(string title)
    {
        var item = new TodoItem { Title = title };
        await _repository.AddAsync(item);
    }

    public async Task UpdateAsync(int id, string title, bool isCompleted)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null) return;

        item.Title = title;
        item.IsCompleted = isCompleted;
        await _repository.UpdateAsync(item);
    }

    public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
}
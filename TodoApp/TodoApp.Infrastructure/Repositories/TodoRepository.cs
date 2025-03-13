using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _items = new();

    public Task<List<TodoItem>> GetAllAsync() => Task.FromResult(_items);

    public Task<TodoItem?> GetByIdAsync(int id) => Task.FromResult(_items.FirstOrDefault(x => x.Id == id));

    public Task AddAsync(TodoItem item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TodoItem item)
    {
        var index = _items.FindIndex(x => x.Id == item.Id);
        if (index != -1) _items[index] = item;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        _items.RemoveAll(x => x.Id == id);
        return Task.CompletedTask;
    }
}
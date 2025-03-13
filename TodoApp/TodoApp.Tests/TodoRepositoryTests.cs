using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Repositories;
using Xunit;
using FluentAssertions;

namespace TodoApp.Tests;

public class TodoRepositoryTests
{
    private readonly TodoRepository _repository;

    public TodoRepositoryTests()
    {
        _repository = new TodoRepository();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoItems()
    {
        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task AddAsync_ShouldStoreTodoItem()
    {
        // Arrange
        var todo = new TodoItem { Title = "Test Task", IsCompleted = false };

        // Act
        await _repository.AddAsync(todo);
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().HaveCount(1);
        result[0].Title.Should().Be("Test Task");
        result[0].IsCompleted.Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectItem()
    {
        // Arrange
        var todo = new TodoItem { Title = "Find Me" };
        await _repository.AddAsync(todo);

        // Act
        var result = await _repository.GetByIdAsync(todo.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be("Find Me");
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyExistingItem()
    {
        // Arrange
        var todo = new TodoItem { Title = "Initial Task" };
        await _repository.AddAsync(todo);

        todo.Title = "Updated Task";
        todo.IsCompleted = true;

        // Act
        await _repository.UpdateAsync(todo);
        var result = await _repository.GetByIdAsync(todo.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Title.Should().Be("Updated Task");
        result.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveItem()
    {
        // Arrange
        var todo = new TodoItem { Title = "Delete Me" };
        await _repository.AddAsync(todo);

        // Act
        await _repository.DeleteAsync(todo.Id);
        var result = await _repository.GetByIdAsync(todo.Id);

        // Assert
        result.Should().BeNull();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;
using Xunit;
using FluentAssertions;

namespace TodoApp.Tests;

public class TodoServiceTests
{
    private readonly Mock<ITodoRepository> _mockRepo;
    private readonly TodoService _service;

    public TodoServiceTests()
    {
        _mockRepo = new Mock<ITodoRepository>();
        _service = new TodoService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTodos()
    {
        // Arrange
        var todos = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Task 1", IsCompleted = false },
            new TodoItem { Id = 2, Title = "Task 2", IsCompleted = true }
        };
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(todos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainSingle(x => x.Title == "Task 1" && !x.IsCompleted);
        result.Should().ContainSingle(x => x.Title == "Task 2" && x.IsCompleted);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectTodo()
    {
        // Arrange
        var todo = new TodoItem { Id = 1, Title = "Task 1", IsCompleted = false };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(todo);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(todo);
    }

    [Fact]
    public async Task AddAsync_ShouldCallRepositoryOnce()
    {
        // Arrange
        var title = "New Task";

        // Act
        await _service.AddAsync(title);

        // Assert
        _mockRepo.Verify(repo => repo.AddAsync(It.Is<TodoItem>(t => t.Title == title)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyTodo()
    {
        // Arrange
        var existingTodo = new TodoItem { Id = 1, Title = "Old Task", IsCompleted = false };
        _mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingTodo);

        // Act
        await _service.UpdateAsync(1, "Updated Task", true);

        // Assert
        existingTodo.Title.Should().Be("Updated Task");
        existingTodo.IsCompleted.Should().BeTrue();
        _mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<TodoItem>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveItem()
    {
        // Act
        await _service.DeleteAsync(1);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}
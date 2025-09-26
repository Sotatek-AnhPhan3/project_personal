using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Application.Usecases.Users.Commands.Delete;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;
using Moq;
using Xunit;

namespace InternSotatek.Personal.Application.Tests.Usecases.Users.Commands.Delete;

public class DeleteUserCommandHandlerTests
{
    [Fact]
    public async Task DeleteUserCommandHandler_ShouldDeleteUser_SuccessResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new DeleteUserCommand { Id = userId };

        var mockRepo = new Mock<IRepository<User, Guid>>();
        mockRepo
            .Setup(r => r.DeleteByIdAsync(userId))
            .Returns(Task.CompletedTask);

        var handler = new DeleteUserCommandHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepo.Verify(r => r.DeleteByIdAsync(userId), Times.Once);
        Assert.Equal(200, result.Code);
        Assert.Equal("OK", result.Message);
    }

    [Fact]
    public async Task DeleteUserCommandHandler_WhenRepositoryThrows_Exception()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new DeleteUserCommand { Id = userId };

        var mockRepo = new Mock<IRepository<User, Guid>>();
        mockRepo
            .Setup(r => r.DeleteByIdAsync(userId))
            .ThrowsAsync(new InvalidOperationException("User not found"));
        var handler = new DeleteUserCommandHandler(mockRepo.Object);

        // Act & Assert: vì handler không catch, exception sẽ được throw ra
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.Handle(command, CancellationToken.None)
        );

        mockRepo.Verify(r => r.DeleteByIdAsync(userId), Times.Once);
    }
}

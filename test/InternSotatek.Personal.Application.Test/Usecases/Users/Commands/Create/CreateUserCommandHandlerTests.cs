using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Application.Usecases.Users.Commands.Create;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure.Repositories;
using Moq;
using Xunit;

namespace InternSotatek.Personal.Application.Tests.Usecases.Users.Commands.Create;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task CreateUserCommandHandler_ShouldCreateUser_Success()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            Username = "testuser",
            Password = "P@ssw0rd!",
            Firstname = "Test",
            Lastname = "User",
            Email = "test@example.com",
            PhoneNumber = "123456789",
            Dob = new DateOnly(1990, 1, 1)
        };

        var mockValidator = new Mock<IValidator<CreateUserCommand>>();

        var mockRepo = new Mock<IRepository<User, Guid>>();
        User capturedUser = null;
        mockRepo
            .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Callback<User, CancellationToken>((u, _) => capturedUser = u)
            .ReturnsAsync((User u, CancellationToken _) => u);

        var handler = new CreateUserCommandHandler(mockValidator.Object, mockRepo.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(200, result.Code);
        mockRepo.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotNull(capturedUser);
        Assert.Equal(command.Username, capturedUser.Username);
        Assert.NotEqual(command.Password, capturedUser.PasswordHashed);
        Assert.Contains("-", capturedUser.PasswordHashed);
    }
}

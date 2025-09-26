using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Application.Usecases.Roles.Queries.GetRolesList;
using InternSotatek.Personal.Application.Usecases.Users.Queries.GetUsersList;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using InternSotatek.Personal.Infrastructure.Repositories;
using Moq;
using Xunit;

namespace InternSotatek.Personal.Application.Tests.Usecases.Users.Queries.GetUsersList;

public class GetUsersListHandlerTests
{
    [Fact]
    public async Task GetUserListHandler_FindBySearch()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), Username = "duyanh", Email = "a@test.com", IsActive = true },
        }.AsQueryable();

        var mockRepo = new Mock<IRepository<User, Guid>>();
        mockRepo
            .Setup(r => r.GetAll())
            .Returns(users);
        var handler = new GetUsersListQueryHandler(mockRepo.Object);

        var query = new GetUsersListQuery
        {
            PageIndex = 1,
            PageSize = 10,
            Search = "duy",
            IsActive = true
        };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal("duyanh", result.Items[0].Username);
    }
}

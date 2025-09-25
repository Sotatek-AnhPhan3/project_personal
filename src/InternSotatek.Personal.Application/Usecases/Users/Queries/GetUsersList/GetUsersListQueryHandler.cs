using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Usecases.Users.Queries.GetUsersList;

public class GetUsersListQueryHandler : IRequestHandler<GetUsersListQuery, GetUsersListResponse>
{
    private readonly IRepository<User, Guid> _userRepository;
    public GetUsersListQueryHandler(IRepository<User, Guid> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUsersListResponse> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
    {
        var allUsers = await AllUsers(request);

        int totalCount = allUsers.Count();
        var items = await allUsers
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(u => new UserListDto
            {
                Id = u.Id,
                Username = u.Username,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                IsActive = u.IsActive,
                Dob = u.Dob,
                CreatedTime = u.CreatedTime,
                UpdatedTime = u.UpdatedTime
            }).ToListAsync();
        return new GetUsersListResponse
        {
            TotalCount = totalCount,
            Items = items,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            SortBy = request.SortBy,
            Sorting = request.Sorting,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
        };
    }
    private async Task<IQueryable<User>> AllUsers(GetUsersListQuery request)
    {
        var allUsers = _userRepository.GetAll();

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            if (request.Sorting.ToLower() == "asc")
            {
                allUsers = allUsers.OrderBy(u => EF.Property<object>(u, request.SortBy));
            }
            else
            {
                allUsers = allUsers.OrderByDescending(u => EF.Property<object>(u, request.SortBy));
            }
        }

        if (!string.IsNullOrEmpty(request.Search))
        {
            allUsers = allUsers.Where(u =>
                u.Username.ToLower().Contains(request.Search.ToLower()) ||
                u.Email.ToLower().Contains(request.Search.ToLower()) ||
                u.PhoneNumber.ToLower().Contains(request.Search.ToLower()) ||
                u.Firstname != null && u.Firstname.ToLower().Contains(request.Search.ToLower()) ||
                u.Lastname != null && u.Lastname.ToLower().Contains(request.Search.ToLower())
            );
        }

        if (request.IsActive)
        {
            allUsers = allUsers.Where(u => u.IsActive);
        }
        else
        {
            allUsers = allUsers.Where(u => !u.IsActive);
        }

        return allUsers;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Users.UseCases.Queries.GetUsersList
{
	public class GetUsersListQueryHandle : IRequestHandler<GetUsersListQuery, GetUsersListResponse>
	{
		private readonly PersonalDbContext _dbContext;
		public GetUsersListQueryHandle(PersonalDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<GetUsersListResponse> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
		{
			var allUsers = _dbContext.Users.AsQueryable();

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
			return new GetUsersListResponse { 
				TotalCount = totalCount, 
				Items = items, 
				PageIndex = request.PageIndex, 
				PageSize = request.PageSize,
				SortBy = request.SortBy,
				Sorting = request.Sorting,
				TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize),
			};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSotatek.Personal.Application.Users.UseCases.Queries.GetUsersList
{
	public class GetUsersListQuery : PagedAndSortedResultQueryDto, IRequest<GetUsersListResponse>
	{
		public string? Search { get; set; }

	}

	public class PagedAndSortedResultQueryDto
	{
		public int PageIndex { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public string Sorting { get; set; } = "ASC";
		public string SortBy { get; set; } = "Username";
	}
}

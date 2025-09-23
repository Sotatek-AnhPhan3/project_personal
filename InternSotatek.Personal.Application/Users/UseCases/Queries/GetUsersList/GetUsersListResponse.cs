using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Application.Users.UseCases.Queries.GetUsersList
{
	public class GetUsersListResponse : PagedResultDto<UserListDto>
	{
		
	}
	public class PagedResultDto<T> : PagedAndSortedResultQueryDto
	{
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
		public List<T> Items { get; set; }
	}
	public class UserListDto
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public bool IsActive { get; set; } = true;
		public DateTime? Dob { get; set; }
		public DateTime CreatedTime { get; set; }
		public DateTime? UpdatedTime { get; set; }
	}
}

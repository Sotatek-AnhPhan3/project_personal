using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Application.Common.Dtos;
using InternSotatek.Personal.Domain.Entities;

namespace InternSotatek.Personal.Application.Usecases.Roles.Queries.GetRolesList;

public class GetRolesListResponse : PagedResultDto<RolesListDto>
{

}
public class PagedResultDto<T> : PagedAndSortedResultQueryDto
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; }
}

public class RolesListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
}

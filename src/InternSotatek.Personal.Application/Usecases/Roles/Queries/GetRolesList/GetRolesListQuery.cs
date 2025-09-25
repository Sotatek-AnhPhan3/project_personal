using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Application.Common.Dtos;
using MediatR;

namespace InternSotatek.Personal.Application.Usecases.Roles.Queries.GetRolesList;

public class GetRolesListQuery : PagedAndSortedResultQueryDto, IRequest<GetRolesListResponse>
{
    public string? Search { get; set; }
    public bool IsActive { get; set; } = true;

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSotatek.Personal.Application.Usecases.UserRoles.Commands.Create;

public class CreateUserRoleCommand : IRequest<CreateUserRoleResponse>
{
    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; }
}

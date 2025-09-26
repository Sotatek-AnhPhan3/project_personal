using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;

namespace InternSotatek.Personal.Application.Usecases.UserRoles.Commands.Create;

public class CreateUserRoleResponse
{
    public Guid UserId { get; set; }
    public List<Guid> RoleIds { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;

namespace InternSotatek.Personal.Application.Usecases.UserRoles.Queries.GetAllRoleByUserId;

public class GetAllRoleByUserIdResponse
{
    public Guid UserId { get; set; }

    public List<Role> Roles { get; set; }
}

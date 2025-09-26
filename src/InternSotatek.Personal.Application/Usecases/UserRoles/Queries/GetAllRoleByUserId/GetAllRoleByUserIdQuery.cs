using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSotatek.Personal.Application.Usecases.UserRoles.Queries.GetAllRoleByUserId;

public class GetAllRoleByUserIdQuery : IRequest<GetAllRoleByUserIdResponse>
{
    public Guid UserId { get; set; }
}

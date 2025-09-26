using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest<DeleteRoleResponse>
{
    public Guid Id { get; set; }
}

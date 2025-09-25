using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<UpdateRoleResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

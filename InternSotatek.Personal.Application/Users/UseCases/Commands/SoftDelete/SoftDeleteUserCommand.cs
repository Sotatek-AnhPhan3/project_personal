using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.SoftDelete;
public class SoftDeleteUserCommand : IRequest<SoftDeleteUserResponse>
{
    public Guid Id { get; set; }
}

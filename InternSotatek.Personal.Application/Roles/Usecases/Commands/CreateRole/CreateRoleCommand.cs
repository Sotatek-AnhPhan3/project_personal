using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InternSotatek.Personal.Application.Roles.Usecases.Commands.CreateRole
{
	public class CreateRoleCommand : IRequest<CreateRoleResponse>
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}

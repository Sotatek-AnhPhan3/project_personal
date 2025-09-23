using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.Delete
{
	public class DeleteUserCommand : IRequest<DeleteUserResponse>
	{
		public Guid Id { get; set; }
	}
}

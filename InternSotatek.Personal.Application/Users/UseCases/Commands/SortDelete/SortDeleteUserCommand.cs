using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.SortDelete
{
	public class SortDeleteUserCommand : IRequest<SortDeleteUserResponse>
	{
		public Guid Id { get; set; }
	}
}

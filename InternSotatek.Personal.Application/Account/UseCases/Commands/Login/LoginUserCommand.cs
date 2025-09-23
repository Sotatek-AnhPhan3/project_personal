using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace InternSotatek.Personal.Application.Account.UseCases.Commands.Login
{
	public class LoginUserCommand : IRequest<LoginUserResponse>
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}

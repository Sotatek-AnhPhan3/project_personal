using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.Delete
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
	{
		private readonly IRepository<User, Guid> _userRepository;

		public DeleteUserCommandHandler(IRepository<User, Guid> userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var existingUser = await _userRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
			if (existingUser == null)
			{
				throw new KeyNotFoundException("User not found");
			}

			await _userRepository.DeleteAsync(existingUser);

			return new DeleteUserResponse
			{
				Code = 200,
				Message = "OK",
			};
		}
	}
}

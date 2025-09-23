using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.Delete
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
	{
		private readonly PersonalDbContext _dbContext;

		public DeleteUserCommandHandler(PersonalDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
			if (existingUser == null)
			{
				throw new KeyNotFoundException("User not found");
			}

			_dbContext.Users.Remove(existingUser);

			await _dbContext.SaveChangesAsync(cancellationToken);

			return new DeleteUserResponse
			{
				Code = 200,
				Message = "OK",
			};
		}
	}
}

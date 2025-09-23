using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InternSotatek.Personal.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.SortDelete
{
	public class SortDeleteUserCommandHandler : IRequestHandler<SortDeleteUserCommand, SortDeleteUserResponse>
	{

		private readonly PersonalDbContext _dbContext;

		public SortDeleteUserCommandHandler(PersonalDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<SortDeleteUserResponse> Handle(SortDeleteUserCommand request, CancellationToken cancellationToken)
		{
			var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
			if (existingUser == null)
			{
				throw new KeyNotFoundException("User not found");
			}

			existingUser.IsActive = false;
			await _dbContext.SaveChangesAsync(cancellationToken);

			return new SortDeleteUserResponse
			{
				Code = 200,
				Message = "Ok"
			};
		} 

	}
}

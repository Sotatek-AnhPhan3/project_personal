using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.Update
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
	{
		private readonly PersonalDbContext _dbContext;
		private readonly IValidator<UpdateUserCommand> _validator;

		public UpdateUserCommandHandler(
			PersonalDbContext dbContext
			, IValidator<UpdateUserCommand> validator
		)
		{
			_dbContext = dbContext;
			_validator = validator;
		}

		public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var checkValid = _validator.Validate(request);
			if (!checkValid.IsValid)
			{
				throw new FluentValidation.ValidationException(checkValid.Errors);
			}

			var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
			if (existingUser == null)
			{
				throw new KeyNotFoundException("User not found");
			}
			existingUser.Username = request.Username;
			existingUser.Firstname = request.Firstname;
			existingUser.Lastname = request.Lastname;
			existingUser.Email = request.Email;
			existingUser.PhoneNumber = request.PhoneNumber;
			existingUser.Dob = request.Dob;
			existingUser.UpdatedTime = DateTime.UtcNow;

			await _dbContext.SaveChangesAsync(cancellationToken);

			return new UpdateUserResponse
			{
				Code = 200,
				Message = "Update successfull"
			};
		}


	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.Update
{
	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
	{
		private readonly IRepository<User, Guid> _userRepository;
		private readonly IValidator<UpdateUserCommand> _validator;

		public UpdateUserCommandHandler(
            IRepository<User, Guid> userRepository
            , IValidator<UpdateUserCommand> validator
		)
		{
            _userRepository = userRepository;
			_validator = validator;
		}

		public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var checkValid = _validator.Validate(request);
			if (!checkValid.IsValid)
			{
				throw new FluentValidation.ValidationException(checkValid.Errors);
			}

			var existingUser = await _userRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
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

			await _userRepository.UpdateAsync(existingUser);

			return new UpdateUserResponse
			{
				Code = 200,
				Message = "Update successfull"
			};
		}


	}
}

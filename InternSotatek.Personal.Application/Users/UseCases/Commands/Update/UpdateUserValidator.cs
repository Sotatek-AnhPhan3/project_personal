using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.Update
{
	public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserValidator()
		{
			// Username
			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("Username unavailable")
				.Length(2, 250).WithMessage("Username is invalid");
			// Email
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email unavailable")
				.EmailAddress().WithMessage("Email is invalid");
			// phone  number
			RuleFor(x => x.PhoneNumber)
				.Matches(@"^\d+$").WithMessage("PhoneNumber must contain only digits")
				.Length(10, 11).WithMessage("PhoneNumber is invalid")
				.NotEmpty().WithMessage("PhoneNumber unavailable");
			// first name
			RuleFor(x => x.Firstname)
				.Length(2, 250).WithMessage("Firstname must be between 2 and 250 characters");
			// Last name
			RuleFor(x => x.Lastname)
				.Length(2, 250).WithMessage("Lastname must be between 2 and 250 characters");
		}
	}
}

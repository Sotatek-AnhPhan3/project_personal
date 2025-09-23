using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Users.UseCases.Queries.GetUserById
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
	{
		private readonly PersonalDbContext _dbContext;
		private readonly IValidator<GetUserByIdQuery> _validator;
		
		public GetUserByIdQueryHandler(
			PersonalDbContext dbContext
			, IValidator<GetUserByIdQuery> validator
		)
		{
			_dbContext = dbContext;
			_validator = validator;
		}

		public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
		{
			var checkValid = _validator.Validate(query);
			if (!checkValid.IsValid)
			{
				throw new FluentValidation.ValidationException(checkValid.Errors);
			}

			var userById = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

			if (userById == null) {
				return new GetUserByIdResponse();
			}
			return new GetUserByIdResponse
			{
				Id = query.Id,
				Username = userById.Username,
				Firstname = userById.Firstname,
				Lastname = userById.Lastname,
				Email = userById.Email,
				PhoneNumber = userById.PhoneNumber,
				IsActive = userById.IsActive,
				Dob = userById.Dob,
				CreatedTime = userById.CreatedTime,
				UpdatedTime = userById.UpdatedTime,

			};
		}
	}
}

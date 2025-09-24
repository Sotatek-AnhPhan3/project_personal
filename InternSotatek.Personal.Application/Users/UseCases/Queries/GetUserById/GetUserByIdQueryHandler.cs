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

namespace InternSotatek.Personal.Application.Users.UseCases.Queries.GetUserById
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
	{
		private readonly IRepository<User, Guid> _userRepository;
		private readonly IValidator<GetUserByIdQuery> _validator;
		
		public GetUserByIdQueryHandler(
             IRepository<User, Guid> userRepository
            , IValidator<GetUserByIdQuery> validator
		)
		{
            _userRepository = userRepository;
			_validator = validator;
		}

		public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
		{
			var checkValid = _validator.Validate(query);
			if (!checkValid.IsValid)
			{
				throw new FluentValidation.ValidationException(checkValid.Errors);
			}

			var userById = await _userRepository.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

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

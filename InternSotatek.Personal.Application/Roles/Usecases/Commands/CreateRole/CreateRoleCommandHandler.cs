using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Application.Utils;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Roles.Usecases.Commands.CreateRole
{
	public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleResponse>
	{
		private readonly PersonalDbContext _dbContext;
		private readonly IValidator<CreateRoleCommand> _validator;
		public CreateRoleCommandHandler(
			PersonalDbContext dbContext
			, IValidator<CreateRoleCommand> validator
		)
		{
			_dbContext = dbContext;
			_validator = validator;
		}

		public async Task<CreateRoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
		{
			var existingRoleByName = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower());
			if (existingRoleByName != null) {
				return new CreateRoleResponse
				{
					Code = 409,
					Message = "Role already exists"
				};
			}

			var checkValid = _validator.Validate(request);
			if (!checkValid.IsValid)
			{
				throw new FluentValidation.ValidationException(checkValid.Errors);
			}

			var newRole = new Role
			{
				Id = Guid.NewGuid(),
				Name = request.Name,
				Description = request.Description,
				CreatedTime = DateTime.UtcNow,
				Slug = SlugHelper.StringToSlug(request.Name)
			};
			await _dbContext.Roles.AddAsync(newRole, cancellationToken);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return new CreateRoleResponse
			{
				Code = 200,
				Message = "Success"
			};
		}
	}
}

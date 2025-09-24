using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Application.Utils;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Usecases.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CreateRoleResponse>
    {
        private readonly IRepository<Role, Guid> _roleRepository;
        private readonly IValidator<CreateRoleCommand> _validator;
        public CreateRoleCommandHandler(
            IRepository<Role, Guid> roleRepository
            , IValidator<CreateRoleCommand> validator
        )
        {
            _roleRepository = roleRepository;
            _validator = validator;
        }

        public async Task<CreateRoleResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var existingRoleByName = await _roleRepository.FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
            if (existingRoleByName != null)
            {
                return new CreateRoleResponse
                {
                    Code = 409,
                    Message = "Role already exists"
                };
            }

            var checkValid = _validator.Validate(request);
            if (!checkValid.IsValid)
            {
                throw new ValidationException(checkValid.Errors);
            }

            var newRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedTime = DateTime.UtcNow,
                Slug = SlugHelper.StringToSlug(request.Name)
            };
            await _roleRepository.AddAsync(newRole, cancellationToken);

            return new CreateRoleResponse
            {
                Code = 200,
                Message = "Success"
            };
        }
    }
}

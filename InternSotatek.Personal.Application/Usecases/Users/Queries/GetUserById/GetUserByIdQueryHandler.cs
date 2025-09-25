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

namespace InternSotatek.Personal.Application.Usecases.Users.Queries.GetUserById;

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

        var userById = await UserExist(query.Id, cancellationToken);

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
    private async Task<User> UserExist(Guid id, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User doesn't exist");
        }
        return existingUser;
    }
}

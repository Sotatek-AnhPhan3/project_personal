using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Users.UseCases.Commands.SoftDelete;

public class SoftDeleteUserCommandHandler : IRequestHandler<SoftDeleteUserCommand, SoftDeleteUserResponse>
{
    private readonly IRepository<User, Guid> _userRepository;

    public SoftDeleteUserCommandHandler(IRepository<User, Guid> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<SoftDeleteUserResponse> Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        existingUser.IsActive = false;
        await _userRepository.UpdateAsync(existingUser, cancellationToken);
        return new SoftDeleteUserResponse
        {
            Code = 200,
            Message = "Ok"
        };
    }

}

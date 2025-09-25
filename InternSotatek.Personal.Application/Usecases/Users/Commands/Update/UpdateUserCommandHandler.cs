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

namespace InternSotatek.Personal.Application.Usecases.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IRepository<User, Guid> _userRepository;

        public UpdateUserCommandHandler(
            IRepository<User, Guid> userRepository
        )
        {
            _userRepository = userRepository;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await UserExist(request,cancellationToken);

            await UpdateUser(existingUser, request, cancellationToken);

            return new UpdateUserResponse
            {
                Code = 200,
                Message = "Update successfull"
            };
        }

        private async Task<User> UserExist(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User doesn't exist");
            }

            var usernameConflict = await _userRepository.FirstOrDefaultAsync(
                x => x.Username == request.Username && x.Id != request.Id, cancellationToken);

            if (usernameConflict != null)
            {
                throw new ArgumentException("Username already exists");
            }
            return existingUser;
        }

        private async Task UpdateUser(User user, UpdateUserCommand request, CancellationToken cancellationToken) {
            user.Username = request.Username;
            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Dob = request.Dob;
            user.UpdatedTime = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user, cancellationToken);
        }

    }
}

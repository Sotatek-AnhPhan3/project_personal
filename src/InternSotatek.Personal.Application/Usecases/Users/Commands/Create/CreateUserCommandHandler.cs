using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Application.Usecases.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IValidator<CreateUserCommand> _validator;
        private readonly IRepository<User, Guid> _userRepository;

        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Interations = 10000;
        private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        public CreateUserCommandHandler(
            IValidator<CreateUserCommand> validator
            , IRepository<User, Guid> userRepository
        )
        {
            _validator = validator;
            _userRepository = userRepository;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Hash password
            string passwordHashed = HashPassword(request.Password);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                PasswordHashed = passwordHashed,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Dob = request.Dob,
                CreatedTime = DateTime.UtcNow,
            };

            await _userRepository.AddAsync(newUser, cancellationToken);

            return new CreateUserResponse { Code = 200 };
        }

        private string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Interations, Algorithm, HashSize);
            string passwordHashed = $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
            return passwordHashed;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Application.Utils;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using InternSotatek.Personal.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InternSotatek.Personal.Application.Usecases.Account.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IRepository<User, Guid> _userRepository;
    private readonly IConfiguration _configuration;

    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 10000;
    private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
    public LoginUserCommandHandler(
        IRepository<User, Guid> userRepository
        , IConfiguration configuration
    )
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand query, CancellationToken cancellationToken)
    {
        var existingUserByUsername = await UserExist(query.Username, cancellationToken);

        // Verify password
        var checkPassword = VerifyPassword(query.Password, existingUserByUsername.PasswordHashed);
        if (!checkPassword)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        // Check is active
        if (!existingUserByUsername.IsActive)
        {
            throw new UnauthorizedAccessException("Account locked");
        }

        // Generate jwt token
        var token = JwtGenerate.GenerateToken(existingUserByUsername, _configuration);

        return new LoginUserResponse
        {
            Code = 200,
            Message = "Login successfull",
            AccessToken = token,
        };
    }

    // Decode hash password
    private bool VerifyPassword(string inputPassword, string passwordHashed)
    {
        var hash = Convert.FromHexString(passwordHashed.Split('-')[0]);
        var salt = Convert.FromHexString(passwordHashed.Split('-')[1]);

        var computedHash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, Algorithm, HashSize);

        return computedHash.SequenceEqual(hash);
    }
    private async Task<User> UserExist(string username, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User doesn't exist");
        }
        return existingUser;
    }
}

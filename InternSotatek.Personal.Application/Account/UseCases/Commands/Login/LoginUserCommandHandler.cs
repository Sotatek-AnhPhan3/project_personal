using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Application.Utils;
using InternSotatek.Personal.Domain.Entities;
using InternSotatek.Personal.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InternSotatek.Personal.Application.Account.UseCases.Commands.Login
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
	{
		private readonly PersonalDbContext _dbContext;
		private readonly IConfiguration _configuration;

		private const int SaltSize = 16;
		private const int HashSize = 32;
		private const int Iterations = 10000;
		private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
		public LoginUserCommandHandler(
			PersonalDbContext dbContext
			, IConfiguration configuration
		)
		{
			_dbContext = dbContext;
			_configuration = configuration;
		}

		public async Task<LoginUserResponse> Handle(LoginUserCommand query, CancellationToken cancellationToken)
		{
			var existingUserByUsername = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == query.Username, cancellationToken);
			if (existingUserByUsername == null)
			{
				return new LoginUserResponse
				{
					Code = 404,
					Message = "User not found"
				};
			}

			var hash = Convert.FromHexString(existingUserByUsername.PasswordHashed.Split('-')[0]);
			var salt = Convert.FromHexString(existingUserByUsername.PasswordHashed.Split('-')[1]);

			var computedHash = Rfc2898DeriveBytes.Pbkdf2(query.Password, salt, Iterations, Algorithm, HashSize);
			if (!computedHash.SequenceEqual(hash))
				throw new Exception("Invalid username or password");
			var token = JwtGenerate.GenerateToken(existingUserByUsername, _configuration);

			return new LoginUserResponse
			{
				Code = 200,
				Message = "Login successfull",
				AccessToken = token,
			};
		}

	}

}

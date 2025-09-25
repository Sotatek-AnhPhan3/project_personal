using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace InternSotatek.Personal.Application.Utils
{
	public static class JwtGenerate
	{
		public static string GenerateToken(User user, IConfiguration configuration)
		{
			var secretKey = configuration["JwtSettings:Secret"];
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
								new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Username),
								new Claim("id", user.Id.ToString())
						};

			var token = new JwtSecurityToken(
					issuer: configuration["JwtSettings:Issuer"],
					audience: configuration["JwtSettings:Audience"],
					claims: claims,
					expires: DateTime.UtcNow.AddMinutes(double.Parse(configuration["JwtSettings:ExpiresInMinutes"])),
					signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}

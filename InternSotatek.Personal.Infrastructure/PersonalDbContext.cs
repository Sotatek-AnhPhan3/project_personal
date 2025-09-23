using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternSotatek.Personal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternSotatek.Personal.Infrastructure
{
	public class PersonalDbContext(DbContextOptions<PersonalDbContext> options) : DbContext(options)
	{
		public DbSet<User> Users { get; set; }

	}
}

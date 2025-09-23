using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Domain.Entities
{
	[Table("Users")]
	public class User
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		public string PasswordHashed { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		public bool IsActive { get; set; } = true;
		public DateTime? Dob {  get; set; }
		public DateTime CreatedTime { get; set; }
		public DateTime? UpdatedTime { get; set; }
	}
}

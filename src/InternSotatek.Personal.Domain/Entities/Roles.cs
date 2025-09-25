using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Domain.Entities
{
	[Table("Roles")]
	public class Role
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Slug { get; set; }
		public DateTime CreatedTime { get; set; }
		public DateTime? UpdatedTime { get; set; }
	}
}

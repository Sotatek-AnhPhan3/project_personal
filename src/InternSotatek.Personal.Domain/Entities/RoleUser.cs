using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Domain.Entities
{
    [Table("UserRoles")]
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        [Required]
        public Role Role { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}

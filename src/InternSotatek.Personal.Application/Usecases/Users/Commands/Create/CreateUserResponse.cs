using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Application.Usecases.Users.Commands.Create;

public class CreateUserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string PasswordHashed { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public DateOnly? Dob { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
}

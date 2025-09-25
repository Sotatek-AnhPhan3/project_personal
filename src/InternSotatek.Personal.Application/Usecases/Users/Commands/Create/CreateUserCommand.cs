using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InternSotatek.Personal.Application.Usecases.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly? Dob { get; set; }
    }
}

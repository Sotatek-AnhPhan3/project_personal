using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSotatek.Personal.Application.Usecases.Account.Commands.Login
{
    public class LoginUserResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

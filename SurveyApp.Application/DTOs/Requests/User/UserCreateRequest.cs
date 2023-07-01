using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Requests.User
{
    public class UserCreateRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AccountType { get; set; } = null!;
    }
}

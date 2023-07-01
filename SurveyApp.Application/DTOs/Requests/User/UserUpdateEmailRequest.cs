using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Requests.User
{
    public class UserUpdateEmailRequest
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.User
{
    public class UserDisplayResponse
    {
        public string Email { get; set; } = null!;
        public string AccountType { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.User
{
    public class UserLoginResponse
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public UserLoginResponse(string id, string token)
        {
            Id = id;
            Token = token;
        }
    }
}

using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.Services.Common;
using SurveyApp.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.UserService
{
    public interface IValidationService : IService
    {
        public Task<User?> ValidateUserAsync(UserLoginRequest userLoginRequest);
        public User ValidateUser(UserLoginRequest userLoginRequest);
    }
}

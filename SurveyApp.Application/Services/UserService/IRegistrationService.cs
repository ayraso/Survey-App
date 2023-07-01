using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.UserService
{
    public interface IRegistrationService : IService
    {
        public Task<bool> IsEmailRegisteredAsync(string email);
        public bool IsEmailRegistered(string email);
        public Task<string> CreateUserAndReturnIdAsync(UserCreateRequest userCreateRequest);
        public string CreateUserAndReturnId(UserCreateRequest userCreateRequest);
        public Task CreateUserAsync(UserCreateRequest userCreateRequest);
        public void CreateUser(UserCreateRequest userCreateRequest);
    }
}

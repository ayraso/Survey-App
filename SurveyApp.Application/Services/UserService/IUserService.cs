using SurveyApp.Application.DTOs.Responses.User;
using SurveyApp.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.UserService
{
    public interface IUserService : IAccountManagementService, IAuthenticationService, IRegistrationService
    {
        Task<IEnumerable<User?>> GetAllUsersAsync();
        IEnumerable<User?> GetAllUsers();
        Task DeleteUserAccountAsync(string UserId);
        void DeleteUserAccount(string UserId);
        Task<User?> GetUserByIdAsync(string UserId);
        User? GetUserById(string UserId);
        Task<bool> IsUserExistsAsync(string UserId);
        bool IsUserExists(string UserId);

    }
}

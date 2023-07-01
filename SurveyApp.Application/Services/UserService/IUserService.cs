using SurveyApp.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.UserService
{
    public interface IUserService : IAccountManagementService, IValidationService, IRegistrationService
    {
        Task<IEnumerable<User?>> GetAllUsersAsync();
        IEnumerable<User?> GetAllUsers();
        Task DeleteUserAccountAsync(string UserId);
        void DeleteUserAccount(string UserId);
    }
}

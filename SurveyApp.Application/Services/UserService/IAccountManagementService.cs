using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.DTOs.Responses.User;
using SurveyApp.Application.Services.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.UserService
{
    public interface IAccountManagementService : IService
    {
        public Task UpdateUserPasswordAsync(UserUpdatePasswordRequest userUpdatePasswordRequest);
        public void UpdateUserPassword(UserUpdatePasswordRequest userUpdatePasswordRequest);
        public Task<UserDisplayResponse> UpdateUserEmailAsync(UserUpdateEmailRequest userUpdateEmailRequest);
        public UserDisplayResponse UpdateUserEmail(UserUpdateEmailRequest userUpdateEmailRequest);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.UserService
{
    public interface IUserService : IAccountManagementService, IValidationService, IRegistrationService
    {
        
    }
}

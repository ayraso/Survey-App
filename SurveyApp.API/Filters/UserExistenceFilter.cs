using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.Services.UserService;

namespace SurveyApp.API.Filters
{
    public class UserExistenceFilter : IAsyncActionFilter
    {
        public readonly IUserService _userService;
        public UserExistenceFilter(IUserService userService)
        {
            _userService = userService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
            var IsMethodWithUserIdParameter = context.ActionArguments.ContainsKey("userId");
            var IsMethodWithUserUpdatePasswordRequestParameter = context.ActionArguments.ContainsKey("userUpdatePasswordRequest");
            var IsMethodWithUserUpdateEmailRequestParameter = context.ActionArguments.ContainsKey("userUpdateEmailRequest");

            string userId = null;

            if (IsMethodWithUserIdParameter == true)
            {
                userId = context.ActionArguments["userId"].ToString();
                
            }
            else if(IsMethodWithUserUpdatePasswordRequestParameter == true)
            {
                var request = (UserUpdatePasswordRequest)context.ActionArguments["userUpdatePasswordRequest"];
                userId = request.Id;
            }
            else if(IsMethodWithUserUpdateEmailRequestParameter == true)
            {
                var request = (UserUpdateEmailRequest)context.ActionArguments["userUpdateEmailRequest"];
                userId = request.Id;
            }

            var isUserExists = await _userService.IsUserExistsAsync(userId);
            if (isUserExists == true)
            {
                await next.Invoke();
            }
            else
            {
                context.Result = new NotFoundObjectResult(new { message = $"No user found with provided id." });
            }

        }
    }
}

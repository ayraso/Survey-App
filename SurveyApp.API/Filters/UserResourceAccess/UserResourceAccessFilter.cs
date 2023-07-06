using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Domain.Entities.Users;
using System.Web.Http.Controllers;

namespace SurveyApp.API.Filters.UserResourceAccess
{
    public class UserResourceAccessFilter : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string? claimUserId = null;
        public UserResourceAccessFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var IsMethodWithUserIdParameter = context.ActionArguments.ContainsKey("userId");
            var IsMethodWithUserUpdatePasswordRequestParameter = context.ActionArguments.ContainsKey("userUpdatePasswordRequest");
            var IsMethodWithUserUpdateEmailRequestParameter = context.ActionArguments.ContainsKey("userUpdateEmailRequest");
            var IsMethodWithSurveyCreateRequestParameter = context.ActionArguments.ContainsKey("surveyCreateRequest");

            string userId = null;

            if (IsMethodWithUserIdParameter == true)
            {
                userId = context.ActionArguments["userId"].ToString();

            }
            else if (IsMethodWithUserUpdatePasswordRequestParameter == true)
            {
                var request = (UserUpdatePasswordRequest)context.ActionArguments["userUpdatePasswordRequest"];
                userId = request.Id;
            }
            else if (IsMethodWithUserUpdateEmailRequestParameter == true)
            {
                var request = (UserUpdateEmailRequest)context.ActionArguments["userUpdateEmailRequest"];
                userId = request.Id;
            }
            else if (IsMethodWithSurveyCreateRequestParameter == true)
            {
                var request = (SurveyCreateRequest)context.ActionArguments["surveyCreateRequest"];
                userId = request.UserIdCreatedBy;
            }

            claimUserId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            var isUserHasAccessRight = false;
            if (userId == claimUserId) isUserHasAccessRight = true;

            if (isUserHasAccessRight == true)
            {
                await next.Invoke();
            }
            else
            {
                context.Result = new UnauthorizedObjectResult(new { message = $"No access right for this resouce." });
            }
        }
    }
}

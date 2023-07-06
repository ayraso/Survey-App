using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Domain.Entities.Users;

namespace SurveyApp.API.Filters
{
    public class SurveyResourceAccessFilter : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISurveyService _surveyService;
        private string? claimUserId = null;
        public SurveyResourceAccessFilter(IHttpContextAccessor httpContextAccessor, ISurveyService surveyService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._surveyService = surveyService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var IsMethodWithSurveyIdParameter = context.ActionArguments.ContainsKey("surveyId");

            this.claimUserId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            string surveyId = null;

            if (IsMethodWithSurveyIdParameter == true)
            {
                surveyId = context.ActionArguments["surveyId"].ToString();
            }

            bool isUserSurveyOwner = await _surveyService.CheckSurveyOwnershipAsync(surveyId, claimUserId);
            if (isUserSurveyOwner == true)
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

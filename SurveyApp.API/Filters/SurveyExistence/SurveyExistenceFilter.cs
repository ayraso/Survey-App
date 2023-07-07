using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SurveyApp.Application.DTOs.Requests.SurveyResponse;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Domain.Entities.Users;

namespace SurveyApp.API.Filters.SurveyExistence
{
    public class SurveyExistenceFilter : IAsyncActionFilter
    {
        public readonly ISurveyService _surveyService;
        public SurveyExistenceFilter(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string surveyId = null;
            var IsMethodWithSurveyParameter = context.ActionArguments.ContainsKey("surveyId");
            var IsMethodWithSurveyResponseCreateRequestParameter = context.ActionArguments.ContainsKey("surveyResponse");

            if (IsMethodWithSurveyParameter == true)
            {
                surveyId = context.ActionArguments["surveyId"].ToString();

            }
            else if (IsMethodWithSurveyResponseCreateRequestParameter == true)
            {
                var request = (SurveyResponseCreateRequest)context.ActionArguments["surveyResponse"];
                surveyId = request.SurveyId;
            }

            var isSurveyExists = await _surveyService.IsSurveyExistsAsync(surveyId);
            if (isSurveyExists == true)
            {
                await next.Invoke();
            }
            else
            {
                context.Result = new NotFoundObjectResult(new { message = $"No survey found with provided id." });
            }
        }
    }
}

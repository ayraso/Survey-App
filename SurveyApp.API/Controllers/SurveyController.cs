using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.API.Filters;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Domain.Entities.Surveys;
using SurveyApp.Domain.Entities.Users;

namespace SurveyApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IUserService _userService; 

        public SurveyController(ISurveyService surveyService, IUserService userService)
        {
            this._surveyService = surveyService;
            this._userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("/Survey/All")]
        public async Task<IActionResult> GetSurveys()
        {
            var surveys = await _surveyService.GetAllSurveysAsync();
            return Ok(surveys);
        }

        [AllowAnonymous]
        [HttpGet("/Survey/{surveyId}")]
        [SurveyExistence]
        public async Task<IActionResult> GetSurveyById(string surveyId)
        {
            if (surveyId != null)
            {
                var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
                return Ok(survey);
            }
            return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpGet("/Survey/User:{userId}")]
        [UserExistence]
        [UserResourceAccess]
        public async Task<IActionResult> GetSurveysByUserId(string userId)
        {
            if (userId != null)
            {
               var surveys = await _surveyService.GetSurveysByUserIdAsync(userId);
               return Ok(surveys);
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost("/Survey/Create")]
        [UserExistence]
        [UserResourceAccess]
        public async Task<IActionResult> CreateSurvey(SurveyCreateRequest surveyCreateRequest)
        {
            if (ModelState.IsValid)
            {
                await _surveyService.CreateSurveyAsync(surveyCreateRequest);
                return Ok();
            }
            return BadRequest();
        }
        
    }
}

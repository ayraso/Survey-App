using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.API.Filters;
using SurveyApp.API.Filters.SurveyExistence;
using SurveyApp.API.Filters.UserExistence;
using SurveyApp.API.Filters.UserResourceAccess;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.Services.CachingServices;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Domain.Entities.Surveys;
using SurveyApp.Domain.Entities.Users;
using System.Net;

namespace SurveyApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IUserService _userService;
        private readonly IMemoryCacheService _cacheService;


        public SurveyController(ISurveyService surveyService, 
                                IUserService userService,
                                IMemoryCacheService cacheService)
        {
            this._surveyService = surveyService;
            this._userService = userService;
            this._cacheService = cacheService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("/Surveys/All")]
        public async Task<IActionResult> GetSurveys()
        {
            var surveys = await _surveyService.GetAllSurveysAsync();
            return Ok(surveys);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/Surveys/{surveyId}")]
        [SurveyExistence]
        public async Task<IActionResult> GetSurveyById(string surveyId)
        {
            if (surveyId != null)
            {
                //var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
                var cacheKey = $"Survey_{surveyId}";
                var survey = await _cacheService.GetOrCreateAsync(cacheKey, () =>
                {
                    return _surveyService.GetSurveyByIdAsync(surveyId);
                }, TimeSpan.FromMinutes(5));

                return Ok(survey);
            }
            return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("/Surveys/UserId:{userId}/All")]
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
        [HttpPost]
        [Route("/Surveys/Create")]
        [UserExistence]
        [UserResourceAccess]
        public async Task<IActionResult> CreateSurvey(SurveyCreateRequest surveyCreateRequest)
        {
            if (ModelState.IsValid)
            {
                string justCreatedSurveyId = await _surveyService.CreateSurveyAsync(surveyCreateRequest);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                string uri = Url.ActionLink("GetSurveyById", "Survey", new {surveyId = justCreatedSurveyId });
                response.Headers.Location = new Uri(uri);
                return Ok(response);
            }
            return BadRequest();
        }
        
    }
}

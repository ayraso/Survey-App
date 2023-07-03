using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Domain.Entities.Surveys;
using SurveyApp.Domain.Entities.Users;

namespace SurveyApp.API.Controllers
{
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

        [HttpGet("/Survey/All")]
        public async Task<IActionResult> GetSurveys()
        {
            var surveys = await _surveyService.GetAllSurveysAsync();
            return Ok(surveys);
        }

        [HttpGet("/Survey/{surveyId}")]
        public async Task<IActionResult> GetSurveyById(string surveyId)
        {
            if (surveyId != null)
            {
                bool isSurveyExists = await _surveyService.IsSurveyExistsAsync(surveyId);
                if (isSurveyExists)
                {
                    var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
                    return Ok(survey);
                }
                return NotFound(new { message = $"Böyle bir survey bulunamadı." });
            }
            return BadRequest();

        }

        [HttpGet("/Survey/User:{userId}")]
        public async Task<IActionResult> GetSurveyByUserId(string userId)
        {
            if (userId != null)
            {
                bool isUserExists = await _userService.IsUserExistsAsync(userId);
                if (isUserExists)
                {
                    var surveys = await _surveyService.GetSurveysByUserIdAsync(userId);
                    return Ok(surveys);
                }
                return NotFound(new { message = $"Böyle bir kullanıcı bulunamadı." });
            }
            return BadRequest();
        }

        [HttpPost("/Survey/Create")]
        public async Task<IActionResult> CreateSurvey(SurveyCreateRequest survey)
        {
            if(ModelState.IsValid) 
            {
                await _surveyService.CreateSurveyAsync(survey);
                return Ok();
            }
            return BadRequest();
        }
    }
}

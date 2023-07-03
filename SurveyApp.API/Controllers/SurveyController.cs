using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.DTOs.Responses.Survey;
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

        public SurveyController(ISurveyService surveyService)
        {
            this._surveyService = surveyService;
        }

        [HttpGet("Surveys/All")]
        public async Task<IEnumerable<Survey?>> GetAllSurveys()
        {
            var surveys = await _surveyService.GetAllSurveysAsync();
            return surveys;
        }

        [HttpGet("Surveys/{id}")]
        public async Task<Survey?> GetSurveyById(string surveyId)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
            return survey;
        }

        [HttpPost("Surveys/Create")]
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

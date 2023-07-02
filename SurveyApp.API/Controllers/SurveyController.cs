using Microsoft.AspNetCore.Mvc;
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
            return await _surveyService.GetAllSurveysAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSurvey(Survey survey)
        {
            await _surveyService.CreateSurvey(survey);
            return Ok();
        }
    }
}

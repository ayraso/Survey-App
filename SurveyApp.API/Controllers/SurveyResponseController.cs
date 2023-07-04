using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.DTOs.Requests.SurveyResponse;
using SurveyApp.Application.Services.SurveyResponseService;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Domain.Entities.SurveyResponses;

namespace SurveyApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurveyResponseController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IUserService _userService;
        private readonly ISurveyResponseService _surveyResponseService;

        public SurveyResponseController(ISurveyService surveyService,
                                        IUserService userService,
                                        ISurveyResponseService surveyResponseService)
        {
            this._surveyService = surveyService;
            this._userService = userService;
            this._surveyResponseService = surveyResponseService;
        }

        [HttpGet("/Surveys/{surveyId}/Responses")]
        public async Task<IActionResult> GetResponsesBySurveyId(string surveyId)
        {
            if(surveyId != null)
            {
                bool isSurveyExists = await _surveyService.IsSurveyExistsAsync(surveyId);
                if (isSurveyExists)
                {
                    var responses = await _surveyResponseService.GetSurveyResponsesBySurveyIdAsync(surveyId);
                    return Ok(responses);
                }
                return NotFound(new { message = $"Böyle bir anket bulunamadı." });
            }
            return BadRequest();
        }

        [HttpGet("/Surveys/{surveyId}/Analyzes")]
        public async Task<IActionResult> GetSurveyAnalysisBySurveyId(string surveyId)
        {
            if (surveyId != null)
            {
                bool isSurveyExists = await _surveyService.IsSurveyExistsAsync(surveyId);
                if (isSurveyExists)
                {
                    var analysis = await _surveyResponseService.AnalyzeSurveyAsync(surveyId);
                    return Ok(analysis);
                }
                return NotFound(new { message = $"Böyle bir anket bulunamadı." });
            }
            return BadRequest();
        }

        [HttpPost("/Surveys/{surveyId}/CreateResponse")]
        public async Task<IActionResult> CreateSurveyResponse(SurveyResponseCreateRequest surveyResponse)
        {
            if (ModelState.IsValid)
            {
                bool isSurveyExists = await _surveyService.IsSurveyExistsAsync(surveyResponse.SurveyId);
                if (isSurveyExists)
                {
                    await _surveyResponseService.CreateSurveyResponseAsync(surveyResponse);
                    return Ok();
                }
                return NotFound(new { message = $"Böyle bir anket bulunamadı." });
            }
            return BadRequest();
        }


    }
}

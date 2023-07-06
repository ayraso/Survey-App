using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.API.Filters;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.DTOs.Requests.SurveyResponse;
using SurveyApp.Application.Services.SurveyResponseService;
using SurveyApp.Application.Services.SurveyService;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Domain.Entities.SurveyResponses;
using System.Security.Claims;

namespace SurveyApp.API.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "User")]
        [HttpGet("/Surveys/{surveyId}/Responses")]
        [SurveyExistence]
        [SurveyResourceAccess]
        public async Task<IActionResult> GetResponsesBySurveyId(string surveyId)
        {
            if(surveyId != null)
            {
                var responses = await _surveyResponseService.GetSurveyResponsesBySurveyIdAsync(surveyId);
                return Ok(responses);
            }
            return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpGet("/Surveys/{surveyId}/Analyzes")]
        [SurveyExistence]
        [SurveyResourceAccess]
        public async Task<IActionResult> GetSurveyAnalysisBySurveyId(string surveyId)
        {
            if (surveyId != null)
            {
                var analysis = await _surveyResponseService.AnalyzeSurveyAsync(surveyId);
                return Ok(analysis);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("/Surveys/{surveyId}/CreateResponse")]
        [SurveyExistence]
        public async Task<IActionResult> CreateSurveyResponse(SurveyResponseCreateRequest surveyResponse)
        {
            if (ModelState.IsValid)
            {
                await _surveyResponseService.CreateSurveyResponseAsync(surveyResponse);
                return Ok();
            }
            return BadRequest();
        }


    }
}

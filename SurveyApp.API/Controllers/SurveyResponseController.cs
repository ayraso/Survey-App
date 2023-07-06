using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.API.Filters;
using SurveyApp.API.Filters.SurveyExistence;
using SurveyApp.API.Filters.SurveyResourceAccess;
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
        [HttpGet]
        [Route("/Surveys/{surveyId}/Responses")]
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
        [HttpGet]
        [Route("/Surveys/{surveyId}/Analyzes")]
        [SurveyExistence]
        [SurveyResourceAccess]
        public async Task<IActionResult> GetSurveyAnalysisBySurveyId(string surveyId)
        {
            if (surveyId != null)
            {
                var analysis = await _surveyResponseService.GetSurveyAnalysisBySurveyIdAsync(surveyId);
                return Ok(analysis);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/Surveys/{surveyId}/SendResponse")]
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

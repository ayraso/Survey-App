using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = "User")]
        [HttpGet("/Surveys/{surveyId}/Analyzes")]
        public async Task<IActionResult> GetSurveyAnalysisBySurveyId(string surveyId)
        {
            string? userId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if(userId == null) return Unauthorized();

            if (surveyId != null)
            {
                bool isSurveyExists = await _surveyService.IsSurveyExistsAsync(surveyId);
                if (isSurveyExists)
                {
                    bool isUserSurveyOwner = await _surveyService.CheckSurveyOwnershipAsync(surveyId, userId);
                    if (isUserSurveyOwner == true)
                    {
                        var analysis = await _surveyResponseService.AnalyzeSurveyAsync(surveyId);
                        return Ok(analysis);
                    }
                    return Unauthorized();
                }
                return NotFound(new { message = $"Böyle bir anket bulunamadı." });
            }
            return BadRequest();
        }

        [AllowAnonymous]
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

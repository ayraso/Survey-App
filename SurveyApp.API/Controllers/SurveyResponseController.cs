using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.API.Filters.SurveyExistence;
using SurveyApp.API.Filters.SurveyResourceAccess;
using SurveyApp.Application.DTOs.Requests.SurveyResponse;
using SurveyApp.Application.Services.CachingServices;
using SurveyApp.Application.Services.SurveyResponseService;


namespace SurveyApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SurveyResponseController : ControllerBase
    {
        private readonly ISurveyResponseService _surveyResponseService;
        private readonly IMemoryCacheService _cacheService;

        public SurveyResponseController(ISurveyResponseService surveyResponseService,
                                        IMemoryCacheService cacheService)
        {
            this._surveyResponseService = surveyResponseService;
            this._cacheService = cacheService;
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
                var responses = await _cacheService.GetOrCreateAsync($"Responses_{surveyId}", () =>
                            _surveyResponseService.GetSurveyResponsesBySurveyIdAsync(surveyId), TimeSpan.FromMinutes(5));
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
                var analysis = await _cacheService.GetOrCreateAsync($"Analysis_{surveyId}", () => 
                            _surveyResponseService.GetSurveyAnalysisBySurveyIdAsync(surveyId), TimeSpan.FromMinutes(5));
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
                _cacheService.Remove($"Analysis_{surveyResponse.SurveyId}");
                _cacheService.Remove($"Responses_{surveyResponse.SurveyId}");

                await _surveyResponseService.CreateSurveyResponseAsync(surveyResponse);
                return Ok();
            }
            return BadRequest();
        }


    }
}

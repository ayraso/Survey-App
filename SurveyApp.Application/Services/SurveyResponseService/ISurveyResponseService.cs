using SurveyApp.Application.DTOs.Requests.SurveyResponse;
using SurveyApp.Application.DTOs.Responses.SurveyResponse;
using SurveyApp.Domain.Entities.SurveyResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.SurveyResponseService
{
    public interface ISurveyResponseService
    {
        Task CreateSurveyResponseAsync(SurveyResponseCreateRequest surveyResponseCreateRequest);
        Task<IEnumerable<SurveyResponse?>> GetSurveyResponsesBySurveyIdAsync(string surveyId);
        Task<SurveyAnalysisResponse> GetSurveyAnalysisBySurveyIdAsync(string surveyId);

    }
}

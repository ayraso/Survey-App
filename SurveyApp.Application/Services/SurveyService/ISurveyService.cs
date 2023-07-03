using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.DTOs.Responses.Survey;
using SurveyApp.Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.SurveyService
{
    public interface ISurveyService
    {
        Task CreateSurveyAsync(SurveyCreateRequest surveyCreeateRequest);
        
        Task<IEnumerable<Survey?>> GetAllSurveysAsync();
        Task<Survey?> GetSurveyByIdAsync(string surveyId);

    }
}

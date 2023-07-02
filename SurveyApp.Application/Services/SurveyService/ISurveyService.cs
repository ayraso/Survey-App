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
        void CreateSurvey(SurveyCreateRequest surveyCreateRequest);
        Task<string> CreateSurveyAndReturnIdAsync(SurveyCreateRequest surveyCreateRequest);
        string CreateSurveyAndReturnId(SurveyCreateRequest surveyCreateRequest);
        Task<IEnumerable<SurveyDisplayResponse?>> GetAllSurveysAsync();
        IEnumerable<SurveyDisplayResponse?> GetAllSurveys();
        Task<Survey?> GetSurveyBySurveyIdAsync(string surveyId);
        SurveyDisplayResponse? GetSurveyBySurveyId(string surveyId);
        Task<IEnumerable<SurveyDisplayResponse?>> GetSurveysByUserIdAsync(string userId);
        IEnumerable<SurveyDisplayResponse?> GetSurveysByUserId(string userId);

    }
}

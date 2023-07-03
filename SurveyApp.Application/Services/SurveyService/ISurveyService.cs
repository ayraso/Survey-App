using SurveyApp.Application.DTOs.Requests.Survey;
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
        void CreateSurvey(SurveyCreateRequest surveyCreeateRequest);
        Task<IEnumerable<Survey?>> GetAllSurveysAsync();
        IEnumerable<Survey?> GetAllSurveys();
        Task<Survey?> GetSurveyByIdAsync(string surveyId);
        Survey? GetSurveyById(string surveyId);
        Task<IEnumerable<Survey?>> GetSurveysByUserIdAsync(string userId);
        IEnumerable<Survey?> GetSurveysByUserId(string userId);
        Task<bool> IsSurveyExistsAsync(string surveyId);
        bool IsUserExists(string surveyId);
    }
}

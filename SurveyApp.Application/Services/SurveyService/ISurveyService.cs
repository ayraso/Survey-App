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
        Task CreateSurvey(SurveyCreateRequest newSurvey);
        Task<IEnumerable<Survey?>> GetAllSurveysAsync();
    }
}

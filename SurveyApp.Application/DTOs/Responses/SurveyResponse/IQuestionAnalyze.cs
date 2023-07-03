using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.SurveyResponse
{
    public interface IQuestionAnalyze
    {
        string SurveyId { get; set; }
        string Index { get; set; }
    }
}

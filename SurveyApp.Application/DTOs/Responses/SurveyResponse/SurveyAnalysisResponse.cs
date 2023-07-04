using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.SurveyResponse
{
    public class SurveyAnalysisResponse
    {
        public string SurveyId { get; set; } = null!;
        public string TotalResponses { get; set; }
        public List<QuestionAnalyze> QuestionAnalyzes { get; set; } = new List<QuestionAnalyze>();
    }
}

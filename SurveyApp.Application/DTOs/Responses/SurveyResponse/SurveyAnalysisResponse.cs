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
        public long TotalResponses { get; set; }
        public IEnumerable<QuestionAnalyze> QuestionAnalyzes { get; set; } = Enumerable.Empty<QuestionAnalyze>();
    }
}

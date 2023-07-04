using SurveyApp.Application.DTOs.Responses.SurveyResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses
{
    public class RangeQuestionAnalyzeResponse : QuestionAnalyze
    {
        public string SurveyId { get; set; } = null!;
        public string Index { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public string NumOfVotes { get; set; } = null!;
        public string RateOfVote { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.SurveyResponse
{
    public class ShortAnswerQuestionAnalyzeResponse : QuestionAnalyze
    {
        public string SurveyId { get; set; } = null!;
        public string Index { get; set; } = null!;
        public List<string> Answers { get; set; } = null!;
    }
}

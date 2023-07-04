using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.SurveyResponse
{
    public class LongAnswerQuestionAnalyzeResponse : QuestionAnalyze
    {
        public List<string> Answers { get; set; } = null!;
    }
}

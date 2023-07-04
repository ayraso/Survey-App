using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.SurveyResponse
{
    public class ShortAnswerQuestionAnalyzeResponse : QuestionAnalyze
    {
        public List<string> Answers { get; set; } = null!;
    }
}

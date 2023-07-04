using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.SurveyResponse
{
    public class RangeQuestionAnalyzeResponse : QuestionAnalyze
    {
        public List<AnswerAnalyze> AnswerAnalyzes { get; set; } = new List<AnswerAnalyze>();
    }
}

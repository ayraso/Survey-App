using SurveyApp.Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.SurveyResponse
{
    [JsonDerivedType(typeof(LongAnswerQuestionAnalyzeResponse))]
    [JsonDerivedType(typeof(ShortAnswerQuestionAnalyzeResponse))]
    [JsonDerivedType(typeof(RangeQuestionAnalyzeResponse))]
    [JsonDerivedType(typeof(MultiChoiceQuestionAnalyzeResponse))]
    public abstract class QuestionAnalyze
    {
        public string SurveyId { get; set; }
        public string Index { get; set; }
    }
}

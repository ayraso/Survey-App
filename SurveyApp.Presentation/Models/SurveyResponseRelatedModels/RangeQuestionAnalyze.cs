using System.Text.Json.Serialization;

namespace SurveyApp.Presentation.Models.SurveyResponseRelatedModels
{
    public class RangeQuestionAnalyze : QuestionAnalyze
    {
        [JsonPropertyName("AnswerAnalyzes")]
        public List<AnswerAnalyze> AnswerAnalyzes { get; set; } = new List<AnswerAnalyze>();
    }
}
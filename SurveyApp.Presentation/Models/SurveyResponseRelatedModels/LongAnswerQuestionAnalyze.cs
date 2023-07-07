using System.Text.Json.Serialization;

namespace SurveyApp.Presentation.Models.SurveyResponseRelatedModels
{
    public class LongAnswerQuestionAnalyze : QuestionAnalyze
    {
        [JsonPropertyName("Answers")]
        public List<string> Answers { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace SurveyApp.Presentation.Models.SurveyResponseRelatedModels
{
    public class ShortAnswerQuestionAnalyze : QuestionAnalyze
    {
        [JsonPropertyName("Answers")]
        public List<string> Answers { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace SurveyApp.Presentation.Models.SurveyResponseRelatedModels
{
    public class SurveyAnalysis
    {
        public string SurveyId { get; set; }
        public string TotalResponses { get; set; }

        [JsonPropertyName("QuestionAnalyzes")]
        public List<QuestionAnalyze> QuestionAnalyzes { get; set; } = new List<QuestionAnalyze>();
    }
}
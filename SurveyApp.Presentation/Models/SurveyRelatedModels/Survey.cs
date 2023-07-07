using SurveyApp.Presentation.Models.SurveyRelatedModels.QuestionModels;
using System.Text.Json.Serialization;

namespace SurveyApp.Presentation.Models.SurveyRelatedModels
{
    public class Survey
    {
        
        public string? Id { get; set; }
        public string UserIdCreatedBy { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }

        [JsonPropertyName("Questions")]
        public List<Question> Questions { get; set; } = new List<Question>();
        public DateTime CreatedAt { get; set; }
    }
}

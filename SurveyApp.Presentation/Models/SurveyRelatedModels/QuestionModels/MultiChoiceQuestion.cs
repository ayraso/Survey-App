using System.Text.Json.Serialization;

namespace SurveyApp.Presentation.Models.SurveyRelatedModels.QuestionModels
{
    public class MultiChoiceQuestion : Question
    {
        [JsonPropertyName("Choices")]
        public IList<string> Choices { get; set; }

    }
}

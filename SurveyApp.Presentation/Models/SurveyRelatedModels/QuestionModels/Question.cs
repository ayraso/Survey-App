using System.Text.Json.Serialization;

namespace SurveyApp.Presentation.Models.SurveyRelatedModels.QuestionModels
{
    [JsonDerivedType(typeof(LongAnswerQuestion))]
    [JsonDerivedType(typeof(ShortAnswerQuestion))]
    [JsonDerivedType(typeof(RangeQuestion))]
    [JsonDerivedType(typeof(MultiChoiceQuestion))]
    public class Question
    {
        public string? Index { get; set; }
        public string Type { get; set; }
        public string? Text { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace SurveyApp.Presentation.Models.SurveyResponseRelatedModels
{
    [JsonDerivedType(typeof(LongAnswerQuestionAnalyze), typeDiscriminator: "LongAnswer")]
    [JsonDerivedType(typeof(ShortAnswerQuestionAnalyze), typeDiscriminator: "ShortAnswer")]
    [JsonDerivedType(typeof(RangeQuestionAnalyze), typeDiscriminator: "Range")]
    [JsonDerivedType(typeof(MultiChoiceQuestionAnalyze), typeDiscriminator: "MultiChoice")]
    [JsonDerivedType(typeof(QuestionAnalyze), typeDiscriminator: "QuestionAnalyze")]
    public class QuestionAnalyze
    {
        public string SurveyId { get; set; }
        public string Index { get; set; }
    }
}

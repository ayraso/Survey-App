namespace SurveyApp.Presentation.Models.SurveyRelatedModels.QuestionModels
{
    public class RangeQuestion : Question
    {
        public string MinRange { get; set; } = "1";
        public string MaxRange { get; set; }

    }
}

using SurveyApp.Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Requests.Survey
{
    public class MultiChoiceQuestionCreateRequest
    {
        public string Type { get; set; } = null!;
        public string Text { get; set; } = null!;
        public IList<string> Choices { get; set; } = null!;
    }
}

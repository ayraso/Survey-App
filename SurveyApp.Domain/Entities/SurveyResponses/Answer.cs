using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.SurveyResponses
{
    public class Answer
    {
        public string QuestionIndex { get; set; } = null!;
        public string AnswerText { get; set; } = null!;
    }
}

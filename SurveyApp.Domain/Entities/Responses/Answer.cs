using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Responses
{
    public class Answer
    {
        public string QuestionId { get; set; } = null!;
        public string AnswerText { get; set; } = null!;
    }
}

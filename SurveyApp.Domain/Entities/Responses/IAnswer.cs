using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Responses
{
    public interface IAnswer
    {
        public string QuestionId { get; set; }
        public string AnswerText { get; set; }
    }
}

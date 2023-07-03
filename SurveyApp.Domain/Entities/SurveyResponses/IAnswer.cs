using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.SurveyResponses
{
    public interface IAnswer
    {
        public string QuestionIndex { get; set; }
    }
}

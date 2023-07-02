using SurveyApp.Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Surveys
{
    public interface ISurvey
    {
        public string UserIdCreatedBy { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
    }
}

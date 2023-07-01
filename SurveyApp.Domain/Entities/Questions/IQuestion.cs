using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Questions
{
    public interface IQuestion
    {
        public string Type { get; set; }
        public string Text { get; set; }
    }
}

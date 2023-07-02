using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Questions
{
    public class Question : IQuestion
    {
        public string Type { get; set; }
        public string Text { get; set; }

        [JsonConstructor]
        public Question(string type, string text)
        {
            Type = type;
            Text = text;
        }
    }
}

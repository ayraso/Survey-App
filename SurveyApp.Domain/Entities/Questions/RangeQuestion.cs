using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Questions
{
    public class RangeQuestion : Question
    {
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string MinRange { get; set; } = "1";
        public string MaxRange { get; set; } = null!;
        public RangeQuestion(IQuestion question) : base(question.Type, question.Text)
        {

        }
        
    }
}

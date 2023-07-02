using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SurveyApp.Domain.Entities.Questions
{
    public class LongAnswerQuestion : Question
    {
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [JsonConstructor]
        public LongAnswerQuestion(IQuestion question) :base(question.Type, question.Text)
        {
        
        }
    }
}

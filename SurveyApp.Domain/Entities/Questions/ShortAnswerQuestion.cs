using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Questions
{
    public class ShortAnswerQuestion : Question
    {
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public ShortAnswerQuestion(IQuestion question) : base(question.Type, question.Text)
        {

        }
    }
}

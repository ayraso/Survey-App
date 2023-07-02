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
    public class MultiChoiceQuestion : Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Choices")]
        [JsonPropertyName("Choices")]
        public IList<string> Choices { get; set; } = null!;

        public MultiChoiceQuestion(IQuestion question) : base(question.Type, question.Text)
        {

        }
    }
}

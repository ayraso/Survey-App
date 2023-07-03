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
    [BsonDiscriminator("MultiChoiceQuestion")]
    public class MultiChoiceQuestion : Question
    {
        public  string? Index { get; set; }
        public  string Type { get; set; } = null!;
        public  string Text { get; set; } = null!;

        [BsonElement("Choices")]
        [JsonPropertyName("Choices")]
        public IList<string> Choices { get; set; } = null!;
        
    }
}

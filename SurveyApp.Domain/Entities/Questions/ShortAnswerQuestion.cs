using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Questions
{
    [BsonDiscriminator("ShortAnswerQuestion")]
    public class ShortAnswerQuestion : Question
    {
        public  string? Index { get; set; }
        public  string Type { get; set; } = null!;
        public  string Text { get; set; } = null!;

    }
}

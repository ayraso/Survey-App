using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Questions
{
    public class RangeQuestion : IQuestion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Type { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string MinRange { get; set; } = "1";
        public string MaxRange { get; set; } = null!;
    }
}

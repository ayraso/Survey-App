using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SurveyApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using SurveyApp.Domain.Entities.Questions;

namespace SurveyApp.Domain.Entities.Surveys
{
    public class Survey : ISurvey, IMongoDbEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string UserIdCreatedBy { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        [BsonElement("Questions")]
        [JsonPropertyName("Questions")]
        public List<Question> Questions { get; set; } = null!;

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SurveyApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SurveyApp.Domain.Entities.Responses
{
    public class Response : IResponse, IMongoDbEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string SurveyId { get; set; } = null!;

        [BsonElement("Answers")]
        [JsonPropertyName("Answers")]
        public IList<IAnswer> Answers { get; set; } = null!;

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

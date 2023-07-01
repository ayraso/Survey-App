using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SurveyApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Users
{
    public class User : IAccount, IMongoDbEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AccountType { get; set; } = null!;

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

using JsonSubTypes;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Questions
{
    [JsonDerivedType(typeof(LongAnswerQuestion))]
    [JsonDerivedType(typeof(ShortAnswerQuestion))]
    [JsonDerivedType(typeof(RangeQuestion))]
    [JsonDerivedType(typeof(MultiChoiceQuestion))]
    [BsonDiscriminator(Required = true, RootClass = true)]
    [BsonKnownTypes(typeof(LongAnswerQuestion), typeof(ShortAnswerQuestion), typeof(RangeQuestion), typeof(MultiChoiceQuestion))]
    public abstract class Question
    {
        public string? Index { get; set; }
        public string Type { get; set; } = null!;
        public string? Text { get; set; } = null!;
    }
}

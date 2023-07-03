using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Questions
{
    [BsonDiscriminator(Required = true, RootClass = true)]
    [BsonKnownTypes(typeof(LongAnswerQuestion), typeof(ShortAnswerQuestion), typeof(RangeQuestion), typeof(MultiChoiceQuestion))]
    public class Question
    {
       
    }
}

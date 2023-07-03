using MongoDB.Bson.Serialization.Attributes;
using SurveyApp.Domain.Entities.SurveyResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Requests.SurveyResponse
{
    public class SurveyResponseCreateRequest
    {
        public string SurveyId { get; set; } = null!;
        public List<Answer> Answers { get; set; } = null!;
    }
}

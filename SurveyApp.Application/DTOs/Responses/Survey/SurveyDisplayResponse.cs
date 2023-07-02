using MongoDB.Bson;
using SurveyApp.Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Responses.Survey
{
    public class SurveyDisplayResponse
    {
        public string? Id { get; set; } = null!;
        public string UserIdCreatedBy { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<Question> Questions { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

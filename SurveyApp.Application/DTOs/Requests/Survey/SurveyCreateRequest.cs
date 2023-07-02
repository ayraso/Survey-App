using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Requests.Survey
{
    public class SurveyCreateRequest
    {
        public string UserIdCreatedBy { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<RangeQuestionCreateRequest>? RangeQuestionCreateRequests { get; set; }
        public List<MultiChoiceQuestionCreateRequest>? MultiChoiceQuestionCreateRequests { get; set; }
        public List<ShortAnswerQuestionCreateRequest>? ShortAnswerQuestionCreateRequests { get; set; }
        public List<LongAnswerQuestionCreateRequest>? LongAnswerQuestionCreateRequests { get; set; }
    }
}

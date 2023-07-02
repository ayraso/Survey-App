using AutoMapper;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.SurveyService
{
    public class QuestionFactory : IQuestionFactory
    {
        private readonly IMapper _mapper;
        public QuestionFactory(IMapper mapper) 
        {
            this._mapper = mapper;
        }
        public IQuestion CreateQuestion(IQuestion createRequest)
        {
            switch (createRequest.Type)
            {
                case "Range":
                    var rangeQuestion = _mapper.Map<RangeQuestion>(createRequest);
                    rangeQuestion.MinRange = ((RangeQuestionCreateRequest)createRequest).MinRange;
                    rangeQuestion.MaxRange = ((RangeQuestionCreateRequest)createRequest).MaxRange;
                    return rangeQuestion;
                case "MultiChoice":
                    var multiChoiceQuestion = _mapper.Map<MultiChoiceQuestion>(createRequest);
                    multiChoiceQuestion.Choices = ((MultiChoiceQuestionCreateRequest)createRequest).Choices;
                    return multiChoiceQuestion;
                case "ShortAnswer":
                    return _mapper.Map<ShortAnswerQuestion>(createRequest);
                case "LongAnswer":
                    return _mapper.Map<LongAnswerQuestion>(createRequest);
                default:
                    throw new ArgumentException("Invalid question type.");
            }
        }
    }
}

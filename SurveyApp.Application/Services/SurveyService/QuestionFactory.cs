using AutoMapper;
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
        public IQuestion CreateQuestion(IQuestion question)
        {
            switch (question.Type)
            {
                case "Range":
                    return _mapper.Map<RangeQuestion>(question);
                case "MultiChoice":
                    return _mapper.Map<MultiChoiceQuestion>(question);
                case "LongAnswer":
                    return _mapper.Map<LongAnswerQuestion>(question);
                case "ShortAnswer":
                    return _mapper.Map<ShortAnswerQuestion>(question);
                default:
                    throw new ArgumentException("Invalid question type.");
            }
        }
    }
}

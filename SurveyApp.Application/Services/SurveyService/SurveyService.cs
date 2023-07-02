using AutoMapper;
using Microsoft.Extensions.Options;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Domain.Entities.Questions;
using SurveyApp.Domain.Entities.Surveys;
using SurveyApp.Domain.Entities.Users;
using SurveyApp.Infrastructure.Data;
using SurveyApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.SurveyService
{
    public class SurveyService : ISurveyService
    {
        private readonly MongoDbRepository<Survey> _surveyRepository;
        //private readonly IQuestionFactory _questionFactory;
        private readonly IMapper _mapper;
        public SurveyService(IOptions<MongoDbSettings> mongoDbSettings,
                            
                            IMapper mapper)
        {
            _surveyRepository = new MongoDbRepository<Survey>(mongoDbSettings);
            //_questionFactory = questionFactory;
            _mapper = mapper;
        }

        public async Task CreateSurvey(SurveyCreateRequest newSurvey)
        {
            var survey = new Survey
            {
                UserIdCreatedBy = newSurvey.UserIdCreatedBy,
                Title = newSurvey.Title,
                Description = newSurvey.Description,
                CreatedAt = newSurvey.CreatedAt,
                Questions = new List<Question>()
            };

            // RangeQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.RangeQuestionCreateRequests != null && newSurvey.RangeQuestionCreateRequests.Count > 0)
            {
                foreach (var rangeQuestionCreateRequest in newSurvey.RangeQuestionCreateRequests)
                {
                    var question = new RangeQuestion(rangeQuestionCreateRequest);
                    question.MinRange = rangeQuestionCreateRequest.MinRange;
                    question.MaxRange = rangeQuestionCreateRequest.MaxRange;
                    survey.Questions.Add(question);
                }
            }

            // MultiChoiceQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.MultiChoiceQuestionCreateRequests != null && newSurvey.MultiChoiceQuestionCreateRequests.Count > 0)
            {
                foreach (var multiChoiceQuestionCreateRequest in newSurvey.MultiChoiceQuestionCreateRequests)
                {
                    var question = new MultiChoiceQuestion(multiChoiceQuestionCreateRequest);
                    question.Choices = multiChoiceQuestionCreateRequest.Choices;
                    survey.Questions.Add(question);
                }
            }

            // ShortAnswerQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.ShortAnswerQuestionCreateRequests != null && newSurvey.ShortAnswerQuestionCreateRequests.Count > 0)
            {
                foreach (var shortAnswerQuestionCreateRequest in newSurvey.ShortAnswerQuestionCreateRequests)
                {
                    var question = new ShortAnswerQuestion(shortAnswerQuestionCreateRequest);
                    survey.Questions.Add(question);
                }
            }

            // LongAnswerQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.LongAnswerQuestionCreateRequests != null && newSurvey.LongAnswerQuestionCreateRequests.Count > 0)
            {
                foreach (var longAnswerQuestionCreateRequest in newSurvey.LongAnswerQuestionCreateRequests)
                {
                    var question = new LongAnswerQuestion(longAnswerQuestionCreateRequest);
                    survey.Questions.Add(question);
                }
            }

            await _surveyRepository.AddAsync(survey);
        }

        public async Task<IEnumerable<Survey?>> GetAllSurveysAsync()
        {
            return await _surveyRepository.GetAllAsync();
        }
    }
}

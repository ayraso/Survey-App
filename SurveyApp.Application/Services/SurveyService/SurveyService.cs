using AutoMapper;
using Microsoft.Extensions.Options;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.DTOs.Responses.Survey;
using SurveyApp.Application.Extensions;
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

        public void CreateSurvey(SurveyCreateRequest surveyCreateRequest)
        {
            throw new NotImplementedException();
        }

        public string CreateSurveyAndReturnId(SurveyCreateRequest surveyCreateRequest)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateSurveyAndReturnIdAsync(SurveyCreateRequest surveyCreateRequest)
        {
            throw new NotImplementedException();
        }

        public async Task CreateSurveyAsync(SurveyCreateRequest newSurvey)
        {
            var survey = new Survey
            {
                UserIdCreatedBy = newSurvey.UserIdCreatedBy,
                Title = newSurvey.Title,
                Description = newSurvey.Description,
                CreatedAt = newSurvey.CreatedAt,
                Questions = new List<IQuestion>()
            };

            // RangeQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.RangeQuestionCreateRequests != null && newSurvey.RangeQuestionCreateRequests.Count > 0)
            {
                foreach (var rangeQuestionCreateRequest in newSurvey.RangeQuestionCreateRequests)
                {
                    var question = new RangeQuestion(rangeQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionId();
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
                    question.Index = survey.GenerateQuestionId();
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
                    question.Index = survey.GenerateQuestionId();
                    survey.Questions.Add(question);
                }
            }

            // LongAnswerQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.LongAnswerQuestionCreateRequests != null && newSurvey.LongAnswerQuestionCreateRequests.Count > 0)
            {
                foreach (var longAnswerQuestionCreateRequest in newSurvey.LongAnswerQuestionCreateRequests)
                {
                    var question = new LongAnswerQuestion(longAnswerQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionId();
                    survey.Questions.Add(question);
                }
            }

            await _surveyRepository.AddAsync(survey);
        }

        public IEnumerable<SurveyDisplayResponse?> GetAllSurveys()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SurveyDisplayResponse?>> GetAllSurveysAsync()
        {
            var surveys = await _surveyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SurveyDisplayResponse>>(surveys);
        }

        public SurveyDisplayResponse? GetSurveyBySurveyId(string surveyId)
        {
            throw new NotImplementedException();
        }

        public async Task<Survey?> GetSurveyBySurveyIdAsync(string surveyId)
        {
            var survey = await _surveyRepository.GetByIdAsync(surveyId);
            return survey;
        }

        public IEnumerable<SurveyDisplayResponse?> GetSurveysByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SurveyDisplayResponse?>> GetSurveysByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}

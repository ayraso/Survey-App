using AutoMapper;
using Microsoft.Extensions.Options;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.Extensions;
using SurveyApp.Domain.Entities.Questions;
using SurveyApp.Domain.Entities.Surveys;
using SurveyApp.Domain.Entities.Users;
using SurveyApp.Infrastructure.Data;
using SurveyApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.SurveyService
{
    public class SurveyService : ISurveyService
    {
        private readonly MongoDbRepository<Survey> _surveyRepository;
        private readonly IMapper _mapper;
        public SurveyService(IOptions<MongoDbSettings> mongoDbSettings,
                            
                            IMapper mapper)
        {
            _surveyRepository = new MongoDbRepository<Survey>(mongoDbSettings);
            _mapper = mapper;
        }

        public void CreateSurvey(SurveyCreateRequest newSurvey)
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
                    var question = new RangeQuestion();
                    question.Type = rangeQuestionCreateRequest.Type;
                    question.Text = rangeQuestionCreateRequest.Text;
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
                    var question = new MultiChoiceQuestion();
                    question.Type = multiChoiceQuestionCreateRequest.Type;
                    question.Text = multiChoiceQuestionCreateRequest.Text;
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
                    var question = new ShortAnswerQuestion();
                    question.Type = shortAnswerQuestionCreateRequest.Type;
                    question.Text = shortAnswerQuestionCreateRequest.Text;
                    question.Index = survey.GenerateQuestionId();
                    survey.Questions.Add(question);
                }
            }

            // LongAnswerQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.LongAnswerQuestionCreateRequests != null && newSurvey.LongAnswerQuestionCreateRequests.Count > 0)
            {
                foreach (var longAnswerQuestionCreateRequest in newSurvey.LongAnswerQuestionCreateRequests)
                {
                    var question = new LongAnswerQuestion();
                    question.Type = longAnswerQuestionCreateRequest.Type;
                    question.Text = longAnswerQuestionCreateRequest.Text;
                    question.Index = survey.GenerateQuestionId();
                    survey.Questions.Add(question);
                }
            }

            _surveyRepository.Add(survey);
        }

        public async Task CreateSurveyAsync(SurveyCreateRequest newSurvey)
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
                    var question = new RangeQuestion();
                    question.Type = rangeQuestionCreateRequest.Type;
                    question.Text = rangeQuestionCreateRequest.Text;
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
                    var question = new MultiChoiceQuestion();
                    question.Type = multiChoiceQuestionCreateRequest.Type;
                    question.Text = multiChoiceQuestionCreateRequest.Text;
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
                    var question = new ShortAnswerQuestion();
                    question.Type = shortAnswerQuestionCreateRequest.Type;
                    question.Text = shortAnswerQuestionCreateRequest.Text;
                    question.Index = survey.GenerateQuestionId();
                    survey.Questions.Add(question);
                }
            }

            // LongAnswerQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.LongAnswerQuestionCreateRequests != null && newSurvey.LongAnswerQuestionCreateRequests.Count > 0)
            {
                foreach (var longAnswerQuestionCreateRequest in newSurvey.LongAnswerQuestionCreateRequests)
                {
                    var question = new LongAnswerQuestion();
                    question.Type = longAnswerQuestionCreateRequest.Type;
                    question.Text = longAnswerQuestionCreateRequest.Text;
                    question.Index = survey.GenerateQuestionId();
                    survey.Questions.Add(question);
                }
            }

            await _surveyRepository.AddAsync(survey);
        }

        public IEnumerable<Survey?> GetAllSurveys()
        {
            var surveys = _surveyRepository.GetAll();
            return surveys;
        }

        public async Task<IEnumerable<Survey?>> GetAllSurveysAsync()
        {
            var surveys = await _surveyRepository.GetAllAsync();
            return surveys;
        }

        public Survey? GetSurveyById(string surveyId)
        {
            var survey = _surveyRepository.GetById(surveyId);
            return survey;
        }

        public async Task<Survey?> GetSurveyByIdAsync(string surveyId)
        {
            var survey = await _surveyRepository.GetByIdAsync(surveyId);
            return survey;
        }

        public IEnumerable<Survey?> GetSurveysByUserId(string userId)
        {
            Expression<Func<Survey, bool>> predicate = survey => survey.UserIdCreatedBy == userId;
            return _surveyRepository.GetAllWithPredicate(predicate);
        }

        public async Task<IEnumerable<Survey?>> GetSurveysByUserIdAsync(string userId)
        {
            Expression<Func<Survey, bool>> predicate = survey => survey.UserIdCreatedBy == userId;
            return await _surveyRepository.GetAllWithPredicateAsync(predicate);
        }

        public async Task<bool> IsSurveyExistsAsync(string surveyId)
        {
            return await _surveyRepository.IsExistsAsync(surveyId);
        }

        public bool IsUserExists(string surveyId)
        {
            return _surveyRepository.IsExists(surveyId);
        }
    }
}

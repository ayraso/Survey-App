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
        //TODO: requestten question oluşturma işleminde factory kullanmaya çalış
        public void CreateSurvey(SurveyCreateRequest newSurvey)
        {
            Survey survey = _mapper.Map<Survey>(newSurvey);

            // RangeQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.RangeQuestionCreateRequests != null && newSurvey.RangeQuestionCreateRequests.Count > 0)
            {
                foreach (var rangeQuestionCreateRequest in newSurvey.RangeQuestionCreateRequests)
                {
                    Question question = _mapper.Map<RangeQuestion>(rangeQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionIndex();
                    survey.Questions.Add(question);
                }
            }

            // MultiChoiceQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.MultiChoiceQuestionCreateRequests != null && newSurvey.MultiChoiceQuestionCreateRequests.Count > 0)
            {
                foreach (var multiChoiceQuestionCreateRequest in newSurvey.MultiChoiceQuestionCreateRequests)
                {
                    Question question = _mapper.Map<MultiChoiceQuestion>(multiChoiceQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionIndex();
                    survey.Questions.Add(question);
                }
            }

            // ShortAnswerQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.ShortAnswerQuestionCreateRequests != null && newSurvey.ShortAnswerQuestionCreateRequests.Count > 0)
            {
                foreach (var shortAnswerQuestionCreateRequest in newSurvey.ShortAnswerQuestionCreateRequests)
                {
                    Question question = _mapper.Map<ShortAnswerQuestion>(shortAnswerQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionIndex();
                    survey.Questions.Add(question);
                }
            }

            // LongAnswerQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.LongAnswerQuestionCreateRequests != null && newSurvey.LongAnswerQuestionCreateRequests.Count > 0)
            {
                foreach (var longAnswerQuestionCreateRequest in newSurvey.LongAnswerQuestionCreateRequests)
                {
                    Question question = _mapper.Map<LongAnswerQuestion>(longAnswerQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionIndex();
                    survey.Questions.Add(question);
                }
            }

            _surveyRepository.Add(survey);
        }

        public async Task CreateSurveyAsync(SurveyCreateRequest newSurvey)
        {
            Survey survey = _mapper.Map<Survey>(newSurvey);

            // RangeQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.RangeQuestionCreateRequests != null && newSurvey.RangeQuestionCreateRequests.Count > 0)
            {
                foreach (var rangeQuestionCreateRequest in newSurvey.RangeQuestionCreateRequests)
                {
                    Question question = _mapper.Map<RangeQuestion>(rangeQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionIndex();
                    survey.Questions.Add(question);
                }
            }

            // MultiChoiceQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.MultiChoiceQuestionCreateRequests != null && newSurvey.MultiChoiceQuestionCreateRequests.Count > 0)
            {
                foreach (var multiChoiceQuestionCreateRequest in newSurvey.MultiChoiceQuestionCreateRequests)
                {
                    Question question = _mapper.Map<MultiChoiceQuestion>(multiChoiceQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionIndex();
                    survey.Questions.Add(question);
                }
            }

            // ShortAnswerQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.ShortAnswerQuestionCreateRequests != null && newSurvey.ShortAnswerQuestionCreateRequests.Count > 0)
            {
                foreach (var shortAnswerQuestionCreateRequest in newSurvey.ShortAnswerQuestionCreateRequests)
                {
                    Question question = _mapper.Map<ShortAnswerQuestion>(shortAnswerQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionIndex();
                    survey.Questions.Add(question);
                }
            }

            // LongAnswerQuestionCreateRequests elemanlarını Questions listesine ekle
            if (newSurvey.LongAnswerQuestionCreateRequests != null && newSurvey.LongAnswerQuestionCreateRequests.Count > 0)
            {
                foreach (var longAnswerQuestionCreateRequest in newSurvey.LongAnswerQuestionCreateRequests)
                {
                    Question question = _mapper.Map<LongAnswerQuestion>(longAnswerQuestionCreateRequest);
                    question.Index = survey.GenerateQuestionIndex();
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

        public bool IsSurveyExists(string surveyId)
        {
            return _surveyRepository.IsExists(surveyId);
        }

        public async Task<bool> CheckSurveyOwnershipAsync(string surveyId, string userId)
        {
            var survey = await _surveyRepository.GetByIdAsync(surveyId);
            if (survey.UserIdCreatedBy == userId) return true;
            else return false;
        }

        public bool CheckSurveyOwnerShip(string surveyId, string userId)
        {
            var survey = _surveyRepository.GetById(surveyId);
            if (survey.UserIdCreatedBy == userId) return true;
            else return false;
        }
    }
}

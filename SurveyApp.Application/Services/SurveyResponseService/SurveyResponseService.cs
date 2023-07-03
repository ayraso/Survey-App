using AutoMapper;
using Microsoft.Extensions.Options;
using SurveyApp.Application.DTOs.Requests.SurveyResponse;
using SurveyApp.Application.DTOs.Responses.SurveyResponse;
using SurveyApp.Domain.Entities.Questions;
using SurveyApp.Domain.Entities.SurveyResponses;
using SurveyApp.Domain.Entities.Surveys;
using SurveyApp.Infrastructure.Data;
using SurveyApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.SurveyResponseService
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly MongoDbRepository<SurveyResponse> _surveyResponseRepository;
        private readonly MongoDbRepository<Survey> _surveyRepository;
        private readonly IMapper _mapper;
        public SurveyResponseService(IOptions<MongoDbSettings> mongoDbSettings,
                               IMapper mapper)
        {
            _surveyResponseRepository = new MongoDbRepository<SurveyResponse>(mongoDbSettings);
            _surveyRepository = new MongoDbRepository<Survey>(mongoDbSettings);
            _mapper = mapper;
        }

        public async Task<SurveyAnalysisResponse> AnalyzeSurveyAsync(string surveyId)
        {
            throw new NotImplementedException();
            //var analysis = new SurveyAnalysisResponse
            //{
            //    SurveyId = surveyId
            //};

            //Survey? survey = _surveyRepository.GetById(surveyId);

            //// bir ankete verilmiş olan tüm response ları çektim
            //var surveyResponses = await _surveyResponseRepository.GetAllWithPredicateAsync(r => r.SurveyId == surveyId);
            //// ankette kaç soru olduğunu buldum
            //int numOfQuestions = survey.Questions.Count();

            //// ankette bulunan her bir soru için
            //for (int i=1; i<= numOfQuestions; i++)
            //{
            //    var questionIndex = i.ToString();

            //    // ilgili soru için verilmiş tüm cevapları topladım
            //    var questionAnswers = new List<Answer>();
            //    foreach (var response in surveyResponses)
            //    {
            //        var answers = response.Answers.Where(answer => answer.QuestionIndex == questionIndex);
            //        questionAnswers.AddRange(answers);
            //    }

            //    // ilgili sorunun tipini buldum
            //    var question = (Question)survey.Questions.Where(q => q.Index == questionIndex);
            //    string questionType = question.Type;

            //    if(questionType == "LongAnswer")
            //    {
            //        // ilgili soru için verilmiş tüm cevapları analiz nesnesi içindeki Answers a at
            //        LongAnswerQuestionAnalyzeResponse questionAnalyze = new LongAnswerQuestionAnalyzeResponse()
            //        {
            //            SurveyId = surveyId,
            //            Index = questionIndex,
            //            Answers = questionAnswers
            //        }
            //    }



            //    if (questionType == "LongAnswer" || questionType == "ShortAnswer")
            //    {
            //        var answers = questionAnswers
            //            .Select(answer => answer.Value.ToString())
            //            .ToList();

            //        //var questionAnalyze = questionType == "LongAnswer"
            //        //    ? new LongAnswerQuestionAnalyzeResponse
            //        //    {
            //        //        SurveyId = surveyId,
            //        //        Index = questionIndex,
            //        //        Answers = answers
            //        //    }
            //        //    : new ShortAnswerQuestionAnalyzeResponse
            //        //    {
            //        //        SurveyId = surveyId,
            //        //        Index = questionIndex,
            //        //        Answers = answers
            //        //    };

            //        ((List<IQuestionAnalyze>)analysisResponse.QuestionAnalyzes).Add(questionAnalyze);
            //    }
            //    else if (questionType == "Range" || questionType == "MultiChoice")
            //    {
            //        var groupedAnswers = questionAnswers
            //            .GroupBy(answer => answer.Value.ToString())
            //            .ToList();

            //        var questionAnalyze = questionType == "Range"
            //            ? new RangeQuestionAnalyzeResponse
            //            {
            //                SurveyId = surveyId,
            //                Index = questionIndex,
            //                NumOfVotes = groupedAnswers.Count.ToString(),
            //                RateOfVote = ((double)groupedAnswers.Count / surveyResponses.Count).ToString("P")
            //            }
            //            : new MultiChoiceQuestionAnalyzeResponse
            //            {
            //                SurveyId = surveyId,
            //                Index = questionIndex,
            //                NumOfVotes = groupedAnswers.Sum(group => group.Count()).ToString(),
            //                RateOfVote = ((double)groupedAnswers.Sum(group => group.Count()) / surveyResponses.Count).ToString("P")
            //            };

            //        ((List<IQuestionAnalyze>)analysisResponse.QuestionAnalyzes).Add(questionAnalyze);
            //    }
            //}
        }

        public async Task CreateSurveyResponseAsync(SurveyResponseCreateRequest surveyResponseCreateRequest)
        {
            var newSurveyResponse = _mapper.Map<SurveyResponse>(surveyResponseCreateRequest);
            await _surveyResponseRepository.AddAsync(newSurveyResponse);
        }

        public async Task<IEnumerable<SurveyResponse?>> GetSurveyResponsesBySurveyIdAsync(string surveyId)
        {
            Expression<Func<SurveyResponse, bool>> predicate = surveyResponse => surveyResponse.SurveyId == surveyId;
            return await _surveyResponseRepository.GetAllWithPredicateAsync(predicate);
        }
    }
}

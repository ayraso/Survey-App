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

        public List<AnswerAnalyze> AnalyzeAnswers(List<string> questionAnswers)
        {
            var answerCounts = questionAnswers
                .GroupBy(x => x)
                .Select(group => new
                {
                    Answer = group.Key,
                    Count = group.Count()
                })
                .ToList();

            var totalVotes = questionAnswers.Count;

            var answerAnalysis = new List<AnswerAnalyze>();

            foreach (var answerCount in answerCounts)
            {
                var rateOfVote = ((double)answerCount.Count / totalVotes) * 100;

                var analysis = new AnswerAnalyze
                {
                    Answer = answerCount.Answer,
                    NumOfVotes = answerCount.Count.ToString(),
                    RateOfVote = $"{rateOfVote:F2}%"
                };

                answerAnalysis.Add(analysis);
            }

            return answerAnalysis;
        }

        public async Task<SurveyAnalysisResponse> AnalyzeSurveyAsync(string surveyId)
        {
            //throw new NotImplementedException();
            var analyzeResponse = new SurveyAnalysisResponse
            {
                SurveyId = surveyId
            };

            Survey? survey = _surveyRepository.GetById(surveyId);

            // bir ankete verilmiş olan tüm response ları çektim
            var surveyResponses = await _surveyResponseRepository.GetAllWithPredicateAsync(r => r.SurveyId == surveyId);
            // ankette kaç soru olduğunu buldum
            int numOfQuestions = survey.Questions.Count();

            // ankette bulunan her bir soru için
            for (int i = 1; i <= numOfQuestions; i++)
            {
                var questionIndex = i.ToString();

                // ilgili soru için verilmiş tüm cevapları topladım
                var questionAnswers = new List<string>();
                foreach (var response in surveyResponses)
                {
                    var answer = response.Answers.Where(answer => answer.QuestionIndex == questionIndex).SingleOrDefault();
                    questionAnswers.Add(answer.AnswerText);
                }

                // ilgili sorunun tipini buldum
                Question question = survey.Questions.SingleOrDefault(q => q.Index == questionIndex);
                string questionType = question.Type;

                if (questionType == "LongAnswer")
                {
                    // ilgili soru için verilmiş tüm cevapları analiz nesnesi içindeki Answers a at
                    LongAnswerQuestionAnalyzeResponse questionAnalyze = new LongAnswerQuestionAnalyzeResponse()
                    {
                        SurveyId = surveyId,
                        Index = questionIndex,
                        Answers = questionAnswers
                    };
                    analyzeResponse.QuestionAnalyzes.Append(questionAnalyze);
                }
                else if(questionType == "ShortAnswer")
                {
                    // ilgili soru için verilmiş tüm cevapları analiz nesnesi içindeki Answers a at
                    ShortAnswerQuestionAnalyzeResponse questionAnalyze = new ShortAnswerQuestionAnalyzeResponse()
                    {
                        SurveyId = surveyId,
                        Index = questionIndex,
                        Answers = questionAnswers
                    };
                    analyzeResponse.QuestionAnalyzes.Append(questionAnalyze);
                }
                else if(questionType == "Range")
                {
                    RangeQuestionAnalyzeResponse questionAnalyze = new RangeQuestionAnalyzeResponse()
                    {
                        SurveyId = surveyId,
                        Index = questionIndex,
                        AnswerAnalyzes = this.AnalyzeAnswers(questionAnswers)
                    };
                    analyzeResponse.QuestionAnalyzes.Append(questionAnalyze);
                }
                else if(questionType == "MultiChoice")
                {
                    MultiChoiceQuestionAnalyzeResponse questionAnalyze = new MultiChoiceQuestionAnalyzeResponse()
                    {
                        SurveyId = surveyId,
                        Index = questionIndex,
                        AnswerAnalyzes = this.AnalyzeAnswers(questionAnswers)
                    };
                    analyzeResponse.QuestionAnalyzes.Append(questionAnalyze);
                }
            }
            analyzeResponse.TotalResponses = surveyResponses.Count();

            return analyzeResponse;
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

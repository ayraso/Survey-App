using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SurveyApp.Application.DTOs.Requests.SurveyResponse;
using SurveyApp.Application.DTOs.Responses.SurveyResponse;
using SurveyApp.Application.Services.SurveyAnalyzer;
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
        private readonly ISurveyAnalyzer? _surveyAnalyzer;
        public SurveyResponseService(IOptions<MongoDbSettings> mongoDbSettings,
                                     IMapper mapper,
                                     IServiceProvider serviceProvider)
        {
            _surveyResponseRepository = new MongoDbRepository<SurveyResponse>(mongoDbSettings);
            _surveyRepository = new MongoDbRepository<Survey>(mongoDbSettings);
            _mapper = mapper;
            this._surveyAnalyzer = serviceProvider.GetService<ISurveyAnalyzer>();

        }
        
        public async Task CreateSurveyResponseAsync(SurveyResponseCreateRequest surveyResponseCreateRequest)
        {
            var newSurveyResponse = _mapper.Map<SurveyResponse>(surveyResponseCreateRequest);
            await _surveyResponseRepository.AddAsync(newSurveyResponse);
        }
        public async Task<SurveyAnalysisResponse> GetSurveyAnalysisBySurveyIdAsync(string surveyId)
        {
            SurveyAnalysisResponse analyze = await this._surveyAnalyzer.AnalyzeSurveyAsync(surveyId);
            return analyze;
        }

        public async Task<IEnumerable<SurveyResponse?>> GetSurveyResponsesBySurveyIdAsync(string surveyId)
        {
            Expression<Func<SurveyResponse, bool>> predicate = surveyResponse => surveyResponse.SurveyId == surveyId;
            return await _surveyResponseRepository.GetAllWithPredicateAsync(predicate);
        }
    }
}

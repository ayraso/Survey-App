using AutoMapper;
using Microsoft.Extensions.Options;
using SurveyApp.Application.DTOs.Requests.SurveyResponse;
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
        private readonly IMapper _mapper;
        public SurveyResponseService(IOptions<MongoDbSettings> mongoDbSettings,
                               IMapper mapper)
        {
            _surveyResponseRepository = new MongoDbRepository<SurveyResponse>(mongoDbSettings);
            _mapper = mapper;
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

﻿using AutoMapper;
using Microsoft.Extensions.Options;
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
        private readonly IMapper _mapper;
        public SurveyService(IOptions<MongoDbSettings> mongoDbSettings, IMapper mapper)
        {
            _surveyRepository = new MongoDbRepository<Survey>(mongoDbSettings);
            _mapper = mapper;
        }

        public IQuestion CreateQuestion(IQuestion question)
        {
            switch (question.Type)
            {
                case "Range":
                    return new RangeQuestion(question);
                case "MultiChoice":
                    return new MultiChoiceQuestion(question);
                case "LongAnswer":
                    return new LongAnswerQuestion(question);
                case "ShortAnswer":
                    return new ShortAnswerQuestion(question);
                default:
                    throw new ArgumentException("Invalid question type.");
            }
        }

        public async Task CreateSurvey(Survey survey)
        {
            await _surveyRepository.AddAsync(survey);
        }

        public async Task<IEnumerable<Survey?>> GetAllSurveysAsync()
        {
            return await _surveyRepository.GetAllAsync();
        }
    }
}

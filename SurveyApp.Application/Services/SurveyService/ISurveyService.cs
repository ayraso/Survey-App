﻿using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.DTOs.Responses.User;
using SurveyApp.Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.SurveyService
{
    public interface ISurveyService
    {
        Task<string> CreateSurveyAsync(SurveyCreateRequest surveyCreeateRequest);
        string CreateSurvey(SurveyCreateRequest surveyCreeateRequest);
        Task<IEnumerable<Survey?>> GetAllSurveysAsync();
        IEnumerable<Survey?> GetAllSurveys();
        Task<Survey?> GetSurveyByIdAsync(string surveyId);
        Survey? GetSurveyById(string surveyId);
        Task<IEnumerable<Survey?>> GetSurveysByUserIdAsync(string userId);
        IEnumerable<Survey?> GetSurveysByUserId(string userId);
        Task<bool> IsSurveyExistsAsync(string surveyId);
        bool IsSurveyExists(string surveyId);
        Task<bool> CheckSurveyOwnershipAsync(string surveyId, string userId);
        bool CheckSurveyOwnerShip(string surveyId, string userId);

    }
}

﻿using SurveyApp.Application.DTOs.Responses.SurveyResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.SurveyAnalyzer
{
    public interface ISurveyAnalyzer
    {
        Task<SurveyAnalysisResponse> AnalyzeSurveyAsync(string surveyId);
        List<AnswerAnalyze> AnalyzeAnswers(List<string> questionAnswers);
    }
}

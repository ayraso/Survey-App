using SurveyApp.Domain.Entities.Questions;
using SurveyApp.Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Extensions
{
    public static class SurveyExtensions
    {
        public static string GenerateQuestionIndex(this Survey survey)
        {
            int newId_int = survey.Questions.Count + 1;
            return newId_int.ToString();
        }
    }
}

using SurveyApp.Application.Services.Common;
using SurveyApp.Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.SurveyService
{
    public interface IQuestionFactory : IFactory<IQuestion>
    {
        IQuestion CreateQuestion(IQuestion question);
    }
}

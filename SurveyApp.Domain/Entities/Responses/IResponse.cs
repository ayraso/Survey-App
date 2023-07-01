using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Responses
{
    public interface IResponse
    {
        public string SurveyId { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Filters.SurveyExistence
{
    public class SurveyExistenceAttribute : TypeFilterAttribute
    {
        public SurveyExistenceAttribute() : base(typeof(SurveyExistenceFilter))
        {

        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Filters
{
    public class SurveyExistenceAttribute : TypeFilterAttribute
    {
        public SurveyExistenceAttribute() : base(typeof(SurveyExistenceFilter))
        {

        }
    }
}

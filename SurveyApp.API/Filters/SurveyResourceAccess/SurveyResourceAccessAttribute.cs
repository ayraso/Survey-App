using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Filters.SurveyResourceAccess
{
    public class SurveyResourceAccessAttribute : TypeFilterAttribute
    {
        public SurveyResourceAccessAttribute() : base(typeof(SurveyResourceAccessFilter))
        {

        }
    }
}
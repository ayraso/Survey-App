using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Filters
{
    public class SurveyResourceAccessAttribute : TypeFilterAttribute
    {
        public SurveyResourceAccessAttribute() : base(typeof(SurveyResourceAccessFilter))
        {

        }
    }
}
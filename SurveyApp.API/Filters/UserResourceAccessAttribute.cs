using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Filters
{
    public class UserResourceAccessAttribute : TypeFilterAttribute
    {
        public UserResourceAccessAttribute() : base(typeof(UserResourceAccessFilter))
        {

        }
    }
}

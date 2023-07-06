using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Filters.UserResourceAccess
{
    public class UserResourceAccessAttribute : TypeFilterAttribute
    {
        public UserResourceAccessAttribute() : base(typeof(UserResourceAccessFilter))
        {

        }
    }
}

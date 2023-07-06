using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Filters.UserExistence
{
    public class UserExistenceAttribute : TypeFilterAttribute
    {
        public UserExistenceAttribute() : base(typeof(UserExistenceFilter))
        {

        }
    }
}

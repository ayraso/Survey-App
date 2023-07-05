using Microsoft.AspNetCore.Mvc;

namespace SurveyApp.API.Filters
{
    public class UserExistenceAttribute : TypeFilterAttribute
    {
        public UserExistenceAttribute()  : base(typeof(UserExistenceFilter))
        {

        }
    }
}

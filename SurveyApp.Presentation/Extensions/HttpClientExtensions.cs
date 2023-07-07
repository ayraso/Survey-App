namespace SurveyApp.Presentation.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddJwtTokenToRequestHeader(this HttpClient httpClient, IHttpContextAccessor httpContextAccessor) 
        {
            var token = httpContextAccessor.HttpContext.Session.GetString("Token");
            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            }
        }
    }
}

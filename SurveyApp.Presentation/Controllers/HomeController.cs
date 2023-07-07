using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SurveyApp.Presentation.Models;
using SurveyApp.Presentation.Models.SurveyRelatedModels;
using SurveyApp.Presentation.Models.SurveyResponseRelatedModels;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SurveyApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult CreateSurvey()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> ViewSurveys()
        {
            var httpClient = new HttpClient();
            try
            {
                var token = HttpContext.Session.GetString("Token");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var userId = HttpContext.Session.GetString("UserId");
                var response = await httpClient.GetAsync($"https://localhost:7248/Surveys/UserId:{userId}/All");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var surveys = JsonConvert.DeserializeObject<List<Survey>>(apiResponse);
                    ViewSurveysModel viewSurveysModel = new ViewSurveysModel();
                    viewSurveysModel.Surveys = surveys;
                    return View(viewSurveysModel);
                }
                else
                {
                    return View();
                }
            }
            finally
            {
                httpClient.Dispose();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> SurveyDetails(string id)
        {
            var httpClient = new HttpClient();
            try
            {
                var token = HttpContext.Session.GetString("Token");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var response = await httpClient.GetAsync($"https://localhost:7248/Surveys/{id}/Analyzes");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    var surveyDetails = JsonConvert.DeserializeObject<SurveyAnalysis>(apiResponse);
                    return View();
                }
                else
                {
                    // Hata durumu işleme
                }
            }
            finally
            {
                httpClient.Dispose();
            }

            // Hata durumu için uygun ActionResult dönüşü
            return View();
        }
    }
}

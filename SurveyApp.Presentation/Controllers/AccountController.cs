using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SurveyApp.Presentation.Models;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SurveyApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLoginRequest)
        {
            using(var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(userLoginRequest), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7248/Login", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var userLoginResponse = JsonConvert.DeserializeObject<LoggedInUser>(apiResponse);

                        HttpContext.Session.SetString("UserId", userLoginResponse.Id);
                        HttpContext.Session.SetString("Token", userLoginResponse.Token);

                        return RedirectToAction("Index", "Home");

                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }


                }
            }
        }


    }
}

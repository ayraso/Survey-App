using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.API.Filters;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Domain.Entities.Users;

namespace SurveyApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly string? key;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            this._userService = userService;
            this._configuration = configuration;
            this.key = _configuration.GetSection("JwtKey").ToString();
        }

        [Authorize(Roles="Admin")]
        [HttpGet("/User/All")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("Get")]
        [UserExistence]
        //TODO: 24 digit hex string mi kontrolü ekle UserExistance attribute'una
        // parametredeki userId ile claim deki aynı mı ? 
        public async Task<IActionResult> GetUser(string userId)
        {
            //if(userId == null) { return BadRequest(); }
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("/User/SignUp")]
        public async Task<IActionResult> CreateNewUser(UserCreateRequest userCreateRequest)
        {
            if(ModelState.IsValid)
            {
                bool isEmailReceived = await _userService.IsEmailRegisteredAsync(userCreateRequest.Email);
                if (!isEmailReceived)
                {
                    await _userService.CreateUserAsync(userCreateRequest);
                    return Ok();
                }
                return Conflict(new { message = $"Bu e-posta hesabına ait bir hesap zaten bulunmaktadır." });
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("/User/Login")]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            if (ModelState.IsValid)
            {
                var token = await _userService.AuthenticateAsync(userLoginRequest, key);
                if (token == null) 
                    return Unauthorized();
                //TODO: userLoginResquest döndürmek yerine ne döndürülebilir?
                return Ok(new {token, userLoginRequest });
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("/Account/{userId}/Info")]
        [UserExistence]
        public async Task<IActionResult> GetUserAccountInfo(string userId)
        {  
            var user = await _userService.GetUserAccountInfoAsync(userId);
            return Ok(user);  
        }

        [Authorize(Roles = "User")]
        [HttpPut("/Account/{userId}/UpdatePassword")]
        [UserExistence]
        public async Task<IActionResult> UpdateUserPassword(UserUpdatePasswordRequest userUpdatePasswordRequest)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserPasswordAsync(userUpdatePasswordRequest);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "User")]
        [HttpPut("/Account/{userId}/UpdateEmail")]
        public async Task<IActionResult> UpdateEmail(UserUpdateEmailRequest userUpdateEmailRequest)
        {
            if(ModelState.IsValid)
            {
                bool isEmailReceived = await _userService.IsEmailRegisteredAsync(userUpdateEmailRequest.Email);
                if (!isEmailReceived)
                {
                    var userDisplayResponse = await _userService.UpdateUserEmailAsync(userUpdateEmailRequest);
                    return Ok(userDisplayResponse);
                }
                return Conflict(new { message = $"Bu e-posta hesabına ait bir hesap zaten bulunmaktadır." });
            }
            return BadRequest(ModelState);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("/Account/{userId}/Delete")]
        [UserExistence]
        public async Task<IActionResult> DeleteUserAccount(string userId)
        {
            await _userService.DeleteUserAccountAsync(userId);
            return Ok();
        }

    }
}

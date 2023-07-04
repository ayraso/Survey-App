using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("/User/All")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("/User/{userId}")] 
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpPost("/User/SignIn")]
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
                var token = _userService.Authenticate(userLoginRequest, key);
                if (token == null) 
                    return Unauthorized();
                //TODO: userLoginResquest döndürmek yerine ne döndürülebilir?
                return Ok(new {token, userLoginRequest });
            }
            return BadRequest();
        }

        [HttpGet("/Account/{userId}/Info")]
        public async Task<IActionResult> GetUserAccountInfo(string userId)
        {
            if (userId != null)
            {
                bool isUserExists = await _userService.IsUserExistsAsync(userId);
                if (isUserExists)
                {
                    var user = await _userService.GetUserByIdAsync(userId);
                    return Ok(user);
                }
                return NotFound(new {message = $"Böyle bir kullanıcı bulunamadı."});
            }
            return BadRequest();
        }

        [HttpPut("/Account/{userId}/UpdatePassword")]
        public async Task<IActionResult> UpdateUserPassword(UserUpdatePasswordRequest userUpdatePasswordRequest)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateUserPasswordAsync(userUpdatePasswordRequest);
                return Ok();
            }
            return BadRequest(ModelState);
        }

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


        [HttpDelete("/Account/{userId}/Delete")] 
        public async Task<IActionResult> DeleteUserAccount(string userId)
        {
            if(userId != null)
            {
                await _userService.DeleteUserAccountAsync(userId);
                return Ok();    
            }
            return BadRequest();
        }

    }
}

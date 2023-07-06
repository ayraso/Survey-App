using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.API.Filters.UserExistence;
using SurveyApp.API.Filters.UserResourceAccess;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.Services.UserService;

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
        [HttpGet]
        [Route("/Users/All")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("/Users/Id:{userId}")]
        [UserExistence]
        //TODO: 24 digit hex string mi kontrolü ekle UserExistance attribute'una
        //TODO: ModelSatate ve null check'leri filter'a ekle
        [UserResourceAccess]
        public async Task<IActionResult> GetUser(string userId)
        {
            if(userId == null) { return BadRequest(); }
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/SignUp")]
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
        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            if (ModelState.IsValid)
            {
                var userLoginResponse = await _userService.AuthenticateAsync(userLoginRequest, key);
                if (userLoginResponse == null) 
                    return Unauthorized();
                return Ok(new { userLoginResponse });
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [Route("/Users/{userId}/Account/Info")]
        [UserExistence]
        public async Task<IActionResult> GetUserAccountInfo(string userId)
        {  
            if (userId == null) return BadRequest();
            var user = await _userService.GetUserAccountInfoAsync(userId);
            return Ok(user);  
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        [Route("/Users/{userId}/Account/UpdatePassword")]
        [UserExistence]
        [UserResourceAccess]
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
        [HttpPut]
        [Route("/Users/{userId}/Account/UpdateEmail")]
        [UserExistence]
        [UserResourceAccess]
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
        [HttpDelete]
        [Route("/Users/{userId}/Account/Delete")]
        [UserExistence]
        [UserResourceAccess]
        public async Task<IActionResult> DeleteUserAccount(string userId)
        {
            if (userId == null) return BadRequest();
            await _userService.DeleteUserAccountAsync(userId);
            return Ok();
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Domain.Entities.Users;

namespace SurveyApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
           this._userService = userService;
        }

        [HttpGet("Users/All")]
        public async Task<IEnumerable<User?>> GetUsersAsync()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpGet("Users/{userId}")] 
        public async Task<User?> GetUser(string userId)
        {
            return await _userService.GetUserByIdAsync(userId);
        }

        [HttpPost("/SignIn")]
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

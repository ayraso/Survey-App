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

        [HttpGet]
        public async Task<IEnumerable<User?>> GetUsersAsync()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpPost]
        public async Task CreateNewUserAsync(UserCreateRequest userCreateRequest)
        {
            await _userService.CreateUserAsync(userCreateRequest);
        }

        [HttpPut("{id}/Account")]
        public async Task UpdateUserPasswordAsync(UserUpdatePasswordRequest userUpdatePasswordRequest)
        {
            await _userService.UpdateUserPasswordAsync(userUpdatePasswordRequest);
        }

        [HttpDelete("{id}/Account")] 
        public async Task DeleteUserAccount(string id)
        {
            await _userService.DeleteUserAccountAsync(id);
        }
    }
}

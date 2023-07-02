using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.DTOs.Responses.User;
using SurveyApp.Domain.Entities.Users;
using SurveyApp.Infrastructure.Data;
using SurveyApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly MongoDbRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public UserService(IOptions<MongoDbSettings> mongoDbSettings, IMapper mapper)
        {
            _userRepository = new MongoDbRepository<User>(mongoDbSettings);
            _mapper = mapper;
        }
        public async Task<List<User>> GetUsersAsync() 
        {
            return (List<User>)await _userRepository.GetAllAsync();
        }
        public async Task CreateUserAsync(UserCreateRequest userCreateRequest)
        {
            var newUser = _mapper.Map<User>(userCreateRequest);
            await _userRepository.AddAsync(newUser);
        }
        public void CreateUser(UserCreateRequest userCreateRequest)
        {
            var newUser = _mapper.Map<User>(userCreateRequest);
            _userRepository.Add(newUser);
        }
        public string CreateUserAndReturnId(UserCreateRequest userCreateRequest)
        {
            var newUser = _mapper.Map<User>(userCreateRequest);
            _userRepository.Add(newUser);
            return newUser.Id;
        }

        public async Task<string> CreateUserAndReturnIdAsync(UserCreateRequest userCreateRequest)
        {
            var newUser = _mapper.Map<User>(userCreateRequest);
            await _userRepository.AddAsync(newUser);
            return newUser.Id;
        }

        public bool IsEmailRegistered(string email)
        {
            Expression<Func<User, bool>> predicate = u => u.Email == email;
            var users = _userRepository.GetAllWithPredicate(predicate);
            return users.Any();
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            Expression<Func<User, bool>> predicate = u => u.Email == email;
            var users = await _userRepository.GetAllWithPredicateAsync(predicate);
            return users.Any();
        }

        public UserDisplayResponse UpdateUserEmail(UserUpdateEmailRequest userUpdateEmailRequest)
        {
            _userRepository.UpdateField(userUpdateEmailRequest.Id, "Email", userUpdateEmailRequest.Email);
            var updatedUser = _userRepository.GetById(userUpdateEmailRequest.Id);
            var response = _mapper.Map<UserDisplayResponse>(updatedUser);
            return response;
        }

        public async Task<UserDisplayResponse> UpdateUserEmailAsync(UserUpdateEmailRequest userUpdateEmailRequest)
        {
            await _userRepository.UpdateFieldAsync(userUpdateEmailRequest.Id, "Email", userUpdateEmailRequest.Email);
            var updatedUser = await _userRepository.GetByIdAsync(userUpdateEmailRequest.Id);
            var response = _mapper.Map<UserDisplayResponse>(updatedUser);
            return response;
        }

        public void UpdateUserPassword(UserUpdatePasswordRequest userUpdatePasswordRequest)
        {
            _userRepository.UpdateField(userUpdatePasswordRequest.Id, "Password", userUpdatePasswordRequest.Password);
        }

        public async Task UpdateUserPasswordAsync(UserUpdatePasswordRequest userUpdatePasswordRequest)
        {
            await _userRepository.UpdateFieldAsync(userUpdatePasswordRequest.Id, "Password", userUpdatePasswordRequest.Password);
        }

        public User ValidateUser(UserLoginRequest userLoginRequest)
        {
            Expression<Func<User, bool>> predicate = u => u.Email == userLoginRequest.Email &&
                                                     u.Password == userLoginRequest.Password;
            var user = _userRepository.GetAllWithPredicate(predicate).SingleOrDefault();
            return user;
        }

        public async Task<User?> ValidateUserAsync(UserLoginRequest userLoginRequest)
        {
            Expression<Func<User, bool>> predicate = u => u.Email == userLoginRequest.Email &&
                                                     u.Password == userLoginRequest.Password;
            var users = await _userRepository.GetAllWithPredicateAsync(predicate);
            var user = users.SingleOrDefault();
            return user;
        }

        public async Task<IEnumerable<User?>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public IEnumerable<User?> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public async Task DeleteUserAccountAsync(string UserId)
        {
            await _userRepository.DeleteByIdAsync(UserId);
        }

        public void DeleteUserAccount(string UserId)
        {
            _userRepository.DeleteById(UserId);
        }

        public async Task<User?> GetUserByIdAsync(string UserId)
        {
            return await _userRepository.GetByIdAsync(UserId);
        }

        public User? GetUserById(string UserId)
        {
            return _userRepository.GetById(UserId);
        }

        public async Task<bool> IsUserExistsAsync(string UserId)
        {
            return await _userRepository.IsExistsAsync(UserId);
        }

        public bool IsUserExists(string UserId)
        {
            return _userRepository.IsExists(UserId);    
        }
    }
}

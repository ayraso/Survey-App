﻿using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.DTOs.Responses.User;
using SurveyApp.Domain.Entities.Users;
using SurveyApp.Infrastructure.Data;
using SurveyApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<UserLoginResponse> AuthenticateAsync(UserLoginRequest loginRequest, string key)
        {
            var user = await this.ValidateUserAsync(loginRequest);
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            UserLoginResponse userLoginResponse = new UserLoginResponse(user.Id, tokenHandler.WriteToken(token));
            return userLoginResponse;
        }

        public async Task<UserDisplayResponse> GetUserAccountInfoAsync(string userId)
        {
            var user = await this.GetUserByIdAsync(userId);
            return _mapper.Map<UserDisplayResponse>(user);
        }

        public UserDisplayResponse GetUserAccountInfo(string userId)
        {
            var user = this.GetUserById(userId);
            return _mapper.Map<UserDisplayResponse>(user);
        }
    }
}

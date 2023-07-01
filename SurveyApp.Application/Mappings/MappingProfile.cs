using AutoMapper;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.DTOs.Responses.User;
using SurveyApp.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // User Mappings
            CreateMap<User, UserDisplayResponse>();
            CreateMap<UserCreateRequest, User>();
            // gerekli mi bunlar bilmiyorum.
            CreateMap<UserUpdateEmailRequest, User>();
            CreateMap<UserUpdatePasswordRequest, User>();
        }
    }
}

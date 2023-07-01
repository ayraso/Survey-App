using AutoMapper;
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
        }
    }
}

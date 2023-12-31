﻿using AutoMapper;
using SurveyApp.Application.DTOs.Requests.Survey;
using SurveyApp.Application.DTOs.Requests.SurveyResponse;
using SurveyApp.Application.DTOs.Requests.User;
using SurveyApp.Application.DTOs.Responses.User;
using SurveyApp.Domain.Entities.Questions;
using SurveyApp.Domain.Entities.SurveyResponses;
using SurveyApp.Domain.Entities.Surveys;
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

            
            // Question Mappings
            CreateMap<RangeQuestionCreateRequest, RangeQuestion>();
            CreateMap<MultiChoiceQuestionCreateRequest, MultiChoiceQuestion>();
            CreateMap<ShortAnswerQuestionCreateRequest, ShortAnswerQuestion>();
            CreateMap<LongAnswerQuestionCreateRequest, LongAnswerQuestion>();

            CreateMap<Question, RangeQuestion>();
            CreateMap<Question, MultiChoiceQuestion>();
            CreateMap<Question, ShortAnswerQuestion>();
            CreateMap<Question, LongAnswerQuestion>();


            // Survey Mappings
            CreateMap<SurveyCreateRequest, Survey>();

            // SurveyResponse Mappings
            CreateMap<SurveyResponseCreateRequest, SurveyResponse>();
        }
    }
}

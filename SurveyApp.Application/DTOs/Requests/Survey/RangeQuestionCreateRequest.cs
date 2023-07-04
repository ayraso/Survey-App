﻿using SurveyApp.Domain.Entities.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.DTOs.Requests.Survey
{
    //TODO: questionCreateRequest base i yaratıp bunların yaratım işlemini otomatize edebilir miyiz diye düşün
    public class RangeQuestionCreateRequest
    {
        public string Type { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string MinRange { get; set; } = "1";
        public string MaxRange { get; set; } = null!;
    }
}

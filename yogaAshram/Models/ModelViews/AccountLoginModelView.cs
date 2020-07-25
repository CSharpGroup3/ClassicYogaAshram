﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace yogaAshram.Models.ModelViews
{
    public class AccountLoginModelView
    {
        [Remote(action: "CheckAuthValid", controller: "Validation", ErrorMessage = "Такой почты или логина не существует")]
        [Required(ErrorMessage = "Введите логин")]
        public string Authentificator { get; set; }
        public string Password { get; set; }
    }
}

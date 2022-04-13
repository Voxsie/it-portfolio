﻿using System.ComponentModel.DataAnnotations;

namespace Portfolio.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Email")]
    public string UserName { get; set; }
         
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
         
    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }
         
    public string ReturnUrl { get; set; }
}
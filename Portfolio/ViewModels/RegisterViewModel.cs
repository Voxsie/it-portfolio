﻿using System.ComponentModel.DataAnnotations;

namespace Portfolio.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
 
    [Required]
    [Display(Name = "Пользователь")]
    public string UserName { get; set; }

    /*[Required]
    [Display(Name = "Год Рождения")]
    public int BirthYear { get; set; }*/
    
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
 
    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль")]
    public string PasswordConfirm { get; set; }
}
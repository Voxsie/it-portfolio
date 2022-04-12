using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.ViewModels;
using Portfolio.Entity;

namespace Portfolio.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    //register
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = new User {Email = model.Email, UserName = model.UserName};
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation($"new user: {0} password: {1}", model.UserName, model.Password);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }
    
    //login
    
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel loginModel)
    {
        if (ModelState.IsValid)
        {
            var result = 
                await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User \"{User}\" logged in | IP: {Ip}", loginModel.Email,
                    Request.HttpContext.Connection.RemoteIpAddress);
                if (!string.IsNullOrEmpty(loginModel.ReturnUrl) && Url.IsLocalUrl(loginModel.ReturnUrl))
                    return Redirect(loginModel.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }
            _logger.LogError("wrong password or email");
            ModelState.AddModelError(string.Empty, "Неправильный логин и (или) пароль");
        }
        return View(loginModel);
    }
 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("user {User} is signed out", User.Identity.Name);
        return RedirectToAction("Index", "Home");
    }
}
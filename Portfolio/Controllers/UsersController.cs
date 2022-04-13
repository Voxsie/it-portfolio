using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Entity;
using Portfolio.ViewModels;

namespace Portfolio.Controllers;

public class UsersController : Controller
{
    private UserManager<User> _userManager;

    public UsersController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    // GET
    public IActionResult Index() => 
        View(_userManager.Users.ToList());
 
    public async Task<IActionResult> Edit(string id)
    {
        User user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        EditUserViewModel model = new EditUserViewModel {Id = user.Id, Email = user.Email, UserName = user.UserName };
        return View(model);
    }
 
    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel editModel)
    {
        if (ModelState.IsValid)
        {
            User user = await _userManager.FindByIdAsync(editModel.Id);
            if (user!=null)
            {
                user.Email = editModel.Email;
                user.UserName = editModel.UserName;
                
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
        }
        return View(editModel);
    }
    
    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
        User user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
        }
        return RedirectToAction("Index");
    }
}
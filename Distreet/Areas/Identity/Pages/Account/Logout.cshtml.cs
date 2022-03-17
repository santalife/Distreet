using Distreet.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Distreet.Areas.Identity.Pages.Account;

public class Logout : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<Logout> _logger;

    public Logout(SignInManager<ApplicationUser> signInManager, ILogger<Logout> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }
    public void OnGet()
    {
        
    }
    public async Task<IActionResult> OnPost(string? returnUrl = null)
    {
        Console.WriteLine("Hello im logging out");
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out");
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return RedirectToPage();
        }
    }
}
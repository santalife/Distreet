using Distreet.Models.User;
using Distreet.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Distreet.Areas.Identity.Pages.Account;

public class Profile : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<Profile> _logger;
    private readonly UserService _userService;
    public Profile(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<Profile> logger,
        UserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _userService = userService;
    }
    [BindProperty] public ApplicationUser ApplicationUser { get; set; }
    
    public async Task OnGet(string residentId)
    {
        ApplicationUser = await _userManager.FindByNameAsync(residentId);
        Console.WriteLine(ApplicationUser.DateOfBirth);
    }
}
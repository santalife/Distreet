using System.Text;
using Distreet.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Distreet.Areas.Users.Pages
{
    public class ConfirmEmail : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmail(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        [TempData]
        public string StatusMessage { get; set; }
        
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load applicationUser with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}


using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Distreet.Models.User;
using Distreet.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Distreet.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class Register : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<Register> _logger;
        private readonly IEmailSender _emailSender;
        private readonly UserService _userService;
        public Register(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<Register> logger,
            IEmailSender emailSender,
            UserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _userService = userService;
        }

        [BindProperty] public InputModel? Input { get; set; }

        public string? ReturnUrl { get; set; }

        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
        [BindProperty] public string? errorMessage { get; set; }

        public class InputModel
        {
            public string? ProfilePictureJson { get; set; }

            [Required]
            [MaxLength(50)]
            [Display(Name = "Full Name")]
            public string? FullName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string? Email { get; set; }

            [Required]
            [MaxLength(75)]
            [Display(Name = "Address Street")]
            public string? AddressStreet { get; set; }

            [Display(Name = "Block Number")] public string? BlockNumber { get; set; }

            [Required]
            [MaxLength(20)]
            [Display(Name = "Unit Number")]
            public string? UnitNumber { get; set; }

            [Required]
            [MaxLength(7)]
            [Display(Name = "Postal Code")]
            public string? PostalCode { get; set; }

            [Required(ErrorMessage = "Phone Number is Required")]
            [RegularExpression(@"^[89]\d{7}$", ErrorMessage = "Check your Phone Number again!")]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Date of Birth is Required")]
            [Display(Name = "Date of Birth")]
            public DateTime DateOfBirth { get; set; }

            [Required]
            [RegularExpression(@"^[SFTG]\d{7}[A-Z]$", ErrorMessage = "Check your NRIC again!")]
            [Display(Name = "NRIC")]
            public string? NRIC { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]

            public string? Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "This password does not match the one above.")]
            [Display(Name = "Confirm Password")]
            public string? ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var existUser = _userManager.FindByEmailAsync(Input.Email).Result;
                if (existUser != null)
                {
                    errorMessage = "Email already exists in the database";
                    return Page();
                }

                var user = _userService.PrepareApplicationUser(Input);
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new {area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl},
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new {email = Input.Email, returnUrl = returnUrl});
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
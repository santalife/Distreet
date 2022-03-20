using System.ComponentModel.DataAnnotations;
using Distreet.Models.User;
using Distreet.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Distreet.Areas.Identity.Pages.Account
{
    public class EditProfile : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EditProfile> _logger;
        private readonly IEmailSender _emailSender;
        private readonly UserService _userService;
        public EditProfile(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<EditProfile> logger,
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

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
        }
    }
}


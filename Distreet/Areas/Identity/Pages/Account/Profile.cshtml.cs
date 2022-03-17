using Distreet.Models;
using Distreet.Models.User;
using Distreet.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Distreet.Areas.Identity.Pages.Account;

public class Profile : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<Profile> _logger;
    private readonly UserService _userService;
    private readonly PostService _postService;
    public Profile(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<Profile> logger,
        UserService userService,
        PostService postService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _userService = userService;
        _postService = postService;
    }
    
    public class InputModel
    {
        public string? PostType { get; set; }
        public string? PostContent { get; set; }
        
        public List<PostImage>? PostImages { get; set; }
    }
    
    [BindProperty]
    public List<Post> UserPosts { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }
    [BindProperty] public ApplicationUser ApplicationUser { get; set; }
    
    public async Task OnGet(string residentId)
    {
        ApplicationUser = await _userManager.FindByNameAsync(residentId);
        Console.WriteLine(ApplicationUser.DateOfBirth);
        UserPosts = _postService.RetrievePostsFromUser(ApplicationUser);
    }

    public async Task<RedirectToPageResult> OnPost()
    {
        Console.WriteLine(Input.PostType);
        Console.WriteLine(Input.PostContent);
        var user = await _userManager.GetUserAsync(User);

        Post post = new Post
        {
            PostType = Input.PostType,
            PostContent = Input.PostContent,
            ApplicationUser = user
        };
        _postService.CreateStandardPost(post);
        return RedirectToPage("Profile");
    }
}
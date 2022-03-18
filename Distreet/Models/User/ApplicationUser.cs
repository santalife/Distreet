using Microsoft.AspNetCore.Identity;

namespace Distreet.Models.User;

public class ApplicationUser: IdentityUser
{
    public string? FullName { get; set; }
    public string? Type { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string? Status { get; set; }
    
    public int PointsEarned { get; set; }
    
    public string? ProfilePicture { get; set; }
    
    public List<Post>? Posts { get; set; }
}
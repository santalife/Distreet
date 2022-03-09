using Distreet.Models.User;

namespace Distreet.Models;

public class PostLike
{
    public int Id { get; set; }
    
    public ApplicationUser? ApplicationUser { get; set; }
    
    public Post? Post { get; set; }
}
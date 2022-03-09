using Distreet.Models.User;

namespace Distreet.Models;

public class PostComment
{
    public int Id { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime CommentDateTime { get; set; }
    
    public Post? Post { get; set; }
    
    public int Root { get; set; }
    
    public ApplicationUser? CommentBy { get; set; }
    
    public List<PostComment>? CommentReplies { get; set; }
}
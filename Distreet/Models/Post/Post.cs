namespace Distreet.Models;

public class Post
{
    public int Id { get; set; }
    
    public int PostType { get; set; }
    
    public string? PostContent { get; set; }
    
    public List<PostImage>? PostImages { get; set; }
    
    public List<PostComment>? PostComments { get; set; }
    
    public List<PostLike>? PostLikes { get; set; }
}
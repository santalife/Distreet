using System.ComponentModel.DataAnnotations;
using Distreet.Models.User;

namespace Distreet.Models;

public class Post
{
    public int Id { get; set; }
    
    public ApplicationUser ApplicationUser { get; set; }
    public string? PostType { get; set; }
    public string? PostContent { get; set; }
    public DateTime PostDateTime { get; set; }
    
    public DateTime LastUpdated { get; set; }
    public List<PostImage>? PostImages { get; set; }
    
    public List<PostComment>? PostComments { get; set; }
    
    public List<PostLike>? PostLikes { get; set; }
}
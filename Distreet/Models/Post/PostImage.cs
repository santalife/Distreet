namespace Distreet.Models;

public class PostImage
{
    public int Id { get; set; }

    public string? Image { get; set; }

    public string? ImageName { get; set; }
    
    public string? FileType { get; set; }

    public DateTime DateCreated { get; set; }
}
using Distreet.Models.User;

namespace Distreet.Models;

public class Event
{
    public int Id { get; set; }
    
    public string? Name { get; set; }

    public DateTime PublishedDate { get; set; }
    
    public int Price { get; set; }
    
    public string? Description { get; set; }
    
    public string? VenueLocation { get; set; }
    
    public string? VenueLink { get; set; }
    
    public int MaxPax { get; set; }
    
    public int Points { get; set; }
    
    public string? Status { get; set; }
    
    public ApplicationUser? CreatedBy { get; set; }

    public List<EventImage>? EventImages { get; set; }

    public List<EventLike>? LikeList { get; set; }
}
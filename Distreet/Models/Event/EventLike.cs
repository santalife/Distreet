using Distreet.Models.User;

namespace Distreet.Models;

public class EventLike
{
    public int Id { get; set; }
    
    public ApplicationUser? ApplicationUser { get; set; }
    
    public Event? Event { get; set; }
}
using Distreet.Models.User;

namespace Distreet.Models;

public class EventFeedback
{
    public int Id { get; set; }
    
    public string? Feedback { get; set; }
    
    public DateTime FeedbackDateTime { get; set; }
    
    public Event? Event { get; set; }
    
    public int Root { get; set; }
    
    public ApplicationUser? FeedbackBy { get; set; }
    
    public List<EventFeedback>? FeedbackReplies { get; set; }
    
}
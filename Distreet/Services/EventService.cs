using Distreet.Models;
using Distreet.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Distreet.Services;

public class EventService
{
    private DistreetDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public EventService(DistreetDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public void AddEvent(EventForm eventForm, ApplicationUser user)
    {
        Event newEvent;
        newEvent = eventForm.Event;
        newEvent.EventImages = GlobalService.PrepareEventImages(eventForm);
        _context.Add(newEvent);
        _context.SaveChanges();
    }

    public void RemoveEvent(Event @event, ApplicationUser user)
    {
    }

    public void UpdateEvent()
    {
    }

    public List<Event> RetrieveAllEvents()
    {
        List<Event> allEvents = new List<Event>();
        return allEvents;
    }
}
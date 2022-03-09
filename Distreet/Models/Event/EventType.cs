using System.ComponentModel;

namespace Distreet.Models;

public enum EventType
{
    Concert,
    [Description("Convention / Exhibition")]
    ConventionExhibition,
    Festival,
    Markets,
    [Description("Pop-Up")]
    PopUp,
    Installation,
    [Description("Online Event")]
    OnlineEvent,
    Show,
    Tournament,
    [Description("Marathon / Run")]
    MarathonRun,
    Seminar,
    Movie,
    [Description("Live House")]
    LiveHouse,
    Conference,
    Party,
    Gala,
    [Description("Meet & Greets")]
    MeetnGreets,
    Others
}
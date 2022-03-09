using Distreet.Models;
using Newtonsoft.Json.Linq;

namespace Distreet.Services;

public static class GlobalService
{
    public static List<EventImage> PrepareEventImages(EventForm eventForm)
    {
        List<EventImage> eventImages = new List<EventImage>();
        if (eventForm.UplodaedImages.Count != 0)
        {
            foreach (var image in eventForm.UplodaedImages)
            {

                JObject jObjectImage = JObject.Parse(image);
                string name = (string) jObjectImage["name"];
                string data = (string) jObjectImage["data"];
                string type = (string) jObjectImage["type"];
                string imageString = $"data:{type};base64,{data}";

                EventImage eventImage = new EventImage
                {
                    ImageName = name,
                    FileType = type,
                    Image = imageString
                };
                eventImages.Add(eventImage);
            }
        }
        
        return eventImages;
    }
}
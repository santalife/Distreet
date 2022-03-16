using Distreet.Areas.Identity.Pages.Account;
using Distreet.Models;
using Distreet.Models.User;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace Distreet.Services;

public class UserService
{
    private DistreetDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(DistreetDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public string PrepareProfilePicture(string? jsonString)
    {
        JObject jObjectImage = JObject.Parse(jsonString);
        string name = (string) jObjectImage["name"];
        string data = (string) jObjectImage["data"];
        string type = (string) jObjectImage["type"];
        string imageString = $"data:{type};base64,{data}";
        
        return imageString;
    }

    public ApplicationUser PrepareApplicationUser(Register.InputModel input)
    {
        string latestUsername;
        var latestuser = _context.DistreetUsers.OrderBy(e => e.Id).FirstOrDefault();
        if (latestuser == null)
        {
            latestUsername = "D0001";
        }
        else
        {
            var number = Convert.ToInt32(latestuser.UserName.Remove(0, 1));
            Console.WriteLine(number);
            number += 1;
            latestUsername = "D" + number.ToString("D4"); 
        }

        input.ProfilePictureJson = PrepareProfilePicture(input.ProfilePictureJson);
        var user = new ApplicationUser
        {
            UserName = latestUsername, Type = "Normal", Email = input.Email, DateOfBirth = input.DateOfBirth,
            PhoneNumber = input.PhoneNumber, Status = "Reviewing", PointsEarned = 0,
            ProfilePicture = input.ProfilePictureJson
        };
        return user;
    }
}
using System;
using System.Threading.Tasks;
using Distreet.Models;
using Distreet.Models.User;
using Distreet.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Distreet.Hubs
{
    public class PostHub : Hub
    {
        private UserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostHub(UserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        public async Task PostStandard()
        {
            
        }
    }
}


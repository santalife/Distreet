using System.Security.Cryptography.X509Certificates;
using Distreet.Models;
using Distreet.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Distreet.Services
{
    public class PostService
    {
        private DistreetDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostService(DistreetDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void CreateStandardPost(Post input)
        {
            // Post post = new Post
            // {
            //     PostType = "Standard",
            //     PostContent = content
            // };
            _context.Posts.Add(input);
            _context.SaveChanges();
        }

        public List<Post> RetrievePostsFromUser(ApplicationUser applicationUser)
        {
            return _context.Posts
                .Where(e => e.ApplicationUser == applicationUser)
                .Include(e => e.ApplicationUser)
                .Include(e=> e.PostImages)
                .Include(e=> e.PostComments)
                .Include(e=> e.PostLikes)
                .OrderByDescending(e => e.Id)
                .ToList();
        }
    } 
}


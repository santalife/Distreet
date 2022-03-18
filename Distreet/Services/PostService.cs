using System.Security.Cryptography.X509Certificates;
using Distreet.Areas.Identity.Pages.Account;
using Distreet.Hubs;
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

        public void CreateStandardPost(Profile.InputModel input, ApplicationUser applicationUser)
        {
            Post post = new Post
            {
                PostType = input.PostType,
                PostContent = input.PostContent,
                ApplicationUser = applicationUser,
                PostDateTime = DateTime.Now,
                LastUpdated = DateTime.Now
            };
            _context.Posts.Add(post);
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

        public Post RetrievePostFromId(int id)
        {
            return _context.Posts.Where(e => e.Id == id)
                .Include(e => e.ApplicationUser)
                .Include(e=> e.PostImages)
                .Include(e=> e.PostComments)
                .Include(e=> e.PostLikes)
                .FirstOrDefault()!;
        }

        public void CreateCommentForPost(ApplicationUser applicationUser, int postId, Profile.CommentModel comment)
        {
            Post post = RetrievePostFromId(postId);
            PostComment postComment = new PostComment
            {
                Comment = comment.CommentContent,
                CommentDateTime = DateTime.Now,
                Post = post,
                CommentBy = applicationUser
            };
            post.PostComments?.Add(postComment);
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
    } 
}


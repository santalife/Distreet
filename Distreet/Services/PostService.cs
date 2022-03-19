using System.Security.Cryptography.X509Certificates;
using Distreet.Areas.Identity.Pages.Account;
using Distreet.Hubs;
using Distreet.Models;
using Distreet.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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

        public void CreatePicturePost(Profile.InputModel input, ApplicationUser applicationUser)
        {
            Post post = new Post()
            {
                PostType = input.PostType,
                PostContent = input.PostContent,
                ApplicationUser = applicationUser,
                PostDateTime = DateTime.Now,
                LastUpdated = DateTime.Now,
                PostImages = PreparePostImages(input)
            };
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public List<Post> RetrievePostsFromUser(ApplicationUser applicationUser)
        {
            List<Post> posts = _context.Posts
                .Where(e => e.ApplicationUser == applicationUser)
                .Include(e => e.ApplicationUser)
                .Include(e=> e.PostImages)
                .Include(e=> e.PostComments)
                .Include(e=> e.PostLikes)
                .OrderByDescending(e => e.Id)
                .ToList();

            foreach (var post in posts)
            {
                post.PostComments = RetrievePostCommentsFromPost(post);
            }

            return posts;
        }

        public List<PostComment> RetrievePostCommentsFromPost(Post post)
        {
            return _context.PostComments.Where(e => e.Post == post)
                .Include(e => e.CommentBy)
                .Include(e => e.CommentReplies)
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
        
        public static List<PostImage> PreparePostImages(Profile.InputModel input)
        {
            List<PostImage> postImages = new List<PostImage>();
            if (input.PostImages != null && input.PostImages.Count != 0)
            {
                foreach (var image in input.PostImages)
                {

                    JObject jObjectImage = JObject.Parse(image);
                    string name = (string) jObjectImage["name"];
                    string data = (string) jObjectImage["data"];
                    string type = (string) jObjectImage["type"];
                    string imageString = $"data:{type};base64,{data}";

                    PostImage postImage = new PostImage
                    {
                        ImageName = name,
                        FileType = type,
                        Image = imageString
                    };
                    postImages.Add(postImage);
                }
            }
        
            return postImages;
        }
    } 
}


using Distreet.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Distreet.Models
{
    public class DistreetDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _config;

        public DistreetDbContext(IConfiguration configuration)
        {
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _config.GetConnectionString("DistreetDB");
            optionsBuilder.UseSqlServer(connectionString);
        }
    
        //Entities
        
        //User
        public DbSet<ApplicationUser> DistreetUsers { get; set; }

        //Events
        public DbSet<Event> Events { get; set; }
        public DbSet<EventFeedback> EventFeedbacks { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<EventLike> EventLikes { get; set; }
        
        //Post
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }

    }
}


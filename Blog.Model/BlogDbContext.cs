using Blog.Model.Models;
using System.Data.Entity;

namespace Blog.Model
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext() : base("BlogConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<Banner> Banners { set; get; }
        public DbSet<Block> Blocks { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<UserGroup> UserGroups { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<Credential> Credentials { set; get; }


        protected override void OnModelCreating(DbModelBuilder builder)
        {
        }
    }
}
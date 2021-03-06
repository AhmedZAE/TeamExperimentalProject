using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreData
{
    public class ApplicationDbContext : DbContext
    {

        #region Entity Sets
#nullable disable

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

#nullable enable
        #endregion


        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #endregion


        #region Configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        #endregion


        #region Helper Methods

        public bool UserExists(int id) => Users.Any(u => u.UserId == id);
        public bool PostExists(int id) => Posts.Any(p => p.PostId == id);

        public bool UserNameExists(string username) => Users.Any(u => u.UserName == username);
        public bool UserEmailExists(string email) => Users.Any(u => u.Email == email);
        public bool UserNameOrEmailExists(string username, string email) => Users.Any(u => u.UserName == username || u.Email == email);

        #endregion

    }
}

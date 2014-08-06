using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialNetwork.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<CommentModel> Comments { get; set; }
        public virtual ICollection<LikeModel> Likes { get; set; }
        public virtual ICollection<UserTaskModel> UserTasks { get; set; }
        public virtual ICollection<UserSolvedTaskModel> UserSolvedTask { get; set; }
        public double UserRate { get; set; }
        public int AttemptAmount { get; set; }
        public string UserPhotoUrl { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<UserTaskModel> UserTasks { get; set; }
        public DbSet<TaskSolutionModel> Solutions { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<LikeModel> Likes { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<UserSolvedTaskModel> SolvedTasks { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project_Builder_Development.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
            : base("DefaultConnection", throwIfV1Schema: false){        }

        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Category> Categories{ get; set; }
        public virtual DbSet<Idea> Ideas { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<ReplyReply> ReplyReplies { get; set; }
        public virtual DbSet<React> Reacts { get; set; }
        public virtual DbSet<UserName> UserNames { get; set; } 
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<TaskGiven> TasksGiven { get; set; }

        // Turn OFF cascade delete, which is (unfortunately) the default setting
        // for Code First generated databases
        // For most apps, we do NOT want automatic cascade delete behaviour
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // First, call the base OnModelCreating method,
            // which uses the existing class definitions and conventions

            base.OnModelCreating(modelBuilder);

            // Then, turn off "cascade delete" for 
            // all default convention-based associations

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
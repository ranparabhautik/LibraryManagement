//using LibraryManagement.DemoClasses;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace LibraryManagement.Data
//{
//    public class DemoDBContext(DbContextOptions<DemoDBContext> options):IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>(options)
//    {
//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            builder.Entity<ApplicationUser>().ToTable("Users");
//            builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
//            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
//            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");
//            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");
//            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");
//            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");

//            base.OnModelCreating(builder);
//        }
//        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
//        {
//            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
//        }
//    }
//}

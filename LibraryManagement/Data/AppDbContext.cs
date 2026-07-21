using LibraryManagement.Model;
using LibraryManagement.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>(options)
    {
        public DbSet<Book> Books{ get; set; }
        public DbSet<BookCopy> BookCopies{ get; set; }
        public DbSet<BorrowRecord> BorrowRecords{ get; set; }
        public DbSet<Member> Members{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Identity Table Names

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");

            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

            #endregion

            modelBuilder.Entity<Book>().HasMany(x => x.Copies).WithOne(x => x.Books).HasForeignKey(x => x.BookId);
            modelBuilder.Entity<Member>().HasOne(member => member.ApplicationUser).WithOne(user => user.Member).HasForeignKey<Member>(member => member.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Member>().HasMany(member => member.BorrowRecords).WithOne(record => record.Member).HasForeignKey(record => record.MemberId);
            modelBuilder.Entity<BookCopy>().HasMany(x => x.BorrowRecords).WithOne(x => x.BookCopy).HasForeignKey(x => x.BookCopyId);
            modelBuilder.Entity<Book>().HasQueryFilter(x => !x.IsDeletable);
           
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IsDeletable>().Where(x => x.State == EntityState.Deleted);

            foreach(var item in entries)
            {
                item.State = EntityState.Modified;
                item.Entity.IsDeletable = true;
            }

            return base.SaveChangesAsync(cancellationToken);
        }



    }
}

using LibraryManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        public DbSet<Book> Books{ get; set; }
        public DbSet<BookCopy> BookCopies{ get; set; }
        public DbSet<BorrowRecord> BorrowRecords{ get; set; }
        public DbSet<Member> Members{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasMany(x => x.Copies).WithOne(x => x.Books).HasForeignKey(x => x.BookId);
            modelBuilder.Entity<Member>().HasMany(x => x.BorrowRecords).WithOne(x => x.Member).HasForeignKey(x => x.MemberId);
            modelBuilder.Entity<BookCopy>().HasMany(x => x.BorrowRecords).WithOne(x => x.BookCopy).HasForeignKey(x => x.BookCopyId);

            //modelBuilder.Entity<Book>().HasQueryFilter(x => !x.IsDeletable);         
        }
    }
}

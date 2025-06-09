using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Reader> Readers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Statyczne wartości GUID i DateTime do seeda
            var readerId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var bookId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var borrowId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var borrowDate = new DateTime(2024, 1, 1);

            modelBuilder.Entity<Reader>().HasData(
                new Reader
                {
                    Id = readerId,
                    FirstName = "Jan",
                    LastName = "Kowalski"
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = bookId,
                    Title = "Wiedźmin",
                    Author = "Andrzej Sapkowski",
                    CopiesAvailable = 3
                }
            );

            modelBuilder.Entity<BorrowRecord>().HasData(
                new BorrowRecord
                {
                    Id = borrowId,
                    ReaderId = readerId,
                    BookId = bookId,
                    BorrowedAt = borrowDate,
                    ReturnedAt = null
                }
            );
        }
    }
}

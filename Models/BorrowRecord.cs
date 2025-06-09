namespace LibraryApi.Models
{
    public class BorrowRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ReaderId { get; set; }
        public Reader Reader { get; set; } = null!;

        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;

        public DateTime BorrowedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? ReturnedAt { get; set; }
        public bool IsReturned { get; internal set; }
    }
}

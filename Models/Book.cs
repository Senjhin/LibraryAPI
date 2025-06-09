namespace LibraryApi.Models
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string Title { get; set; } = null!;
        
        public string? Author { get; set; }
        
        public int CopiesAvailable { get; set; }
        
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}

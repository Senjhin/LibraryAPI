namespace LibraryApi.Dtos
{
    public class BorrowRecordDto
    {
        public Guid Id { get; set; }
        public Guid ReaderId { get; set; }
        public required string ReaderName { get; set; }
        public required string ReaderLastName { get; set; } // Added property
        public Guid BookId { get; set; }
        public required string BookTitle { get; set; }
        public DateTime BorrowedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
    }
}

namespace LibraryApi.Dtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public int CopiesAvailable { get; set; }
    }
}

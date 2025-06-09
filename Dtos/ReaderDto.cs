namespace LibraryApi.Dtos
{
    public class ReaderDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
namespace LibraryApi.Dtos
{
    public class UpdateBookDto
    {
        // Nullable pola do patchowania (w PATCH mogą być pominięte)
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? CopiesAvailable { get; set; }
    }
}
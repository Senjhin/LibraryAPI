using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [Range(0, int.MaxValue)]
        public int CopiesAvailable { get; set; }
    }
}

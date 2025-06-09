using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class CreateReaderDto
    {
        [Required]
        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Dtos
{
    public class BorrowRequestDto
    {
        [Required]
        public Guid ReaderId { get; set; }

        [Required]
        public Guid BookId { get; set; }
    }
}

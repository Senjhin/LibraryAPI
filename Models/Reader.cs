using Newtonsoft.Json;

namespace LibraryApi.Models
{
    public class Reader
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string FirstName { get; set; } = null!;
        
        public string? LastName { get; set; }

        // Nawigacja do pożyczek
        [JsonIgnore]
        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}

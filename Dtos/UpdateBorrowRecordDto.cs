namespace LibraryApi.Dtos
{
    public class UpdateBorrowRecordDto
    {
        public Guid? ReaderId { get; set; }
        public Guid? BookId { get; set; }
        public bool? IsReturned { get; set; }
        
    }
}

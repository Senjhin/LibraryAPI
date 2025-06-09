using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.JsonPatch;
using LibraryApi.Models;
using LibraryApi.Dtos; // Ensure this namespace contains UpdateBorrowRecordDto

public class BorrowRecordPatchExample : IExamplesProvider<JsonPatchDocument<UpdateBorrowRecordDto>>
{
    public JsonPatchDocument<UpdateBorrowRecordDto> GetExamples()
    {
        var patchDoc = new JsonPatchDocument<UpdateBorrowRecordDto>();
        patchDoc.Replace(b => b.BookId, Guid.Parse("11111111-1111-1111-1111-111111111111"));
        patchDoc.Replace(b => b.ReaderId, Guid.Parse("22222222-2222-2222-2222-222222222222"));
        patchDoc.Replace(b => b.IsReturned, true);

        return patchDoc;
    }
}

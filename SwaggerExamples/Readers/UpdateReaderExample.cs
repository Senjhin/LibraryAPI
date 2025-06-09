using LibraryApi.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace LibraryApi.SwaggerExamples.Readers
{
    public class UpdateReaderExample : IExamplesProvider<UpdateReaderDto>
    {
        public UpdateReaderDto GetExamples()
        {
            return new UpdateReaderDto
            {
                FirstName = "Jan",
                LastName = "Kowalski"
            };
        }
    }
}

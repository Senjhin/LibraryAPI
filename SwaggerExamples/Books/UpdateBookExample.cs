using LibraryApi.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace LibraryApi.SwaggerExamples.Books
{
    public class UpdateBookExample : IExamplesProvider<UpdateBookDto>
    {
        public UpdateBookDto GetExamples()
        {
            return new UpdateBookDto
            {
                Title = "Nowy tytuł książki",
                Author = "Nowy autor",
                CopiesAvailable = 10
            };
        }
    }
}

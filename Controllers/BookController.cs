using LibraryApi.Data;
using LibraryApi.Dtos;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/books")]
    [ApiVersion("1.0")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/v1/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        // GET: api/v1/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound(new { message = $"Book with id {id} not found." });

            return Ok(book);
        }

        // POST: api/v1/books
        [HttpPost]
        public async Task<ActionResult<Book>> Create([FromBody] UpdateBookDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Request body is null." });

            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title ?? throw new ArgumentException("Title is required."),
                Author = dto.Author ?? throw new ArgumentException("Author is required."),
                CopiesAvailable = dto.CopiesAvailable ?? 0
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = book.Id, version = "1.0" }, book);
        }

        // PUT: api/v1/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Request body is null." });

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound(new { message = $"Book with id {id} not found." });

            // Walidacja - można rozszerzyć np. [Required] na DTO
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest(new { message = "Title cannot be empty." });

            if (string.IsNullOrWhiteSpace(dto.Author))
                return BadRequest(new { message = "Author cannot be empty." });

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.CopiesAvailable = dto.CopiesAvailable ?? book.CopiesAvailable;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { message = "Error saving changes to database." });
            }

            return Ok(new { message = "Book updated successfully.", book });
        }

        // PATCH: api/v1/books/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateBookDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Request body is null." });

            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound(new { message = $"Book with id {id} not found." });

            // Aktualizuj tylko pola które przyszły
            if (dto.Title != null)
            {
                if (string.IsNullOrWhiteSpace(dto.Title))
                    return BadRequest(new { message = "Title cannot be empty if provided." });
                book.Title = dto.Title;
            }

            if (dto.Author != null)
            {
                if (string.IsNullOrWhiteSpace(dto.Author))
                    return BadRequest(new { message = "Author cannot be empty if provided." });
                book.Author = dto.Author;
            }

            if (dto.CopiesAvailable.HasValue)
            {
                if (dto.CopiesAvailable < 0)
                    return BadRequest(new { message = "CopiesAvailable cannot be negative." });
                book.CopiesAvailable = dto.CopiesAvailable.Value;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { message = "Error saving changes to database." });
            }

            return Ok(new { message = "Book updated successfully.", book });
        }

        // DELETE: api/v1/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound(new { message = $"Book with id {id} not found." });

            _context.Books.Remove(book);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { message = "Error deleting book from database." });
            }

            return NoContent(); // 204 No Content na powodzenie usunięcia
        }
    }
}

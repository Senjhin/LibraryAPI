using LibraryApi.Data;
using LibraryApi.Dtos;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/readers")]
    [ApiVersion("1.0")]
    public class ReadersController : ControllerBase
    {
        private readonly LibraryContext _context;

        public ReadersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/v1/readers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reader>>> GetAll()
        {
            var readers = await _context.Readers.ToListAsync();
            return Ok(readers);
        }

        // GET: api/v1/readers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Reader>> GetById(Guid id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
                return NotFound(new { message = $"Reader with id {id} not found." });

            return Ok(reader);
        }

        // POST: api/v1/readers
        [HttpPost]
        public async Task<ActionResult<Reader>> Create([FromBody] UpdateReaderDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Request body is null." });

            if (string.IsNullOrWhiteSpace(dto.FirstName))
                return BadRequest(new { message = "FirstName is required." });

            var reader = new Reader
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName ?? string.Empty
            };

            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = reader.Id, version = "1.0" }, reader);
        }

        // PUT: api/v1/readers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReaderDto dto)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
                return NotFound(new { message = $"Reader with id {id} not found." });

            if (string.IsNullOrWhiteSpace(dto.FirstName))
                return BadRequest(new { message = "FirstName is required." });

            reader.FirstName = dto.FirstName;
            reader.LastName = dto.LastName ?? reader.LastName;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Reader updated successfully.", reader });
        }

        // PATCH: api/v1/readers/{id}
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerRequestExample(typeof(UpdateReaderDto), typeof(SwaggerExamples.Readers.UpdateReaderExample))]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateReaderDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "Request body is null." });

            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
                return NotFound(new { message = $"Reader with id {id} not found." });

            if (dto.FirstName != null)
            {
                if (string.IsNullOrWhiteSpace(dto.FirstName))
                    return BadRequest(new { message = "FirstName cannot be empty if provided." });

                reader.FirstName = dto.FirstName;
            }

            if (dto.LastName != null)
            {
                reader.LastName = dto.LastName;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Reader updated successfully.", reader });
        }

        // DELETE: api/v1/readers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader == null)
                return NotFound(new { message = $"Reader with id {id} not found." });

            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.Data;
using LibraryApi.Dtos;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/borrowrecords")]
    public class BorrowRecordsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowRecordsController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowRecordDto>>> GetAll()
        {
            var borrowRecords = await _context.BorrowRecords
                .Include(br => br.Reader)
                .Include(br => br.Book)
                .Select(br => new BorrowRecordDto
                {
                    Id = br.Id,
                    ReaderId = br.ReaderId,
                    ReaderName = br.Reader.FirstName,
                    ReaderLastName = br.Reader.LastName ?? string.Empty,
                    BookId = br.BookId,
                    BookTitle = br.Book.Title,
                    BorrowedAt = br.BorrowedAt,
                    ReturnedAt = br.ReturnedAt
                })
                .ToListAsync();

            return Ok(borrowRecords);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowRecordDto>> GetById(Guid id)
        {
            var br = await _context.BorrowRecords
                .Include(b => b.Reader)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (br == null)
                return NotFound(new { message = $"Borrow record with ID {id} not found." });

            var dto = new BorrowRecordDto
            {
                Id = br.Id,
                ReaderId = br.ReaderId,
                ReaderName = br.Reader.FirstName,
                ReaderLastName = br.Reader.LastName ?? string.Empty,
                BookId = br.BookId,
                BookTitle = br.Book.Title,
                BorrowedAt = br.BorrowedAt,
                ReturnedAt = br.ReturnedAt
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<BorrowRecordDto>> Create([FromBody] CreateBorrowRecordDto createDto)
        {
            if (createDto == null)
                return BadRequest(new { message = "Request body cannot be empty." });

            var reader = await _context.Readers.FindAsync(createDto.ReaderId);
            if (reader == null)
                return BadRequest(new { message = $"Reader with ID {createDto.ReaderId} not found." });

            var book = await _context.Books.FindAsync(createDto.BookId);
            if (book == null)
                return BadRequest(new { message = $"Book with ID {createDto.BookId} not found." });

            if (book.CopiesAvailable <= 0)
                return BadRequest(new { message = "No copies available for this book." });

            var borrowRecord = new BorrowRecord
            {
                Id = Guid.NewGuid(),
                ReaderId = createDto.ReaderId,
                BookId = createDto.BookId,
                BorrowedAt = DateTime.UtcNow,
                IsReturned = false,
                ReturnedAt = null
            };

            book.CopiesAvailable--;

            _context.BorrowRecords.Add(borrowRecord);
            await _context.SaveChangesAsync();

            var dto = new BorrowRecordDto
            {
                Id = borrowRecord.Id,
                ReaderId = reader.Id,
                ReaderName = reader.FirstName,
                ReaderLastName = reader.LastName ?? string.Empty,
                BookId = book.Id,
                BookTitle = book.Title,
                BorrowedAt = borrowRecord.BorrowedAt,
                ReturnedAt = borrowRecord.ReturnedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = dto.Id, version = "1.0" }, dto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBorrowRecord(Guid id, [FromBody] UpdateBorrowRecordDto updateDto)
        {
            if (updateDto == null)
                return BadRequest(new { message = "Update payload is missing." });

            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null)
                return NotFound(new { message = $"Borrow record with ID {id} not found." });

            // ReaderId
            if (updateDto.ReaderId.HasValue)
            {
                var readerExists = await _context.Readers.AnyAsync(r => r.Id == updateDto.ReaderId.Value);
                if (!readerExists)
                    return BadRequest(new { message = $"Reader with ID {updateDto.ReaderId} does not exist." });

                record.ReaderId = updateDto.ReaderId.Value;
            }

            // BookId
            if (updateDto.BookId.HasValue)
            {
                var bookExists = await _context.Books.AnyAsync(b => b.Id == updateDto.BookId.Value);
                if (!bookExists)
                    return BadRequest(new { message = $"Book with ID {updateDto.BookId} does not exist." });

                record.BookId = updateDto.BookId.Value;
            }

            // IsReturned
            if (updateDto.IsReturned.HasValue)
            {
                if (record.IsReturned && updateDto.IsReturned.Value)
                    return BadRequest(new { message = "Book is already marked as returned." });

                record.IsReturned = updateDto.IsReturned.Value;
                record.ReturnedAt = updateDto.IsReturned.Value ? DateTime.UtcNow : null;

                if (updateDto.IsReturned.Value)
                {
                    var book = await _context.Books.FindAsync(record.BookId);
                    if (book != null)
                        book.CopiesAvailable++;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Borrow record updated successfully.",
                record = new
                {
                    record.Id,
                    record.ReaderId,
                    record.BookId,
                    record.IsReturned,
                    record.BorrowedAt,
                    record.ReturnedAt
                }
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var borrowRecord = await _context.BorrowRecords.FindAsync(id);
            if (borrowRecord == null)
                return NotFound(new { message = $"Borrow record with ID {id} not found." });

            _context.BorrowRecords.Remove(borrowRecord);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Borrow record with ID {id} deleted." });
        }
    }
}

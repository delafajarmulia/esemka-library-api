using EsemkaLibrary.Model;
using EsemkaLibrary.ModelDto;
using EsemkaLibrary.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsemkaLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookInformationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _iconfiguration;
        private readonly IUserService _user;

        public BookInformationController(DataContext context, IConfiguration iconfiguration, IUserService user)
        {
            _context = context;
            _iconfiguration = iconfiguration;
            _user = user;
        }

        [HttpGet("{isbn}/Books"), Authorize(Roles ="Librarian")]
        public async Task<ActionResult<List<BookInformation>>> GetBookInformation(string isbn)
        {
            BookInformation? bookInformation = await _context.BookInformations.Where(bi => bi.Isbn == isbn).Include(b => b.Books).FirstOrDefaultAsync();
            if(bookInformation == null)
            {
                return NotFound();
            }else
            {
                return Ok(bookInformation);
            }
        }

        [HttpPost("{isbn}/Books"), Authorize(Roles ="Librarian")]
        public async Task<ActionResult<Book>> PostBookToIsbn(string isbn, BookDto bookDto)
        {
            BookInformation? bookInformation = await _context.BookInformations.Where(b => b.Isbn == isbn).FirstOrDefaultAsync();

            if(bookInformation == null)
                return NotFound();

            var book = new Book
            {
                Isbn = isbn,
                Code = bookDto.Code,
                IsTadon = bookDto.IsTadon,
                ShelfCode = bookDto.ShelfCode,
                Source = bookDto.Source,
                SourceDesc = bookDto.SourceDesc,
                BookInformationIsbn = bookInformation
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpGet("{isbn}/Books/{code}"), Authorize(Roles ="Librarian")]
        public async Task<ActionResult<Book>> GetBookByIsbnAndCode(string isbn, string code)
        {
            BookInformation? bookInformation = await _context.BookInformations.Where(bi => bi.Isbn == isbn).Include(b => b.Books.Where(b => b.Code == code)).FirstOrDefaultAsync();

            if(bookInformation == null)
                return NotFound();
            return Ok(bookInformation);
        }

        [HttpPut("{isbn}/Books/{code}"), Authorize(Roles ="Librarian")]
        public async Task<ActionResult<Book>> PutBookByIsbnCode(string isbn, string code, BookDto bookDto)
        {
            BookInformation? bookInformation = await _context.BookInformations.Where(bi => bi.Isbn == isbn).Include(b => b.Books.Where(b => b.Code == code)).FirstOrDefaultAsync();
            Book? book = await _context.Books.Where(b => b.BookInformationIsbn == bookInformation).FirstOrDefaultAsync();  

            if (bookInformation == null)
                return NotFound();
            else if(book == null) 
                return NotFound();

            book.Isbn = isbn;
            book.Code = code;
            book.Isbn = bookDto.IsTadon;
            book.ShelfCode = bookDto.ShelfCode;
            book.Source = bookDto.Source;
            book.SourceDesc = bookDto.SourceDesc;
            book.BookInformationIsbn = bookInformation;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpDelete("{isbn}/Books/{code}"), Authorize(Roles ="Librarian")]
        public async Task<ActionResult<List<BookInformation>>> DeleteBook(string isbn, string code)
        {
            BookInformation? bookInformation = await _context.BookInformations.Where(bi => bi.Isbn == isbn).Include(b => b.Books.Where(b => b.Code == code)).FirstOrDefaultAsync();
            Book? book = await _context.Books.Where(b => b.BookInformationIsbn == bookInformation).FirstOrDefaultAsync();

            if (bookInformation == null)
                return NotFound();
            else if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok(bookInformation);
        }
    }
}

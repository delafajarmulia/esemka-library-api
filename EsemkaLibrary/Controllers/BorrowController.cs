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
    public class BorrowController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _user;

        public BorrowController(DataContext context, IConfiguration configuration, IUserService user)
        {
            _context = context;
            _configuration = configuration;
            _user = user;
        }

        [HttpGet("/Me"), Authorize]
        public async Task<ActionResult<List<Borrow>>> GetMyBorrow()
        {
            User? user = await _context.Users.Where(u => u.Name == _user.GetMyName()).FirstOrDefaultAsync();
            Borrow? borrow = await _context.Borrows.Where(b => b.EmailUser == user).FirstOrDefaultAsync();

            if(borrow == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(borrow);
            }
        }

        /*
         private bool BorrowExists(string code)
        {
            Book? book = _context.Books.Where(b => b.Code == code).FirstOrDefault();
            bool cek = (_context.Borrows?.Any(e => e.CodeBook == book)).GetValueOrDefault();
            if(cek != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            //return (_context.Borrows?.Any(e => e.CodeBook == book)).GetValueOrDefault();
        }

        


        
        */

        [HttpPost("{code}/Reserve")]
        public async Task<ActionResult<Borrow>> PostBorrow(string code, PostBorrowDto borrowDto) //Borrow borrow
        {
            Borrow? borrow = await _context.Borrows.Where(b => b.CodeBook.Code == code).FirstOrDefaultAsync();
            User? user = await _context.Users.Where(u => u.Id == borrowDto.IdUser).FirstOrDefaultAsync();
            BookInformation? bookInformation = await _context.BookInformations.Where(b => b.Id == borrowDto.IdBookInformation).FirstOrDefaultAsync();
            Book? book = await _context.Books.Where(b => b.Id == borrowDto.IdBook).FirstOrDefaultAsync();

            if (_context.Borrows == null)
            {
                return Problem("Entity set 'LibraryContext.Borrows'  is null.");
            }
            
            if(borrowDto.BorrowAt > DateTime.Now)
            {
                return Problem("Please check your time");
            }

            var newBorrow = new Borrow
            {
                Status = borrowDto.Status,
                EmailUser = user,
                BookInformationIsbn = bookInformation,
                CodeBook = book,
                BorrowAt = borrowDto.BorrowAt
            };

            /*
            borrow.Status = borrowDto.Status;
            borrow.EmailUser = user;
            borrow.BookInformationIsbn = borrow.BookInformationIsbn;
            borrow.CodeBook = borrow.CodeBook;
            borrow.BorrowAt = borrow.BorrowAt;
            borrow.ReturnAt = borrow.ReturnAt;
            borrow.DueAt = borrow.DueAt; 
            */

            _context.Borrows.Add(newBorrow);
            await _context.SaveChangesAsync();
            return Ok(newBorrow);
            /*try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BorrowExists(borrow.CodeBook.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBorrow", new { code = borrow.CodeBook }, borrow);*/
        }

        private bool BorrowExists(string code)
        {
            return (_context.Borrows?.Any(e => e.CodeBook.Code == code)).GetValueOrDefault();
        }

        [HttpPut("{id}/Take"), Authorize(Roles ="Librarian")]
        public async Task<IActionResult> PutBorrowTake(string id, string codeBook)
        {
            Borrow? borrow= await _context.Borrows.Where(b => b.Equals(id)).FirstOrDefaultAsync();
            //Book? book = await _context.Books.Where(b => b.Equals(borrow.CodeBook).First();

            if (borrow.CodeBook.Code != codeBook)
            {
                return BadRequest();
            }

            borrow.BorrowAt = DateTime.Now;
            borrow.Status = borrow.Status;
            borrow.EmailUser = borrow.EmailUser;
            borrow.BookInformationIsbn = borrow.BookInformationIsbn;
            borrow.CodeBook = borrow.CodeBook;

            //_context.Entry(borrow).State = EntityState.Modified;

            _context.Borrows.Update(borrow);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}/Return"), Authorize(Roles = "Librarian")]
        public async Task<IActionResult> PutBorrowReturn(string id, string codeBook)
        {
            Borrow? borrow = await _context.Borrows.Where(b => b.Equals(id)).FirstOrDefaultAsync();
            //Book? book = await _context.Books.Where(b => b.Equals(borrow.CodeBook).First();

            if (borrow.CodeBook.Code != codeBook)
            {
                return BadRequest();
            }

            borrow.ReturnAt = DateTime.Now;
            borrow.Status = borrow.Status;
            borrow.EmailUser = borrow.EmailUser;
            borrow.BookInformationIsbn = borrow.BookInformationIsbn;
            borrow.CodeBook = borrow.CodeBook;

            //_context.Entry(borrow).State = EntityState.Modified;

            _context.Borrows.Update(borrow);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /*[HttpPut("{id}/Cancel")]
        public async Task<IActionResult> PutBorrowCancel(string id, Borrow borrow)
        {
            if (id != borrow.CodeBook)
            {
                return BadRequest();
            }

            _context.Entry(borrow).State = EntityState.Modified;

            await _context.SaveChangesAsync();


            return NoContent();
        }*/

        /*[HttpDelete("{isbn}/Books/{code}")]
        public async Task<IActionResult> DeleteBookInformation(string isbn, string code)
        {
            if (_context.BookInformations == null)
            {
                return NotFound();
            }
            var bookInformation = await _context.BookInformations.FindAsync(isbn);
            if (bookInformation == null)
            {
                return NotFound();
            }

            _context.BookInformations.Remove(bookInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

    }
}

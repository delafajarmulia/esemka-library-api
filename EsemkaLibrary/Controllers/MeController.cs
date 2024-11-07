using Azure.Core;
using EsemkaLibrary.Migrations;
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
    public class MeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _user;

        public MeController(DataContext context, IConfiguration configuration, IUserService user)
        {
            _context = context;
            _configuration = configuration;
            _user = user;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<User>> GetMe()
        {
            User? user = await _context.Users.Where(u => u.Name == _user.GetMyName()).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            } else
            {
                return Ok(user);
            }
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<User>> PutMe(PostUserDto request)
        {
            User? user = await _context.Users.Where(u => u.Name == _user.GetMyName()).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Name = _user.GetMyName();
                user.Email = request.Email;
                user.Address = request.Address;
                user.Role = request.Role;
                user.Gender = request.Gender;
                user.DateOfBirth = request.DateOfBirth;
                user.PhoneNumber = request.PhoneNumber;
                user.PhotoPath = request.PhotoPath;
                user.Password = request.Password;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
        }

        [HttpGet("/Photo"), Authorize]
        public async Task<ActionResult<string>> GetMyPhoto()
        {
            User? user = await _context.Users.Where(u => u.Name == _user.GetMyName()).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user.PhotoPath);
            }
        }

        [HttpPost("/Photo"), Authorize]
        public async Task<ActionResult<string>> PostMyPhoto(string photoPath)
        {
            User? user = await _context.Users.Where(u => u.Name == _user.GetMyName()).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Name = _user.GetMyName();
                user.Email = user.Email;
                user.Address = user.Address;
                user.Role = user.Role;
                user.Gender = user.Gender;
                user.DateOfBirth = user.DateOfBirth;
                user.PhoneNumber = user.PhoneNumber;
                user.PhotoPath = photoPath;
                user.Password = user.Password;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user.PhotoPath);
            }
        }
    }
}

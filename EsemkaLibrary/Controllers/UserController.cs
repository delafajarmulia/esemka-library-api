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
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _user;

        public UserController(DataContext context, IConfiguration configuration, IUserService user)
        {
            _context = context;
            _configuration = configuration;
            _user = user;
        }

        [HttpGet, Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        
        [HttpPost, Authorize(Roles ="Admin")]
        public async Task<ActionResult<List<User>>> PostUser(PostUserDto postUserDto)
        {
            var newUser = new User
            {
                Name = postUserDto.Name,
                Password = postUserDto.Password,
                Email = postUserDto.Email,
                Role = postUserDto.Role,
                Gender = postUserDto.Gender,
                Address = postUserDto.Address,
                DateOfBirth = postUserDto.DateOfBirth,
                PhoneNumber = postUserDto.PhoneNumber,
                PhotoPath = postUserDto.PhotoPath
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok(newUser);
        }

        [HttpGet("{email}"), Authorize(Roles ="Admin")]
        public async Task<ActionResult<User>> GetUserByEmial(string email)
        {
            User? user = await _context.Users.FindAsync(email);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPut("{email}"), Authorize(Roles ="Admin")]
        public async Task<ActionResult<User>> PutUseByEmail(string email, PostUserDto request)
        {
            User? user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if(user == null)
                return NotFound();

            user.Name = request.Name;
            user.Email = email;
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
}

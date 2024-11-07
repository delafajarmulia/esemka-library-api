using EsemkaLibrary.Model;
using EsemkaLibrary.ModelDto;
using EsemkaLibrary.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EsemkaLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _user;

        public AuthController(DataContext context, IConfiguration configuration, IUserService user)
        {
            _context = context;
            _configuration = configuration;
            _user = user;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _user.GetMyName();
            return Ok(new
            {
                Username = userName
            });
            //return Ok(userName);
        }

        [HttpPost("/Login")]
        public async Task<ActionResult<User>> Login(LoginDto request)
        {
            User? user = await _context.Users.Where(u => u.Name == request.Name).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            } else if (user.Password != request.Password)
            {
                return BadRequest();
            }
            else
            {
                var token = CreateToken(user);
                user.Token = token;
                await _context.SaveChangesAsync();
                return Ok(token);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speer_Project.Data;
using Speer_Project.DTOs;
using Speer_Project.Model;
using System.Security.Cryptography;
using System.Text;

namespace Speer_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto userReg)
        {
 
            var user = new User();

            using var hmac = new HMACSHA512();

            user.Username = userReg.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userReg.password));
            user.PasswordSalt = hmac.Key;


            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User Create");
         }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto userLogin)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == userLogin.Username);

            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLogin.password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new User
            {
                Username = userLogin.Username
            };
        }


    }
}

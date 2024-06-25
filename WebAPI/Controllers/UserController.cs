using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _authContext.Users
                .FirstOrDefaultAsync(x => x.Username == userObj.Username && x.Password == userObj.Password);
            if (user == null)
                return NotFound(new { Message = "User not found!" });


            return Ok(new
            {
                Token = "",
                Message = "Login Success"
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();
            //check username
            if (await CheckUserNameExistAsync(userObj.Username))
                return BadRequest(new { Message = "Username Already Exists" });

            //check password
            var pass = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });


            userObj.Role = "User";
            userObj.Token = "";
            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered!"
            });

        }

        private async Task<bool> CheckUserNameExistAsync(string username)
        {
            return await _authContext.Users.AnyAsync(x => x.Username == username);
        }

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 6)
                sb.Append("Minimum password length should be 6" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]")
                && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric"+Environment.NewLine);
            if (!Regex.IsMatch(password, "[(.),(,),@]"))
                sb.Append("Password should contain special characters @ , ." + Environment.NewLine);
            return sb.ToString();
        }
    }
}

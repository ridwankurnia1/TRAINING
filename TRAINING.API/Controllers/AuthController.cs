using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TRAINING.API.Data;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            var userFromDb = await _repo.LoginX(user.Username, user.Password);
            if (userFromDb == null)
                return Unauthorized();
            
            if (userFromDb.XURCST == 0)
                return BadRequest("User Id Inactive");
            
            if (!string.IsNullOrEmpty(userFromDb.XUREMA))
                return BadRequest(userFromDb.XUREMA);


            var claim = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userFromDb.XUUSNO),
                new Claim(ClaimTypes.Name, userFromDb.XUUSNA),
                new Claim(ClaimTypes.PrimarySid, userFromDb.XUOGNO),
                new Claim(ClaimTypes.Email, userFromDb.XUEMAD),
                new Claim(ClaimTypes.Surname, userFromDb.XUGCEP),
                new Claim(ClaimTypes.PrimaryGroupSid, userFromDb.XUOGNO),
                new Claim(ClaimTypes.PrimarySid, userFromDb.XUEMNO),
                new Claim("locality", userFromDb.XUBRNO)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurePassword"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("Zlogin")]
        public async Task<IActionResult> ZLogin(UserLoginDto user)
        {
            var userFromDb = await _repo.Login(user.Username, user.Password);
            if (userFromDb == null)
                return Unauthorized();
            
            var claim = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userFromDb.ZUUSNO),
                new Claim(ClaimTypes.Name, userFromDb.ZUUSNA),
                new Claim(ClaimTypes.PrimarySid, userFromDb.ZUOGNO),
                new Claim(ClaimTypes.Email, userFromDb.ZUEMAD),
                new Claim("Location", userFromDb.ZUBRNO)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurePassword"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
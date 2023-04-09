
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Wrapper;

namespace tfg.Controllers.AuthController
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     
        public IConfiguration _configuration;   
        public AuthController(IRepositoryWrapper repository,IConfiguration configuration) 
        { 
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost]
        //[Route("Auth")]
        public IActionResult Auth([FromBody]User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                int loginUser = 0;
                loginUser = _repository.User.Login(user);
                //_repository.Save();
                Console.WriteLine(loginUser);

                if(loginUser <= 0 || loginUser == null){
                    return BadRequest("Incorrect Credentials");
                }

                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("id",loginUser.ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(4),
                    signingCredentials: singIn
                );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error "+ex.Message);
            }
        }
    }
}
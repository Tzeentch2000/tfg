
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Wrapper;

namespace tfg.Controllers.AuthController
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     
        public IConfiguration _configuration;   
        private IMapper _mapper;  
        public AuthController(IRepositoryWrapper repository,IConfiguration configuration,IMapper mapper) 
        { 
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        //[Route("Auth")]
        public IActionResult Auth([FromBody]UserForLoginDTO user)
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
                var userEntity = _mapper.Map<User>(user);
                loginUser = _repository.User.Login(userEntity);
                //_repository.Save();
                Console.WriteLine(loginUser);

                if(loginUser <= 0 || loginUser == null){
                    Log.Error("Error in authentication");
                    return BadRequest("Incorrect Credentials");
                }

                var role = _repository.User.isUserAdmin(loginUser);

                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("id",loginUser.ToString()),
                    new Claim("userRole",role.ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(10),
                    signingCredentials: singIn
                );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in authentication");
                return StatusCode(500, "Internal server error "+ex.Message);
            }
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Wrapper;

namespace tfg.Controllers.UserController
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     
        public IConfiguration _configuration;   
        public UserController(IRepositoryWrapper repository,IConfiguration configuration) 
        { 
            _repository = repository;
            _configuration = configuration;
        }
        [Authorize]
        [HttpGet] 
        //[AllowAnonymous]
        public async Task<IActionResult> GetAllUsers() 
        { 
            try 
            { 
                var users = await _repository.User.GetAllUsers1(); 

                return Ok(users); 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        /*[HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]User user)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.TokenValidation(identity);
                if(!rToken.success){
                    return BadRequest("Invalid Token");
                }
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
                //Console.WriteLine(loginUser);

                if(loginUser <= 0 || loginUser == null){
                    return BadRequest("Incorrect Credentials");
                }

                return Ok(rToken.result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error "+ex.Message);
            }
        }*/

        

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateUser([FromBody]User user)
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

                _repository.User.CreateUser(user);
                _repository.Save();

                return Created("created",user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
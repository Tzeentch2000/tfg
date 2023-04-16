
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Wrapper;
using Encrypt;

namespace tfg.Controllers.UserController
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     
        public IConfiguration _configuration;   
        private IMapper _mapper;   
        public UserController(IRepositoryWrapper repository,IConfiguration configuration,IMapper mapper) 
        { 
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet] 
        //[AllowAnonymous]
        public async Task<IActionResult> GetAllUsers() 
        { 
            try 
            { 
                var users = await _repository.User.GetAllUsersWithDetails(); 
                return Ok(users); 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error " + ex.Message); 
            } 
        }

        [Authorize]
        [HttpGet("{id}", Name = "UserById")] 
        public IActionResult GetById(int id) 
        { 
            try 
            { 
                var user = _repository.User.GetUserWithDetails(id); 
                if (user == null) 
                { 
                    return NotFound(); 
                } 
                else 
                { 
                    return Ok(user); 
                } 
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
        public IActionResult CreateUser([FromBody]UserForInsertDTO user)
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
                User userEntity = _mapper.Map<User>(user);
                userEntity.IsAdmin = false;
                userEntity.Password = Hash.EncryptPassword(userEntity.Password);
                _repository.User.CreateUser(userEntity);
                _repository.Save();

                return Created("created",userEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserForUpdateDTO user)
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

                if(id != user.Id){
                    return BadRequest("The ids dont match");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var tokenRole = rToken.result.IsAdmin;
                var tokenId = rToken.result.Id;

                if(!tokenRole && user.Id != tokenId){
                    return BadRequest("Invalid Tokens");
                }

                var userEntity = _repository.User.GetUserById(id);
                if (userEntity == null)
                {
                    return NotFound();
                }
                User userMappedEntity = _mapper.Map<User>(user);
                _repository.User.UpdateUser(userMappedEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }

                _repository.User.DeleteUser(user);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
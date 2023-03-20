
using Microsoft.AspNetCore.Mvc;
using Wrapper;

namespace UserController
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;        
        public UserController(IRepositoryWrapper repository) 
        { 
            _repository = repository;
        }
        
        [HttpGet] 
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

        [HttpPost]
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
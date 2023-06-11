
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Wrapper;

namespace tfg.Controllers.StateController
{
    [ApiController]
    [Route("[controller]")]
    public class StateController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     
        private IMapper _mapper;   
        public StateController(IRepositoryWrapper repository,IMapper mapper) 
        { 
            _repository = repository;
             _mapper = mapper;

        }
        
        [HttpGet] 
        public IActionResult GetAll() 
        { 
            try 
            { 
                var states = _repository.State.GetAllStates(); 
                return Ok(states); 
            } 
            catch (Exception ex) 
            { 
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("/Active/States", Name = "ActiveStates")] 
        public IActionResult GetActiveAll() 
        { 
            try 
            { 
                var states = _repository.State.GetActiveStates(); 
                return Ok(states); 
            } 
            catch (Exception ex) 
            { 
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}", Name = "StateById")] 
        public IActionResult GetById(int id) 
        { 
            try 
            { 
                var state = _repository.State.GetStateById(id); 
                if (state == null) 
                { 
                    return NotFound(); 
                } 
                else 
                { 
                    return Ok(state); 
                } 
            } 
            catch (Exception ex) 
            { 
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpPost]
        public IActionResult Create([FromBody]StateForInsertDTO state)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.TokenValidation(identity);

                if(!rToken.success){
                    Log.Error("Invalid Tokens");
                    return BadRequest("Invalid Token");
                }

                var tokenRole = rToken.result.IsAdmin;
                if(!tokenRole){
                     Log.Error("Invalid Tokens");
                    return BadRequest("Invalid Tokens");
                }

                if (state == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var stateEntity = _mapper.Map<State>(state);
                _repository.State.CreateState(stateEntity);
                _repository.Save();

                return Created("created",stateEntity);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]StateForInsertDTO state)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.TokenValidation(identity);

                if(!rToken.success){
                    Log.Error("Invalid Tokens");
                    return BadRequest("Invalid Token");
                }

                var tokenRole = rToken.result.IsAdmin;
                if(!tokenRole){
                     Log.Error("Invalid Tokens");
                    return BadRequest("Invalid Tokens");
                }

                if (state == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var stateEntity = _repository.State.GetStateById(id);
                if (stateEntity == null)
                {
                    return NotFound();
                }

                var stateEntityEdit = _mapper.Map<State>(state);
                _repository.State.UpdateState(stateEntityEdit);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.TokenValidation(identity);

                if(!rToken.success){
                    Log.Error("Invalid Tokens");
                    return BadRequest("Invalid Token");
                }

                var tokenRole = rToken.result.IsAdmin;
                if(!tokenRole){
                     Log.Error("Invalid Tokens");
                    return BadRequest("Invalid Tokens");
                }
                
                var state = _repository.State.GetStateById(id);
                if (state == null)
                {
                    return NotFound();
                }

                if (_repository.Book.booksByState(id).Any()) 
                {
                    return BadRequest("Cannot delete state. It has related books. Delete those books first"); 
                }

                _repository.State.DeleteState(state);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
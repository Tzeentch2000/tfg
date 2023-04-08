
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Wrapper;

namespace tfg.Controllers.StateController
{
    [ApiController]
    [Route("[controller]")]
    public class StateController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     
        private IMapper _mapper;   
        public StateController(IRepositoryWrapper repository) 
        { 
            _repository = repository;

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
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpPost]
        public IActionResult Create([FromBody]State state)
        {
            try
            {
                if (state == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.State.CreateState(state);
                _repository.Save();

                return Created("created",state);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]State state)
        {
            try
            {
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

                _repository.State.UpdateState(state);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /*[HttpDelete("{id}")]
        public IActionResult DeleteOwner(int id)
        {
            try
            {
                var category = _repository.Category.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }

                if (_repository.category.AccountsByOwner(id).Any()) 
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first"); 
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first"); 
                }

                _repository.Owner.DeleteOwner(owner);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }*/
    }
}
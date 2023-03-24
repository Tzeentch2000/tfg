
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Wrapper;

namespace tfg.Controllers.CategoryController
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     
        public IConfiguration _configuration;   
        public CategoryController(IRepositoryWrapper repository,IConfiguration configuration) 
        { 
            _repository = repository;
            _configuration = configuration;
        }
        
        [HttpGet] 
        public IActionResult GetAllOwners() 
        { 
            try 
            { 
                var categories = _repository.Category.GetAllCategories(); 
                return Ok(categories); 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}", Name = "CategoryById")] 
        public IActionResult GetOwnerById(int id) 
        { 
            try 
            { 
                var category = _repository.Category.GetCategoryById(id); 
                if (category == null) 
                { 
                    return NotFound(); 
                } 
                else 
                { 
                    return Ok(category); 
                } 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}/books")] 
        public IActionResult GetOwnerWithDetails(int id) 
        { 
            try 
            { 
                var categories = _repository.Category.GetCategoryWithDetails(id); 
                if (categories == null) 
                { 
                    return NotFound(); 
                } 
                else 
                { 
                    return Ok(categories); 
                } 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error"); 
            }
        }

        [HttpPost]
        public IActionResult CreateOwner([FromBody]Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.Category.CreateCategory(category);
                _repository.Save();

                return Created("created",category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(int id, [FromBody]Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var categoryEntity = _repository.Category.GetCategoryById(id);
                if (categoryEntity == null)
                {
                    return NotFound();
                }

                _repository.Category.UpdateCategory(category);
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
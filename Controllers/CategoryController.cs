
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Wrapper;

namespace tfg.Controllers.CategoryController
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     

        private IMapper _mapper;   
        public CategoryController(IRepositoryWrapper repository,IMapper mapper) 
        { 
            _repository = repository;
            _mapper = mapper;

        }
        
        [HttpGet] 
        public IActionResult GetAll() 
        { 
            try 
            { 
                var categories = _repository.Category.GetAllCategories(); 
                var categoriesResult = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                return Ok(categoriesResult); 
            } 
            catch (Exception ex) 
            { 
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("/Active/Categories", Name = "ActiveCategories")] 
        public IActionResult GetActiveAll() 
        { 
            try 
            { 
                var categories = _repository.Category.GetActiveCategories(); 
                var categoriesResult = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                return Ok(categoriesResult); 
            } 
            catch (Exception ex) 
            { 
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}", Name = "CategoryById")] 
        public IActionResult GetById(int id) 
        { 
            try 
            { 
                var category = _repository.Category.GetCategoryById(id); 
                var categoryResult = _mapper.Map<CategoryDTO>(category);
                if (category == null) 
                { 
                    return NotFound(); 
                } 
                else 
                { 
                    return Ok(categoryResult); 
                } 
            } 
            catch (Exception ex) 
            { 
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpPost]
        public IActionResult Create([FromBody]CategoryForInsertDTO category)
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

                var categoryEntity = _mapper.Map<Category>(category);
                _repository.Category.CreateCategory(categoryEntity);
                _repository.Save();

                return Created("created",categoryEntity);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Category category)
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
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var category = _repository.Category.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }

                if (_repository.Book.booksByCategories(id).Any()) 
                {
                    return BadRequest("Cannot delete category. It has related books. Delete those books first"); 
                }

                _repository.Category.DeleteCategory(category);
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

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Wrapper;

namespace tfg.Controllers.BookController
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     
        private IMapper _mapper;   
        public BookController(IRepositoryWrapper repository,IMapper mapper) 
        { 
            _repository = repository;
             _mapper = mapper;
        }

        [HttpGet] 
        public IActionResult GetAll() 
        { 
            try 
            { 
                var books = _repository.Book.GetAllBooksWithDetails(); 
                var booksResult = _mapper.Map<IEnumerable<BookDTO>>(books);
                return Ok(booksResult); 
            } 
            catch (Exception ex) 
            { 
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet ("/Active/Books", Name = "ActiveBooks")]  
        public IActionResult GetActiveBooks() 
        { 
            try 
            { 
                var books = _repository.Book.GetActiveBooksWithDetails(); 
                var booksResult = _mapper.Map<IEnumerable<BookDTO>>(books);
                return Ok(booksResult); 
            } 
            catch (Exception ex) 
            { 
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}", Name = "BookById")] 
        public IActionResult GetById(int id) 
        { 
            try 
            { 
                var book = _repository.Book.GetBookWithDetails(id); 
                var bookResult = _mapper.Map<BookDTO>(book);
                if (book == null) 
                { 
                    return NotFound(); 
                } 
                else 
                { 
                    return Ok(bookResult); 
                } 
            } 
            catch (Exception ex) 
            { 
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error"); 
            } 
        }


        
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody]BookForInsertDTO book)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.TokenValidation(identity);

                if(!rToken.success){
                    return BadRequest("Invalid Token");
                }

                if (book == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var tokenRole = rToken.result.IsAdmin;
                if(!tokenRole){
                    return BadRequest("Invalid Tokens");
                }

                var bookEntity = _mapper.Map<Book>(book);
                _repository.Book.CreateBookWithDetails(bookEntity);
                _repository.Save();

                return Created("created",bookEntity);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody]BookDTO book)
        {
            try
            {

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.TokenValidation(identity);

                if(!rToken.success){
                    return BadRequest("Invalid Token");
                }

                if (book == null)
                {
                    return BadRequest("Book object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var tokenRole = rToken.result.IsAdmin;
                if(!tokenRole){
                    return BadRequest("Invalid Tokens");
                }

                var bookEntity = _repository.Book.GetBookById(id);
                if (bookEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(book, bookEntity);
                _repository.Book.UpdateAllBookAtributes(bookEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
                return StatusCode(500, "Internal server error " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var rToken = Jwt.TokenValidation(identity);

                if(!rToken.success){
                    return BadRequest("Invalid Token");
                }

                var tokenRole = rToken.result.IsAdmin;
                if(!tokenRole){
                    return BadRequest("Invalid Tokens");
                }

                var book = _repository.Book.GetBookById(id);
                if (book == null)
                {
                    return NotFound();
                }

                if (_repository.User.usersByBooks(id).Any()) 
                {   
                    Log.Error("Cannot delete book");
                    return BadRequest("Cannot delete book. It has related users. Delete those users first"); 
                }

                _repository.Book.DeleteBook(book);
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

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                return StatusCode(500, "Internal server error"); 
            } 
        }


        
        [HttpPost]
        public IActionResult Create([FromBody]BookForInsertDTO book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var bookEntity = _mapper.Map<Book>(book);
                _repository.Book.CreateBookWithDetails(bookEntity);
                _repository.Save();

                return Created("created",book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]BookDTO book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("Book object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
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
                return StatusCode(500, "Internal server error " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var book = _repository.Book.GetBookById(id);
                if (book == null)
                {
                    return NotFound();
                }

                if (_repository.User.usersByBooks(id).Any()) 
                {   
                    return BadRequest("Cannot delete book. It has related users. Delete those users first"); 
                }

                _repository.Book.DeleteBook(book);
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
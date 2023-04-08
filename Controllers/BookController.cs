
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
        public IActionResult Create([FromBody]Book book)
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

                _repository.Book.CreateBookWithDetails(book);
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
                _repository.Book.tuputamadre(bookEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex.Message);
            }
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Wrapper;

namespace tfg.Controllers.OrderController
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase 
    { 
        private IRepositoryWrapper _repository;     
        private IMapper _mapper;   
        public OrderController(IRepositoryWrapper repository,IMapper mapper) 
        { 
            _repository = repository;
             _mapper = mapper;

        }
        
        [HttpGet] 
        public IActionResult GetAll() 
        { 
            try 
            { 
                var orders = _repository.Order.GetAllOrders(); 
                return Ok(orders); 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}", Name = "OrderById")] 
        public IActionResult GetById(int id) 
        { 
            try 
            { 
                var order = _repository.Order.GetOrderById(id); 
                if (order == null) 
                { 
                    return NotFound(); 
                } 
                else 
                { 
                    return Ok(order); 
                } 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpPost]
        public IActionResult Create([FromBody]Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Order object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                _repository.Order.CreateOrder(order);
                _repository.Save();

                return Created("created",order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var order = _repository.Order.GetOrderById(id);
                if (order == null)
                {
                    return NotFound();
                }

                Console.WriteLine($"GRUCCCCCCCCCCCCI: {DateTime.Now.Subtract(order.Date)}");
                /*if (_repository.Book.booksByState(id).Any()) 
                {
                    return BadRequest("Cannot delete state. It has related books. Delete those books first"); 
                }*/

                /*_repository.Order.DeleteOrder(order);
                _repository.Save();*/

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
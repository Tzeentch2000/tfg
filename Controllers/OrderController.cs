
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
        [Authorize]
        public IActionResult GetAll() 
        { 
            try 
            { 
                var orders = _repository.Order.getAllOrdersWithDetails(); 
                return Ok(orders); 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpGet("{id}", Name = "OrderById")] 
        [Authorize]
        public IActionResult GetById(int id) 
        { 
            try 
            { 
                var order = _repository.Order.getOrderByIdWithDetails(id); 
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

        [HttpGet("/Order/UserId/{id}", Name = "OrderByUserId")] 
        [Authorize]
        public IActionResult GetByUserId(int id) 
        { 
            try 
            { 
                var orders = _repository.Order.GetOrderByUserId(id); 
                if (orders == null) 
                { 
                    return NotFound(); 
                } 
                else 
                { 
                    return Ok(orders); 
                } 
            } 
            catch (Exception ex) 
            { 
                return StatusCode(500, "Internal server error"); 
            } 
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateOrders([FromBody]IEnumerable<OrderForInsertDTO> orders)
        {
            try
            {
                if (orders == null)
                {
                    return BadRequest("Order object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var orderEntity = _mapper.Map<IEnumerable<Order>>(orders);
                _repository.Order.createOrders(orderEntity);
                _repository.Save();

                return Created("created",orderEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex.Message);
            }
        }

        /*[Route("Create")]
        public IActionResult Create([FromBody]OrderForInsertDTO order)
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

                var orderEntity = _mapper.Map<Order>(order);
                _repository.Order.CreateOrderWithDetails(orderEntity);
                _repository.Save();

                return Created("created",orderEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error " + ex.Message);
            }
        }*/

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

                var totalHours = DateTime.Now.Subtract(order.Date).TotalHours-2;
                if (totalHours>1) 
                {
                    return BadRequest("Cannot delete order. It's been over an hour. You can return it once it arrives following the instructions"); 
                }

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
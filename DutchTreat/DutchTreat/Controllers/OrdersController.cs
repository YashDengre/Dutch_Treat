using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]         //only authorize will give you 302
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchRepository repo, ILogger<OrdersController> logger, IMapper mapper
            ,UserManager<StoreUser> userManager )
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetOrders(bool includeItem = true)
        {
            try
            {
                //return Ok(_repo.GetAllOrders());
                //var result = _repo.GetAllOrders();
                // return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(result));    //automapper

                //include includeItem  for below
                var result = _repo.GetAllOrders(includeItem);
                return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(result));    //automapper

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get the orders : {ex}");
                return BadRequest("Failed to get the orders");
            }
        }

        [HttpGet("order-by-user")]
        public IActionResult GetOrdersForUser(bool includeItem = true)
        {
            try
            {
                //return Ok(_repo.GetAllOrders());
                //var result = _repo.GetAllOrders();
                // return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(result));    //automapper

                //include includeItem  for below
                var user = User.Identity.Name;
                var result = _repo.GetAllOrdersByUser(user, includeItem);

                return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(result));    //automapper

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get the orders : {ex}");
                return BadRequest("Failed to get the orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOrder(int id)
        {
            try
            {
                var order = _repo.GetOrderById(id);
                if (order != null)
                {
                    // return Ok(order);
                    return Ok(_mapper.Map<Order, OrderViewModel>(order)); //create the profile class 
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get the order for Id:{id} : {ex}");
                return BadRequest("Failed to get the order for Id:{id}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderViewModel orderDetails)
        {
            //we can use [FromQuery] [FromRoute] in function it self
            //for validting the model - we have created new View Model and replaced it in the request
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = new Order()
                    {
                        OrderDate = orderDetails.OrderDate,
                        OrderNumber = orderDetails.OrderNumber,
                        Id = orderDetails.OrderId
                    };

                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser =await  _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;

                    _repo.AddEntity(newOrder);
                    if (_repo.SaveAll())
                    {
                        var viewModel = new OrderViewModel()
                        {
                            OrderDate = newOrder.OrderDate,
                            OrderId = newOrder.Id,
                            OrderNumber = newOrder.OrderNumber
                        };
                        return Created($"/api/orders/{viewModel.OrderId}", viewModel);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to add the new order: {ex}");
            }
            return BadRequest($"Failed to add the new order.");
        }

        [HttpPost("create-order-auto-mapper")]
        public IActionResult AddOrderUsingAutoMapper([FromBody] OrderViewModel orderDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(orderDetails);


                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    _repo.AddEntity(newOrder);
                    if (_repo.SaveAll())
                    {
                        var viewModel = _mapper.Map<Order, OrderViewModel>(newOrder);
                        return Created($"/api/orders/{viewModel.OrderId}", viewModel);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to add the new order: {ex}");
            }
            return BadRequest($"Failed to add the new order.");
        }
    }
}

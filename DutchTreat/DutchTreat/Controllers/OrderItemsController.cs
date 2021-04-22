using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("/api/orders/{orderId}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository _repo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchRepository repo,
            ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        public IActionResult GetOrderItemById(int orderId)
        {
            var order = _repo.GetOrderById(orderId);
            if (order != null)
            {
                return Ok(_mapper.Map<IEnumerable<OrderItem>,
                      IEnumerable<OrderItemViewModel>>(order.Items));
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("orderitem-by-user")]
        public IActionResult GetOrderItemByIdForUser(int orderId)
        {
            var user = User.Identity.Name;
            var order = _repo.GetOrderByIdForUser(user, orderId);
            if (order != null)
            {
                return Ok(_mapper.Map<IEnumerable<OrderItem>,
                      IEnumerable<OrderItemViewModel>>(order.Items));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderItemByIdandItemId(int orderId, int id)
        {
            var order = _repo.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                    return Ok(_mapper.Map<OrderItem,
                          OrderItemViewModel>(item));
            }
            return NotFound();
        }

    }
}

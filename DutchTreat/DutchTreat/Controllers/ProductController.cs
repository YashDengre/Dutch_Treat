using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController] //specify that - it is a API controller- it helps us in swagger contract etc.
    [Produces("application/json")] //state that it returns only json
    public class ProductController : Controller // ,ControllerBase
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IDutchRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        //[HttpGet("rtrvp")]
        //[Route("products")]
        public IEnumerable<Product> GetProducts()
        {
            try
            {
                return _repository.GetProducts();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get the products: {ex}");
                return null;
            }
        }
        [HttpGet("byjsonresult")]
        public JsonResult GetProductsJsonResult()
        {
            try
            {
                return Json(_repository.GetProducts().Take(10));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get the products: {ex}");
                return Json("Bad Request");
            }
        }
        [HttpGet]
        [Route("by-iaction-result")]
        public IActionResult GetProductsIActionResult()
        {
            try
            {
                return Ok(_repository.GetProducts().Where(x=>x.Id>10).Take(4));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get the products: {ex}");
                return BadRequest("Failed to get the products");
            }
        }
        [HttpGet]
        [Route("by-action-result-swagger")]
        [ProducesResponseType(200,Type = typeof(List<Product>))]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> GetProductsActionResultForSwagger()
        {
            try
            {
                return Ok(_repository.GetProducts().Where(x => x.Id > 10).Take(4));

                //we can skip the Ok - directly return the list   but then we have to handle it for type conversion
                //either we need to return list and we have to change Ienumerable to list 
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get the products: {ex}");
                return BadRequest("Failed to get the products");
            }
        }

    }
}

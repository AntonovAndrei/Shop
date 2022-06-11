using Microsoft.AspNetCore.Mvc;
using Shop.API.HAL;
using Shop.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepositry;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepositry, ILogger<ProductsController> logger)
        {
            _productRepositry = productRepositry;
            _logger = logger;
        }

        const int PAGE_SIZE = 25;

        [HttpGet("{id}")]
        [Produces("application/hal+json")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _productRepositry.GetById(id);
                var resource = product.ToResource();
                resource._actions = new
                {
                    create = new
                    {
                        href = $"/api/products/{id}",
                        method = "POST",
                        name = $"Create product with {id} id"
                    },
                    update = new
                    {
                        href = $"/api/products/{id}",
                        method = "PUT",
                        name = $"Update product with {id} id"
                    },
                    delete = new
                    {
                        href = $"/api/products/{id}",
                        method = "DELETE",
                        name = $"Delete product with {id} id from the database"
                    }
                };
                return Ok(resource);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

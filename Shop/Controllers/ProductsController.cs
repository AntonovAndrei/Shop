using Microsoft.AspNetCore.Mvc;
using Shop.API.HAL;
using Shop.API.Models;
using Shop.Core.Entities;
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

        [HttpGet("getproductsbysaleid/{saleId}")]
        [Produces("application/hal+json")]
        public IActionResult GetProductsBySaleId(int saleId, int index = 0, int count = PAGE_SIZE)
        {
            try
            {
                var products = _productRepositry.GetProductBySaleId(saleId).Skip(index).Take(count)
                    .Select(v => v.ToResource());
                var total = _productRepositry.Count();
                var _links = ProductHAL.PaginateAsDynamic("/api/products", index, count, total);
                var result = new
                {
                    _links,
                    count,
                    total,
                    index,
                    products
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Produces("application/hal+json")]
        public IActionResult Get(int index = 0, int count = PAGE_SIZE)
        {
            var products = _productRepositry.GetAll().Skip(index).Take(count)
                .Select(v => v.ToResource());
            var total = _productRepositry.Count();
            var _links = ProductHAL.PaginateAsDynamic("/api/products", index, count, total);
            var result = new
            {
                _links,
                count,
                total,
                index,
                products
            };
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
            };
            try
            {
                _productRepositry.UpdateAsync(product);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto dto)
        {
            var existing = _productRepositry.GetById(dto.Id);
            if (existing != null)
                return Conflict($"Sorry, there is already a product with registration {dto.Id} in the database.");


            var product = new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
            };
            try
            {
                убрать из дто id 
                await _productRepositry.AddAsync(product);
                return Created($"/api/products/{product.Id}", product.ToResource());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _productRepositry.GetById(id);
                _productRepositry.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
    }
}

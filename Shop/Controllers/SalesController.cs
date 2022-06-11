using Microsoft.AspNetCore.Mvc;
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
    public class SalesController : ControllerBase
    {
        private readonly ISaleRepository _saleRepositry;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISaleRepository saleRepositry, ILogger<SalesController> logger)
        {
            _saleRepositry = saleRepositry;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var sale = _saleRepositry.GetById(id);
                return Ok(sale);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getsalesbyproductid/{productId}")]
        public IActionResult GetSalesByProductId(int productId)
        {
            try
            {
                var sale = _saleRepositry.GetByProductId(productId);
                return Ok(sale);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var sale = _saleRepositry.GetAll();
            if (sale == null) return NotFound();
            return Ok(sale);
        }
    }
}

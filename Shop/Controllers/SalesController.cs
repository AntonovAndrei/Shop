using Microsoft.AspNetCore.Mvc;
using Shop.API.HAL;
using Shop.Data.IRepositories;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleRepository _saleRepositry;
        private readonly ILogger<SalesController> _logger;
        const int PAGE_SIZE = 25;

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
        [Produces("application/hal+json")]
        public IActionResult Get(int index = 0, int count = PAGE_SIZE)
        {
            var products = _saleRepositry.GetAll().Skip(index).Take(count)
                .Select(v => v.ToResource());
            var total = _saleRepositry.Count();
            var _links = SaleHAL.PaginateAsDynamic("/api/sales", index, count, total);
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
    }
}

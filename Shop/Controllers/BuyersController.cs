using Microsoft.AspNetCore.Mvc;
using Shop.Data.IRepositories;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        private readonly IBuyerRepository _buyerRepositry;
        private readonly ILogger<BuyersController> _logger;

        public BuyersController(IBuyerRepository buyerRepositry, ILogger<BuyersController> logger)
        {
            _buyerRepositry = buyerRepositry;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var buyer = _buyerRepositry.GetById(id);
                return Ok(buyer);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

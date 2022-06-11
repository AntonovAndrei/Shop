using Microsoft.AspNetCore.Mvc;

namespace Auto.API.Controllers;

[Route("api")]
[ApiController]
public class DiscoveryEndpointController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() {
        var welcome = new {
            _links = new {
                products = new {
                    href = "/api/products"
                },
                sales = new
                {
                    href = "/api/sales"
                }
            },
            message = "Welcome to the Shop API!",
        };
        return Ok(welcome);
    }
}
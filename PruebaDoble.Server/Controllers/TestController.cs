using Microsoft.AspNetCore.Mvc;
using BlazorAppWithServer.Shared.Models;

namespace BlazorAppWithServer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("menu")]
        public ActionResult GetTestMenu()
        {
            var testMenu = new List<MenuItem>
            {
                new MenuItem { Name = "Pizza", Price = 100 },
                new MenuItem { Name = "Hamburguesa", Price = 150 },
                new MenuItem { Name = "Ensalada", Price = 80 }
            };
            
            return Ok(testMenu);
        }

        [HttpGet("orders/{tableId}")]
        public ActionResult GetTestOrders(int tableId)
        {
            var testOrders = new List<string>
            {
                $"Pedido 1 de mesa {tableId}",
                $"Pedido 2 de mesa {tableId}"
            };
            
            return Ok(testOrders);
        }

        [HttpPost("order")]
        public ActionResult AddTestOrder([FromBody] OrderRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be empty");

            return Ok(new { message = $"Pedido recibido: {request.Order} para la mesa {request.TableId}" });
        }

        [HttpGet("simple")]
        public IActionResult GetSimpleTest()
        {
            return Ok("Test exitoso");
        }
    }
}

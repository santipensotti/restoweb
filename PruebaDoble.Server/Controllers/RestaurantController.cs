using Microsoft.AspNetCore.Mvc;
using BlazorAppWithServer.Shared.Models;
using BlazorAppWithServer.Server.Data;

namespace BlazorAppWithServer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        [HttpGet("tables/{restaurantId}")]
        public ActionResult<int> GetTableCount(int restaurantId)
        {
            try
            {
                var count = RestaurantRepository.getCountTableFromRestaurant(restaurantId);
                return Ok(count);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{restaurantId}/addtable")]
        public IActionResult AddTable(int restaurantId)
        {
            try
            {
                RestaurantRepository.addTable(restaurantId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{restaurantId}/removetable")]
        public IActionResult RemoveTable(int restaurantId)
        {
            try
            {
                RestaurantRepository.substractTable(restaurantId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("menuitem")]
        public IActionResult AddMenuItem([FromBody] MenuItemRequest request)
        {
            try
            {
                RestaurantRepository.AddItemToMenu(
                    request.RestaurantId, 
                    request.Price, 
                    request.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("menuitem/{restaurantId}/{itemName}")]
        public IActionResult DeleteMenuItem(int restaurantId, string itemName)
        {
            try
            {
                var menu = RestaurantRepository.getMenuOfARestaurant(restaurantId);
                var item = menu.FirstOrDefault(i => i.Item1 == itemName);
                if (item != null)
                {
                    RestaurantRepository.DeleteItemFromMenu(
                        restaurantId,
                        item.Item2,
                        itemName);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
} 
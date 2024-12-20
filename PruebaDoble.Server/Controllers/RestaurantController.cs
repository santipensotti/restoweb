using Microsoft.AspNetCore.Mvc;
using BlazorAppWithServer.Shared.Models;
using BlazorAppWithServer.Server.Data;


namespace BlazorAppWithServer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {

        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost("create")]
        public IActionResult CreateRestaurant([FromBody] RestaurantRequest restaurantRequest)
        {
            try
            {
                _restaurantService.CreateRestaurant(restaurantRequest);
                return Ok(new { message = "Restaurant created successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear restaurante: {ex.Message}");
                return BadRequest(new { message = "Error creating restaurant", error = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateRestaurant(int id, [FromBody] RestaurantRequest restaurantRequest)
        {
            try
            {
                _restaurantService.UpdateRestaurant(id, restaurantRequest);
                return Ok(new { message = "Restaurant updated successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating restaurant", error = ex.Message });
            }
        }

        [HttpGet("all")]
        public IActionResult GetRestaurants()
        {
            try
            {
                var restaurants = _restaurantService.GetAllRestaurants();
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener restaurantes: {ex.Message}");
                return BadRequest(new { message = "Error fetching restaurants", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRestaurantById(int id)
        {
            
            try
            {
                var restaurantRequest = _restaurantService.GetRestaurantRequestById(id);

                return Ok(restaurantRequest);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

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
        [HttpGet("getMaps")]
        public ActionResult<List<RestaurantMapInfo>> GetLocations()
        {
            try
            {
                var restaurantInfos = _restaurantService.GetInfoRestaurantForMaps();
                return Ok(restaurantInfos);
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
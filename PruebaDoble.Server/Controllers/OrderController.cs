using Microsoft.AspNetCore.Mvc;
using BlazorAppWithServer.Shared.Models;
using BlazorAppWithServer.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAppWithServer.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] OrderRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Order))
                return BadRequest(new { message = "Request or order cannot be empty" });

            try
            {
                OrdersRepository.AddOrderForTable(request.TableId, request.Order);
                MessagesRepository.AddMessage($"Mesa {request.TableId}: {request.Order}");
                return Ok(new { message = "Order sent successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al procesar el pedido: {ex.Message}" });
            }
        }

        [HttpPost("createRestaurant")]
        public IActionResult CreateRestaurant(int countTables)
        {
            if (countTables <= 0)
                return BadRequest(new { message = "Table count must be greater than 0" });

            RestaurantRepository.AddRestaurant(countTables);
            return Ok(new { message = "Restaurant created successfully" });
        }

        [HttpPost("restaurant/menuitem")]
        public IActionResult AddMenuItem([FromBody] MenuItemRequest request)
        {
            if (request.Price <= 0 || string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new { message = "Invalid price or item name" });

            try
            {
                RestaurantRepository.AddItemToMenu(request.RestaurantId, request.Price, request.Name);
                return Ok(new { message = "Menu item added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("restaurant/menuitem/{restaurantId}/{itemName}")]
        public IActionResult DeleteMenuItem(int restaurantId, string itemName)
        {
            try
            {
                var menu = RestaurantRepository.getMenuOfARestaurant(restaurantId);
                var item = menu.FirstOrDefault(i => i.Item1 == itemName);
                if (item != null)
                {
                    RestaurantRepository.DeleteItemFromMenu(restaurantId, item.Item2, itemName);
                    return Ok(new { message = "Menu item deleted successfully" });
                }
                return NotFound(new { message = "Item not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("restaurant/{restaurantId}/addtable")]
        public IActionResult AddTable(int restaurantId)
        {
            if (restaurantId < 0)
                return BadRequest(new { message = "Invalid restaurant ID" });

            try
            {
                RestaurantRepository.addTable(restaurantId);
                return Ok(new { message = "Table added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("restaurant/{restaurantId}/removetable")]
        public IActionResult RemoveTable(int restaurantId)
        {
            if (restaurantId < 0)
                return BadRequest(new { message = "Invalid restaurant ID" });

            try
            {
                RestaurantRepository.substractTable(restaurantId);
                return Ok(new { message = "Table removed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("deliver")]
        public IActionResult DeliverOrder([FromBody] OrderDeliveryRequest request)
        {
            try
            {
                OrdersRepository.DeliverOrder(request.TableId, request.Order);
                return Ok(new { message = $"Pedido {request.Order} entregado a la mesa {request.TableId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("messages/{tableId}")]
        public ActionResult<List<string>> GetTableOrders(int tableId)
        {
            if (tableId < 0)
                return BadRequest(new { message = "Invalid table ID" });

            try
            {
                var orders = OrdersRepository.GetTableOrders(tableId) ?? new List<string>();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error fetching table orders: {ex.Message}" });
            }
        }

      [HttpGet("menu/{restaurantId}")]
        public ActionResult<List<MenuItem>> GetRestaurantMenu(int restaurantId)
        {
            if (restaurantId < 0)
                return BadRequest(new { message = "Invalid restaurant ID" });

            try
            {
                var menu = RestaurantRepository.getMenuOfARestaurant(restaurantId);
                if (menu == null || !menu.Any())
                {
                    RestaurantRepository.AddItemToMenu(restaurantId, 100, "Pizza");
                    RestaurantRepository.AddItemToMenu(restaurantId, 150, "Hamburguesa");
                    RestaurantRepository.AddItemToMenu(restaurantId, 80, "Ensalada");
                    menu = RestaurantRepository.getMenuOfARestaurant(restaurantId);
                }
                return Ok(menu.Select(item => new MenuItem { Name = item.Item1, Price = item.Item2 }).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error fetching menu: {ex.Message}" });
            }
        }

        [HttpGet("restaurant/tables/{restaurantId}")]
        public ActionResult<int> GetTableCount(int restaurantId)
        {
            if (restaurantId < 0)
                return BadRequest(new { message = "Invalid restaurant ID" });

            try
            {
                int tableCount = RestaurantRepository.getCountTableFromRestaurant(restaurantId);
                return Ok(tableCount);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error fetching table count: {ex.Message}" });
            }
        }
    }

    public class MenuItemRequest
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}

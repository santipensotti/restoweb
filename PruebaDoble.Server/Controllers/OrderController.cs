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
        // Endpoint para recibir un nuevo pedido y agregarlo a la mesa
        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] OrderRequest request)
        {
            if (request == null || request.MenuItem == null)
                return BadRequest(new { message = "Request or menu item cannot be empty" });

            try
            {
                // Crear la nueva orden con los detalles proporcionados
                var order = new Order
                {
                    OrderId = new Random().Next(1, 10000), // Generar un ID de orden aleatorio, se puede mejorar
                    MenuItem = request.MenuItem,
                    State = OrderState.Pending
                };

                // Agregar la orden a la mesa correspondiente
                OrdersRepository.AddOrderForTable(request.RestoId, request.TableId, order);

                // Enviar un mensaje (opcional)
                MessagesRepository.AddMessage($"Mesa {request.TableId}: {request.MenuItem.Name}");

                return Ok(new { message = "Order sent successfully", orderId = order.OrderId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al procesar el pedido: {ex.Message}" });
            }
        }

        // Endpoint para marcar un pedido como entregado
        [HttpPost("deliver")]
        public IActionResult DeliverOrder([FromBody] OrderDeliveryRequest request)
        {
            try
            {
                // Buscar la orden y actualizar su estado
                OrdersRepository.UpdateOrderState(request.RestoId, request.TableId, request.OrderId, OrderState.Delivered);
                
                return Ok(new { message = $"Pedido {request.OrderId} entregado a la mesa {request.TableId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Endpoint para obtener los pedidos de una mesa
        [HttpGet("tableOrders/{restoId}/{tableId}")]
        public ActionResult<List<Order>> GetTableOrders(int restoId, int tableId)
        {
            if (tableId < 0)
                return BadRequest(new { message = "Invalid table ID" });

            try
            {
                // Obtener las órdenes de la mesa
                var orders = OrdersRepository.GetTableOrders(restoId, tableId) ?? new List<Order>();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error fetching table orders: {ex.Message}" });
            }
        }

        // Endpoint para obtener todos los pedidos de un restaurante
        [HttpGet("tableOrders/{restoId}")]
        public ActionResult<Dictionary<int, List<Order>>> GetOrdersByRestaurantId(int restoId)
        {
            if (restoId < 0)
                return BadRequest(new { message = "Invalid resto ID" });

            try
            {
                // Obtener las órdenes del restaurante
                var orders = OrdersRepository.GetOrdersByRestaurantId(restoId);

                // Si no hay órdenes, devolver un diccionario vacío
                if (orders == null || orders.Count == 0)
                {
                    return Ok(new Dictionary<int, List<Order>>());
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error fetching table orders: {ex.Message}" });
            }
        }

        // Endpoint para obtener el menú de un restaurante
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
                    // Si el menú está vacío, agregar algunos ítems predeterminados
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


        // Endpoint para actualizar el estado de una orden
        [HttpPost("updateState")]
        public IActionResult UpdateOrderState([FromBody] OrderDeliveryRequest request)
        {
            try
            {
                // Actualizar el estado de la orden
                OrdersRepository.UpdateOrderState(request.RestoId, request.TableId, request.OrderId, request.NewState);

                return Ok(new { message = $"Estado de la orden {request.OrderId} actualizado a {request.NewState} en la mesa {request.TableId}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al actualizar el estado: {ex.Message}" });
            }
        }


        [HttpDelete("{restoId}/{tableId}/{orderId}")]
        public IActionResult DeleteOrder(int restoId, int tableId, int orderId)
        {
            try
            {
                var success = OrdersRepository.DeliverOrder(restoId, tableId, orderId);
                if (success)
                {
                    return Ok(new { message = "Orden eliminada exitosamente" });
                }
                else
                {
                    return NotFound(new { message = "Orden no encontrada" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al eliminar la orden: {ex.Message}" });
            }
        }
    }
}


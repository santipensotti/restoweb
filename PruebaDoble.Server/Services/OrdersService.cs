using BlazorAppWithServer.Server.Data;
using BlazorAppWithServer.Shared.Models;
using System;
using System.Collections.Generic;

public class OrdersService
{
    int _orderCounter = 0;
    // Lógica para crear una nueva orden
    public void CreateOrder(int restoId, int tableId, OrderRequest request)
    {
        var order = new Order
        {
            OrderId = _orderCounter++, // Generar un ID de orden aleatorio
            MenuItem = request.MenuItem,
            State = OrderState.Pending
        };

        // Agregar la orden a la mesa correspondiente
        OrdersRepository.AddOrderForTable(restoId, tableId, order);
    }

    // Lógica para actualizar el estado de una orden
    public void UpdateOrderState(int restoId, int tableId, int orderId, OrderState newState)
    {
        OrdersRepository.UpdateOrderState(restoId, tableId, orderId, newState);
    }

    // Lógica para obtener los pedidos de una mesa
    public List<Order> GetOrdersForTable(int restoId, int tableId)
    {
        return OrdersRepository.GetTableOrders(restoId, tableId) ?? new List<Order>();
    }

    // Lógica para obtener todos los pedidos de un restaurante
    public Dictionary<int, List<Order>> GetOrdersByRestaurantId(int restoId)
    {
        return OrdersRepository.GetOrdersByRestaurantId(restoId);
    }

    // Lógica para eliminar una orden
    public bool DeleteOrder(int restoId, int tableId, int orderId)
    {
        return OrdersRepository.DeliverOrder(restoId, tableId, orderId);
    }

    // Lógica para obtener el menú de un restaurante
    public List<MenuItem> GetRestaurantMenu(int restaurantId)
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

        return menu.Select(item => new MenuItem { Name = item.Item1, Price = item.Item2 }).ToList();
    }
}

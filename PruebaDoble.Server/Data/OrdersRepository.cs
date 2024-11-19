using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorAppWithServer.Shared.Models;


namespace BlazorAppWithServer.Server.Data
{
    public static class OrdersRepository
    {
        // Diccionario para almacenar las órdenes por restaurante y mesa.
        // La clave es (restaurantId, tableId) y el valor es una lista de órdenes (Order).
        private static Dictionary<(int restaurantId, int tableId), List<Order>> _tableOrders 
            = new Dictionary<(int, int), List<Order>>();

        // Agregar un pedido a una mesa
        public static void AddOrderForTable(int restaurantId, int tableId, Order order)
        {
            var key = (restaurantId, tableId);
            if (!_tableOrders.ContainsKey(key))
            {
                _tableOrders[key] = new List<Order>();
            }
            _tableOrders[key].Add(order);
        }

        // Marcar un pedido como entregado (removerlo de la lista)
        public static bool DeliverOrder(int restaurantId, int tableId, int orderId)
        {
            var key = (restaurantId, tableId);
            if (_tableOrders.ContainsKey(key))
            {
                var order = _tableOrders[key].FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    _tableOrders[key].Remove(order);
                    return true;
                }
                return false;
            }
            return false;
        }

        // Obtener los pedidos de una mesa
        public static List<Order> GetTableOrders(int restaurantId, int tableId)
        {
            var key = (restaurantId, tableId);
            return _tableOrders.ContainsKey(key) ? _tableOrders[key] : new List<Order>();
        }

        // Obtener los pedidos de un restaurante por ID
        public static Dictionary<int, List<Order>> GetOrdersByRestaurantId(int restaurantId)
        {
            var result = new Dictionary<int, List<Order>>();

            foreach (var entry in _tableOrders)
            {
                if (entry.Key.restaurantId == restaurantId)
                {
                    if (!result.ContainsKey(entry.Key.tableId))
                    {
                        result[entry.Key.tableId] = new List<Order>();
                    }
                    result[entry.Key.tableId].AddRange(entry.Value);
                }
            }

            return result;
        }

        // Cambiar el estado de una orden
        public static void UpdateOrderState(int restaurantId, int tableId, int orderId, OrderState newState)
        {
            var key = (restaurantId, tableId);
            if (_tableOrders.ContainsKey(key))
            {
                var order = _tableOrders[key].FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.State = newState;
                }
            }
        }
    }
}

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
        private static Dictionary<(int restaurantId, int tableId), List<Order>> _tableOrders 
            = new Dictionary<(int, int), List<Order>>();

        public static void AddOrderForTable(int restaurantId, int tableId, Order order)
        {
            var key = (restaurantId, tableId);
            if (!_tableOrders.ContainsKey(key))
            {
                _tableOrders[key] = new List<Order>();
            }
            _tableOrders[key].Add(order);
        }

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

        public static List<Order> GetTableOrders(int restaurantId, int tableId)
        {
            var key = (restaurantId, tableId);
            return _tableOrders.ContainsKey(key) ? _tableOrders[key] : new List<Order>();
        }

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

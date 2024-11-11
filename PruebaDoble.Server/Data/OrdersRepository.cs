namespace BlazorAppWithServer.Server.Data;

public static class OrdersRepository
{
    private static Dictionary<int, List<string>> _tableOrders = new Dictionary<int, List<string>>();

    public static void AddOrderForTable(int tableId, string order)
    {
        if (!_tableOrders.ContainsKey(tableId))
        {
            _tableOrders[tableId] = new List<string>();
        }
        _tableOrders[tableId].Add(order);
    }

    public static void DeliverOrder(int tableId, string order)
    {
        if (_tableOrders.ContainsKey(tableId))
        {
            _tableOrders[tableId].Remove(order);
        }
    }

    public static List<string> GetTableOrders(int tableId)
    {
        return _tableOrders.ContainsKey(tableId) ? _tableOrders[tableId] : new List<string>();
    }
} 
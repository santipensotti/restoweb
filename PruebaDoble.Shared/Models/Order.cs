namespace BlazorAppWithServer.Shared.Models
{
    public enum OrderState
    {
        Pending,    // Pendiente
        Cooking,    // Cocinando
        Delivered   // Entregado
    }

    public class Order
    {
        public int OrderId { get; set; }      // Identificador único de la orden
        public MenuItem MenuItem { get; set; } // El menú asociado a la orden
        public OrderState State { get; set; }  // El estado de la orden
    }
}

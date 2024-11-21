namespace BlazorAppWithServer.Shared.Models
{
    public enum OrderState
    {
        Pending,    
        Cooking,    
        Delivered   
    }

    public class Order
    {
        public int OrderId { get; set; }      
        public MenuItem MenuItem { get; set; }
        public OrderState State { get; set; } 
    }
}

public class PlaceOrderRequest
{
    public List<int> CartItemIds { get; set; } = new List<int>();

    public string ShippingFirstName { get; set; }
    public string ShippingLastName { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingPostalCode { get; set; }
    
}
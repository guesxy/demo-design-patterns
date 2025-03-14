namespace MediatorDemo.Colleagues;

using Mediator;

public class OrderFulfillment : IColleague
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, string> _orderStatuses = new();

    public OrderFulfillment(IMediator mediator)
    {
        _mediator = mediator;
        
        // Register for message types this colleague is interested in
        _mediator.Register("OrderStatusRequest", this);
        _mediator.Register("PaymentStatus", this);
        
        // Initialize some orders for demo
        _orderStatuses["ORD-1001"] = "Processing";
        _orderStatuses["ORD-1002"] = "Shipped";
        _orderStatuses["ORD-1003"] = "Delivered";
    }

    public void SendMessage(string messageType, string message)
    {
        Console.WriteLine($"Order Fulfillment sending message: {messageType} - {message}");
        _mediator.Send(messageType, message);
    }

    public void ReceiveMessage(string messageType, string message)
    {
        Console.WriteLine($"Order Fulfillment received {messageType}: {message}");
        
        // Handle order status requests
        if (messageType == "OrderStatusRequest")
        {
            string orderId = message.Split(' ').Last();
            if (_orderStatuses.TryGetValue(orderId, out string status))
            {
                SendMessage("OrderStatus", $"Order {orderId} is: {status}");
            }
            else
            {
                SendMessage("OrderStatus", $"Order {orderId} not found");
            }
        }
        
        // Process an order once payment is confirmed
        else if (messageType == "PaymentStatus" && message.Contains("Successful"))
        {
            string orderId = message.Split(' ')[1];
            Console.WriteLine($"Order Fulfillment starting to process order {orderId}");
            _orderStatuses[orderId] = "Processing";
            SendMessage("OrderStatus", $"Order {orderId} is now processing");
        }
    }

    public void ShipOrder(string orderId)
    {
        if (_orderStatuses.TryGetValue(orderId, out _))
        {
            _orderStatuses[orderId] = "Shipped";
            SendMessage("OrderStatus", $"Order {orderId} has been Shipped");
        }
    }
}
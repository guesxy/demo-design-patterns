namespace MediatorDemo.Colleagues;

using Mediator;

public class CustomerService : IColleague
{
    private readonly IMediator _mediator;
    private readonly string _name;

    public CustomerService(IMediator mediator, string name)
    {
        _mediator = mediator;
        _name = name;
        
        // Register for message types this colleague is interested in
        _mediator.Register("OrderStatus", this);
        _mediator.Register("PaymentStatus", this);
    }

    public void SendMessage(string messageType, string message)
    {
        Console.WriteLine($"Customer Service ({_name}) sending message: {messageType} - {message}");
        _mediator.Send(messageType, message);
    }

    public void ReceiveMessage(string messageType, string message)
    {
        Console.WriteLine($"Customer Service ({_name}) received {messageType}: {message}");
        
        // Respond to different message types
        if (messageType == "OrderStatus" && message.Contains("Shipped"))
        {
            Console.WriteLine($"Customer Service ({_name}) is notifying customer about shipment");
        }
    }

    public void RequestOrderStatus(string orderId)
    {
        SendMessage("OrderStatusRequest", $"Need status update for order {orderId}");
    }
}
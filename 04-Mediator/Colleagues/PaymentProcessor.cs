namespace MediatorDemo.Colleagues;

using Mediator;

public class PaymentProcessor : IColleague
{
    private readonly IMediator _mediator;

    public PaymentProcessor(IMediator mediator)
    {
        _mediator = mediator;
        
        // Register for message types this colleague is interested in
        _mediator.Register("PaymentRequest", this);
    }

    public void SendMessage(string messageType, string message)
    {
        Console.WriteLine($"Payment Processor sending message: {messageType} - {message}");
        _mediator.Send(messageType, message);
    }

    public void ReceiveMessage(string messageType, string message)
    {
        Console.WriteLine($"Payment Processor received {messageType}: {message}");
        
        // Process payment requests
        if (messageType == "PaymentRequest")
        {
            string[] parts = message.Split(' ');
            string orderId = parts[1];

            Console.WriteLine(parts[3]);
            decimal amount = decimal.Parse(parts[3]);
            
            // Simulate payment processing
            bool paymentSuccessful = amount < 1000; // Simple rule for demo
            
            if (paymentSuccessful)
            {
                SendMessage("PaymentStatus", $"Payment Successful for {orderId}");
            }
            else
            {
                SendMessage("PaymentStatus", $"Payment Failed for {orderId} - Amount too high");
            }
        }
    }

    public void ProcessPayment(string orderId, decimal amount)
    {
        SendMessage("PaymentRequest", $"Process {orderId} for {amount} USD");
    }
}
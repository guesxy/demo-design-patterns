using MediatorDemo.Colleagues;
using MediatorDemo.Mediator;

Console.WriteLine("Mediator Pattern Demo\n");

IMediator mediator = new ConcreteMediator();

var customerService = new CustomerService(mediator, "CS-Team-1");
var orderFulfillment = new OrderFulfillment(mediator);
var paymentProcessor = new PaymentProcessor(mediator);

// Simulate a customer service representative checking order status
Console.WriteLine("\n--- Scenario 1: Customer inquires about an order ---");
customerService.RequestOrderStatus("ORD-1001");

// Simulate payment processing and order fulfillment
Console.WriteLine("\n--- Scenario 2: Process a new order ---");
paymentProcessor.ProcessPayment("ORD-1004", 499.99m);

// Simulate shipping an order
Console.WriteLine("\n--- Scenario 3: Ship an order ---");
orderFulfillment.ShipOrder("ORD-1001");

// Simulate a payment decline
Console.WriteLine("\n--- Scenario 4: Payment declined ---");
paymentProcessor.ProcessPayment("ORD-1005", 1500.00m);

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
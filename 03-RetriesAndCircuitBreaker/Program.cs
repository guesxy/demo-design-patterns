
using ResilienceDemo.Services;

Console.WriteLine("Retries and Circuit Breaker Pattern Demo\n");

IWeatherService unreliableService = new UnreliableWeatherService();
IWeatherService resilientService = new ResilientWeatherService(unreliableService);

for (int i = 0; i < 10; i++)
{
    try
    {
        Console.WriteLine($"\nAttempt #{i+1}:");
        string result = await resilientService.GetWeatherForecastAsync("New York");
        Console.WriteLine($"Success: {result}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed completely: {ex.Message}");
    }
    
    await Task.Delay(1000);
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
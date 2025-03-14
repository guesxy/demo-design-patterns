using System;

namespace ResilienceDemo.Services;

public class UnreliableWeatherService : IWeatherService
{
    private readonly Random _random = new Random();
    private int _failureCount = 0;
    
    public async Task<string> GetWeatherForecastAsync(string city)
    {
        // Simulate network delay
        await Task.Delay(100);
        
        int randomValue = _random.Next(1, 10);
        
        // 60% chance of failure on the first few calls
        if (randomValue <= 6 && _failureCount < 3)
        {
            _failureCount++;
            Console.WriteLine($"Service failed! Attempt #{_failureCount}");
            throw new Exception($"Failed to fetch weather data for {city}. Server is busy.");
        }
        
        _failureCount = 0;
        
        string[] weatherConditions = { "Sunny", "Cloudy", "Rainy", "Windy", "Snowy" };
        int temperature = _random.Next(-10, 35);
        
        return $"Weather for {city}: {weatherConditions[_random.Next(weatherConditions.Length)]}, {temperature}°C";
    }
}

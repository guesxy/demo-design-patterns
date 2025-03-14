using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace ResilienceDemo.Services;

public class ResilientWeatherService : IWeatherService
{
    private readonly IWeatherService _weatherService;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
    private readonly AsyncPolicy _resiliencePolicy;
    
    public ResilientWeatherService(IWeatherService weatherService)
    {
        _weatherService = weatherService;
        
        // Retry 3 times with exponential backoff
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                3, 
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry #{retryCount} after {timeSpan.TotalSeconds} seconds due to: {exception.Message}");
                });
        
        // Break the circuit after 5 failures, reset after 10 seconds
        _circuitBreakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(
                5,
                TimeSpan.FromSeconds(10),
                onBreak: (exception, timeSpan) =>
                {
                    Console.WriteLine($"Circuit broken for {timeSpan.TotalSeconds} seconds due to: {exception.Message}");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit reset, calls are now allowed again");
                },
                onHalfOpen: () =>
                {
                    Console.WriteLine("Circuit is half-open, next call is a trial");
                });
        
        // Combine policies: first retry, then circuit breaker
        _resiliencePolicy = _retryPolicy.WrapAsync(_circuitBreakerPolicy);
    }

    public async Task<string> GetWeatherForecastAsync(string city) => 
        await _resiliencePolicy.ExecuteAsync(async () => await _weatherService.GetWeatherForecastAsync(city));
}

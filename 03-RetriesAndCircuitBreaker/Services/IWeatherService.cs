using System;

namespace ResilienceDemo.Services;

public interface IWeatherService
{
    Task<string> GetWeatherForecastAsync(string city);
}

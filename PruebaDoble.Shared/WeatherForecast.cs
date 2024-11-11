namespace BlazorAppWithServer.Shared
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }

        // Propiedad calculada para la temperatura en Fahrenheit
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}

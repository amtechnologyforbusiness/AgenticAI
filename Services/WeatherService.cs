using Agentic.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Agentic.Services
{
    public class WeatherService
    {

        private readonly HttpClient _httpClient;
        //private readonly MyDbContext _dbContext;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_dbContext = dbContext;
        }

        public async Task<dynamic> GetWeatherData(string city)
        {
            // Specify coordinates for the city (you can modify this to dynamically get coordinates based on city name)
            double latitude = 52.52;  // Example latitude for Berlin
            double longitude = 13.405; // Example longitude for Berlin

            // Build the URL for the Open-Meteo API
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";

            // Make the HTTP GET request
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful

            // Parse the weather data
            var weatherData = await response.Content.ReadAsStringAsync();

            // Deserialize the weather data to access specific fields (like temperature, condition)
            var weatherJson = JObject.Parse(weatherData);
            var temperature = weatherJson["current_weather"]?["temperature"].Value<float>();
            var condition = "Unknown"; // You can enhance the condition parsing if the API provides a condition description

            // Format the weather log as per your request
            string logData = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} --------------------------------------------------------------\n";
            logData += $"Temperature: {temperature} °C, Condition: {condition}\n";
            logData += $"--------------------------------------------------------------\n";

            // Specify the log folder path (make sure it exists or create it)
            string logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "weatherlog");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            // Generate the log file path with the city name and current date
            string logFilePath = Path.Combine(logFolderPath, $"{city}_WeatherLog_{DateTime.UtcNow:yyyyMMdd}.txt");

            // Append the log data to the file
            await File.AppendAllTextAsync(logFilePath, logData);

            // Optionally, you can save the weather data to the database as you were doing earlier

            // Return the weather data as a string (or object as needed)
            return weatherData;
        }
    }
}

using Agentic.Services;
using System.Threading.Tasks;

namespace Agentic.WeatherTask
{
    public class WeatherTask
    {
        private readonly WeatherService _weatherService;

        public WeatherTask(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        // Define a method that Hangfire will call for the recurring task
        public async Task FetchWeatherData()
        {
            // Array of USA states (you can add more states as needed)
            string[] usaStates = new string[]
            {
        "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut",
        "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa",
        "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan",
        "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire",
        "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio",
        "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota",
        "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia",
        "Wisconsin", "Wyoming"
            };

            // Loop through each state and fetch weather data
            foreach (var state in usaStates)
            {
                // Call the WeatherService to get the weather data for each state
                var weatherData = await _weatherService.GetWeatherData(state);

                // Process the weather data, for example, log it
                Console.WriteLine($"Weather data fetched for {state}: {weatherData}");

                // Optionally, you can log the data to a file or database here
            }


            // Process the weather data, for example, log it
            //Console.WriteLine($"Weather data fetched: {weatherData}");

            // Since this is async, we don't need to return anything
            // It is enough that it returns Task (implicitly).
        }
    }
}
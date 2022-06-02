using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;

namespace Tests
{
    public class ComponentTests
    {
        [Fact, Trait("InMemory", "yes")]
        public async Task Some_Random_Test()
        {
            var webAppFactory = new WebApplicationFactory<WebAPI.Program>();
            var httpClient = webAppFactory.WithWebHostBuilder(b => { b.ConfigureServices(x => { }); }).CreateDefaultClient();

            /*
            var response = await httpClient.GetAsync("/WeatherForecast");
            var result = await response.Content.ReadAsStringAsync();

            result.Should().Contain("temperatureC");
            */
        }
    }
}
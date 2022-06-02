using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ComponentTests
    {
        [Fact]
        public async Task Some_Random_Test()
        {
            var webAppFactory = new WebApplicationFactory<WebAPI.Program>();
            var httpClient = webAppFactory.WithWebHostBuilder(b => { b.ConfigureServices(x => { }); }).CreateDefaultClient();

            var response = await httpClient.GetAsync("/WeatherForecast");
            var result = await response.Content.ReadAsStringAsync();

            result.Should().Contain("temperatureC");
        }
    }
}
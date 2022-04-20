using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Tests
{
    public class ComponentTests
    {
        [Fact]
        public async Task Some_Random_Test()
        {
            var webAppFactory = new WebApplicationFactory<WebAPI.Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync("/WeatherForecast");
            var result = await response.Content.ReadAsStringAsync();

            result.Should().Contain("temperatureC");
        }
    }
}
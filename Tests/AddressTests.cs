using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Domain;

namespace Tests
{
    public class AddressTests
    {
        public AddressTests()
        {

        }

        [Theory]
        [InlineData("GBR", "GB", "United Kingdom")]
        public void When_3Digit_Address_Used_Resolves_Object_Properties(string countryCode, string expected2Digit, string expectedName)
        {
            // Arrange
            CountryVM resolvedObject;

            // Act
            resolvedObject = new CountryVM(countryCode);

            // Assert
            resolvedObject.Should().NotBeNull();
            resolvedObject.CountryCode2.Should().Be(expected2Digit);
            resolvedObject.CountryName.Should().Be(expectedName);
        }

        [Fact]
        public void When_3Digit_Address_Doesnt_Exist()
        {
            // Arrange
            CountryVM resolvedObject;
            string fakeCode = "Fake";

            // Act
            resolvedObject = new CountryVM(fakeCode);

            // Assert
            resolvedObject.Should().NotBeNull();
            resolvedObject.CountryCode.Should().Be(fakeCode);
            resolvedObject.CountryCode2.Should().BeEmpty();
            resolvedObject.CountryName.Should().BeEmpty();
        }
    }
}

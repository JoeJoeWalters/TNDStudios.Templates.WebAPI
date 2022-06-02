using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Tests
{
    public class AddressTests
    {
        public AddressTests()
        {

        }

        [Theory, Trait("InMemory", "yes")]
        [InlineData()]
        public void When_Address_Cast_To_AddressVM() 
        {
            // Arrange
            string expected2DigitCode = "GB";
            string expectedCountryName = "United Kingdom";
            Address address = 
                new Address() 
                {
                    Id = Guid.NewGuid().ToString(),
                    Addressee = "Addressee",
                    AddressLine1 = "AddressLine1",
                    AddressLine2 = "AddressLine2",
                    AddressLine3 = "AddressLine3",
                    AddressLine4 = "AddressLine4",
                    CountryCode = "GBR",
                    CountyState = "CountyState",
                    PostalCode = "PS12BBB"
                };

            // Act
            AddressVM result = new AddressVM(address);

            // Assert
            result.Id.Should().Be(address.Id);
            result.Addressee.Should().Be(address.Addressee);
            result.AddressLine1.Should().Be(address.AddressLine1);
            result.AddressLine2.Should().Be(address.AddressLine2);
            result.AddressLine3.Should().Be(address.AddressLine3);
            result.AddressLine4.Should().Be(address.AddressLine4);
            result.Country.Should().NotBeNull();
            result.Country.CountryCode.Should().Be(address.CountryCode);
            result.Country.CountryCode2.Should().Be(expected2DigitCode);
            result.Country.CountryName.Should().Be(expectedCountryName);
            result.CountyState.Should().Be(address.CountyState);
            result.PostalCode.Should().Be(address.PostalCode);
        }

        [Theory, Trait("InMemory", "yes")]
        [InlineData("GBR", "GB", "United Kingdom")]
        public void When_3Digit_Address_Exists(string countryCode, string expected2Digit, string expectedName)
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

        [Fact, Trait("InMemory", "yes")]
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

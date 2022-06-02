using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Tests
{
    public class ContactTests
    {
        public ContactTests()
        {

        }

        [Fact, Trait("InMemory", "yes")]
        public void When_Contact_Cast_To_ContactVM()
        {
            // Arrange
            string expected2DigitCode = "GB";
            string expectedCountryName = "United Kingdom";
            Contact contact =
                new Contact()
                {
                    Forename = "Forename",
                    Surname = "Surname",
                    Middlenames = "Middlenames",
                    Title = "Mr",
                    Address = new Address()
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
                    }
                };

            // Act
            ContactVM result = new ContactVM(contact);

            // Assert
            result.Address.Id.Should().Be(contact.Address.Id);
            result.Address.Addressee.Should().Be(contact.Address.Addressee);
            result.Address.AddressLine1.Should().Be(contact.Address.AddressLine1);
            result.Address.AddressLine2.Should().Be(contact.Address.AddressLine2);
            result.Address.AddressLine3.Should().Be(contact.Address.AddressLine3);
            result.Address.AddressLine4.Should().Be(contact.Address.AddressLine4);
            result.Address.Country.Should().NotBeNull();
            result.Address.Country.CountryCode.Should().Be(contact.Address.CountryCode);
            result.Address.Country.CountryCode2.Should().Be(expected2DigitCode);
            result.Address.Country.CountryName.Should().Be(expectedCountryName);
            result.Address.CountyState.Should().Be(contact.Address.CountyState);
            result.Address.PostalCode.Should().Be(contact.Address.PostalCode);
        }

    }
}

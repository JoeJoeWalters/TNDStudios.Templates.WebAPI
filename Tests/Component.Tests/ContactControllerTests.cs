using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using WebAPI.Contracts;
using WebAPI.Controllers;

namespace Component.Tests
{
    public class ContactControllerTests
    {
        [Fact, Trait("InMemory", "yes")]
        public void When_Contact_Is_Removed()
        {
            // Arrange
            ILogger<IContactController> logger = new NullLoggerFactory().CreateLogger<IContactController>();
            IContactController contactController = new ContactController(logger);
            string contactId = Guid.NewGuid().ToString();
            Contact contact = new Contact() { Id = contactId };
            IActionResult deleteResult, addResult;

            // Act
            addResult = contactController.Add(contact);
            deleteResult = contactController.Delete(contact);

            // Assert
            addResult.Should().NotBeNull();
            addResult.GetType().Should().Be(typeof(OkObjectResult));
            deleteResult.Should().NotBeNull();
            deleteResult.GetType().Should().Be(typeof(OkResult));
        }

        [Fact, Trait("InMemory", "yes")]
        public void When_Contact_Is_Added()
        {
            // Arrange
            ILogger<IContactController> logger = new NullLoggerFactory().CreateLogger<IContactController>();
            IContactController contactController = new ContactController(logger);
            string contactId = Guid.NewGuid().ToString();
            Contact contact = new Contact() { Id = contactId };
            IActionResult addResult;

            // Act
            addResult = contactController.Add(contact);

            // Assert
            addResult.Should().NotBeNull();
            addResult.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact, Trait("InMemory", "yes")]
        public void When_Contacts_Are_Queried()
        {
            // Arrange
            ILogger<IContactController> logger = new NullLoggerFactory().CreateLogger<IContactController>();
            IContactController contactController = new ContactController(logger);
            string contactId = Guid.NewGuid().ToString();
            Contact contact = new Contact() { Id = contactId };
            IActionResult queryResult, addResult;

            // Act
            addResult = contactController.Add(contact);
            queryResult = contactController.QueryContacts();

            // Assert
            addResult.Should().NotBeNull();
            addResult.GetType().Should().Be(typeof(OkObjectResult));
            queryResult.Should().NotBeNull();
            queryResult.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact, Trait("InMemory", "yes")]
        public void When_Contact_Is_Merged()
        {
            // Arrange
            ILogger<IContactController> logger = new NullLoggerFactory().CreateLogger<IContactController>();
            IContactController contactController = new ContactController(logger);
            string contactId = Guid.NewGuid().ToString();
            Contact contact1 = new Contact() { Id = contactId, Forename = null, Surname = "Walters" };
            Contact contact2 = new Contact() { Id = contactId, Forename = "Joe", Surname = null };
            IActionResult mergeResult, addResult;

            // Act
            addResult = contactController.Add(contact1);
            mergeResult = contactController.Merge(contact2);

            // Assert
            addResult.Should().NotBeNull();
            addResult.GetType().Should().Be(typeof(OkObjectResult));
            mergeResult.Should().NotBeNull();
            mergeResult.GetType().Should().Be(typeof(OkObjectResult));
        }
    }
}

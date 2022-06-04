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
            if (addResult != null)
                deleteResult = contactController.Delete(contact);

            // Assert
            addResult.Should().NotBeNull();
            addResult.Should().BeEquivalentTo(new OkObjectResult(contact));
        }
    }
}

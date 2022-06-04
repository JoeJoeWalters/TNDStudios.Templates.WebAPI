using Domain;
using Microsoft.AspNetCore.Mvc;
using Repository.Common;
using WebAPI.Contracts;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Contacts
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class ContactController : ControllerBase, IContactController
    {
        private readonly ILogger<IContactController> _logger;
        //private readonly IRepository<Contact, ContactVM> _repository;

        public ContactController(ILogger<IContactController> logger)//, IRepository<Contact, ContactVM> repository)
        {
            _logger = logger;
            //_repository = repository;
        }

        /// <summary>
        /// Query the available stored contacts
        /// </summary>
        /// <returns>An array of contacts</returns>
        [HttpGet]
        [Route("Query")]
        public IActionResult QueryContacts()
        {
            return Ok(new List<ContactVM>() { });
        }

        /// <summary>
        /// Save a contact
        /// </summary>
        /// <param name="contact">The contact model to be saved</param>
        /// <returns>The saved record</returns>
        [HttpPost]
        public IActionResult Add(Contact contact)
        {
            return Ok(new ContactVM(contact) { });
        }

        /// <summary>
        /// Merge changes to a contact in to an existing record
        /// </summary>
        /// <param name="contact">The contact record changes to merge in</param>
        /// <returns>The merged record</returns>
        [HttpPatch]
        public IActionResult Merge(Contact contact)
        {
            return Ok(new ContactVM(contact) { });
        }

        /// <summary>
        /// Remove a contact from the store
        /// </summary>
        /// <param name="contact">The contact to be removed from the store</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(Contact contact)
        {
            return Ok();
        }
    }
}
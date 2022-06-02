using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Contact
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; } = string.Empty;

        public string Forename { get; set; } = string.Empty;

        public string Middlenames { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public Address Address { get; set; } = new Address() { };

        public Contact()
        {

        }
    }
}

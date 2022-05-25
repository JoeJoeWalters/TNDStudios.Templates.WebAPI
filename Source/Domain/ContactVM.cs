using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ContactVM
    {
        public string Title { get; set; } = string.Empty;

        public string Forename { get; set; } = string.Empty;

        public string Middlenames { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public AddressVM Address { get; set; } = new AddressVM() { };

        public ContactVM()
        {

        }

        public ContactVM(Contact source)
        {
            this.Title = source.Title;
            this.Forename = source.Forename;
            this.Middlenames = source.Middlenames;
            this.Surname = source.Surname;
            this.Address = new AddressVM(source.Address);
        }
    }
}

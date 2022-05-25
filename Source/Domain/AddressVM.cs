using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AddressVM
    {
        public string Id { get; set; } = string.Empty;

        public string Addressee { get; set; } = string.Empty;

        public string AddressLine1 { get; set; } = string.Empty;

        public string AddressLine2 { get; set; } = string.Empty;

        public string AddressLine3 { get; set; } = string.Empty;

        public string AddressLine4 { get; set; } = string.Empty;

        public string CountyState { get; set; } = string.Empty;

        public CountryVM Country { get; set; } = new CountryVM() { };

        public string PostalCode { get; set; } = string.Empty;

        public AddressVM()
        {

        }

        public AddressVM(Address source)
        {
            this.Id = source.Id;
            this.Addressee = source.Addressee;
            this.AddressLine1 = source.AddressLine1;
            this.AddressLine2 = source.AddressLine2;
            this.AddressLine3 = source.AddressLine3;
            this.AddressLine4 = source.AddressLine4;
            this.CountyState = source.CountyState;
            this.PostalCode = source.PostalCode;
            this.Country = new CountryVM(source.CountryCode);
        }
    }
}

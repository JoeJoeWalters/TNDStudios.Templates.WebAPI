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
    }
}

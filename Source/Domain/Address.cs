using System;

namespace Domain
{
    public class Address
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Addressee { get; set; } = string.Empty;

        public string AddressLine1 { get; set; } = string.Empty;

        public string AddressLine2 { get; set; } = string.Empty;

        public string AddressLine3 { get; set; } = string.Empty;

        public string AddressLine4 { get; set; } = string.Empty;

        public string CountyState { get; set; } = string.Empty;

        /// <summary>
        /// ISO 3166 3-Digit Country Code
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;
    }
}

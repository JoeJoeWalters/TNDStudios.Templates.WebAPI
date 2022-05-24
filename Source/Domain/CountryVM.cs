using System;
using System.Collections.Generic;
using System.Text;
using ISO._3166; // https://github.com/maisak/iso.resolvers/tree/master/src

namespace Domain
{
    /// <summary>
    /// ISO 3166 Based Country Object
    /// </summary>
    public class CountryVM
    {
        public string CountryName { get; set; } = String.Empty;

        public string CountryCode { get; set; } = String.Empty;

        public string CountryCode2 { get; set; } = String.Empty;

        public CountryVM(){}

        public CountryVM(string countryCode) 
        {
            CountryCode = countryCode;
            if (countryCode == String.Empty)
            {
                var result = CountryCodesResolver.GetByAlpha3Code(countryCode);
                CountryCode2 = result.Alpha2;
                CountryName = result.Name;
            }
        }
    }
}

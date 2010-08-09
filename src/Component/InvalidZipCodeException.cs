using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZipCodes
{
    public class InvalidZipCodeException : Exception
    {
        internal InvalidZipCodeException(string zipCode) : base(
            string.Format("Zip Code '{0}' is invalid. A Zip Code must be a 5 digit numerical value.",
            zipCode)) { }

        internal InvalidZipCodeException(int zipCode) : this(zipCode.ToString()) { }
    }
}

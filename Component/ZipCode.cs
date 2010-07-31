using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZipCodes
{
    public class ZipCode
    {
        #region Private Fields

            private int value;
            private bool region;

        #endregion

        #region Constructor

            internal ZipCode(int zipCode) : this(zipCode, false) { }
            internal ZipCode(string zipCode) : this(zipCode, false) { }

            internal ZipCode(int zipCode, bool region)
            {
                if (!IsValid(zipCode))
                    throw new InvalidZipCodeException(zipCode);
                else
                    this.value = zipCode;
                this.region = region;
            }

            internal ZipCode(string zipCode, bool region)
            {
                if (!TryParse(zipCode, out this.value) ||
                    !IsValid(this.value))
                 throw new InvalidZipCodeException(zipCode);
                this.region = region;
            }

        #endregion

        #region Public Properties

            public int Value
            { get { return value; } }

            public bool Region
            { get { return region; } }

        #endregion

        #region Conversion Operators

            public static bool operator ==(ZipCode valueA, ZipCode valueB)
            {
                return (valueA.value == valueB.value);
            }

            public static bool operator !=(ZipCode valueA, ZipCode valueB)
            {
                return (valueA.value != valueB.value);
            }

            public static implicit operator int(ZipCode zip)
            {
                return zip.value;
            }

            public static implicit operator ZipCode(int zip)
            {
                return new ZipCode(zip);
            }

        #endregion

        #region Public Methods

            public override string ToString()
            {
                return value.ToString();
            }

            public override bool Equals(object obj)
            {
                return (obj != null && 
                        obj is ZipCode &&
                        this == (ZipCode)obj);
            }

            public override int GetHashCode()
            {
                return value;
            }

            public static bool IsValid(int zipCode)
            {
                return (zipCode > 0 & zipCode <= 99999);
            }

            public static bool TryParse(string zipCode, out int result)
            {
                int zipCodeResult;
                if (zipCode == null ||
                    zipCode.Length != 5 ||
                    !int.TryParse(zipCode, out zipCodeResult) ||
                    !IsValid(zipCodeResult))
                {
                    result = 0;
                    return false;
                }
                else
                {
                    result = zipCodeResult;
                    return true;
                }
            }

        #endregion
    }
}

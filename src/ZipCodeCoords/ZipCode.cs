using System.Globalization;

namespace ZipCodes
{
    public class ZipCode
    {
        internal ZipCode(string zipCode)
        {
            int value;
            if (!TryParse(zipCode, out value)) throw new InvalidZipCodeException(zipCode);
            Value = value;
        }

        internal ZipCode(int zipCode)
        {
            if (!IsValid(zipCode)) throw new InvalidZipCodeException(zipCode);
            Value = zipCode;
        }

        public int Value { get; private set; }

        public static bool operator ==(ZipCode valueA, ZipCode valueB)
        {
            return (valueA.Value == valueB.Value);
        }

        public static bool operator !=(ZipCode valueA, ZipCode valueB)
        {
            return (valueA.Value != valueB.Value);
        }

        public static implicit operator int(ZipCode zip)
        {
            return zip.Value;
        }

        public static implicit operator ZipCode(int zip)
        {
            return new ZipCode(zip);
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public override bool Equals(object obj)
        {
            return (obj is ZipCode && this == (ZipCode)obj);
        }

        public override int GetHashCode()
        {
            return Value;
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
            result = zipCodeResult;
            return true;
        }
    }
}

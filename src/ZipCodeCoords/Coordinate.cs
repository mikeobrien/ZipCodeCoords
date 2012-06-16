namespace ZipCodeCoords
{
    public class Coordinate
    {
        internal Coordinate(ZipCode zip, double latitude, double longitude)
        {
            Zip = zip;
            Latitude = latitude;
            Longitude = longitude;
        }

        public ZipCode Zip { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}

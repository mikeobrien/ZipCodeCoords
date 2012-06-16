using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace ZipCodes
{
    public static class Spatial
    {
        private static readonly Lazy<List<Coordinate>> Coordinates = new Lazy<List<Coordinate>>(LoadCoordinates);

        public static Coordinate Search(double latitude, double longitude)
        {
            return Coordinates.Value.OrderBy(x => 
                Math.Sqrt(Math.Pow(latitude - x.Latitude, 2) +
                Math.Pow(longitude - x.Longitude, 2))).First();
        }

        public static Coordinate Search(string zipCode)
	    {
            var zip = new ZipCode(zipCode);
            return Coordinates.Value.FirstOrDefault(x => x.Zip == zip);
        }

        private static List<Coordinate> LoadCoordinates()
        {
            var coordinates = new List<Coordinate>();

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ZipCodes.Data.ZipSpatial.dat"))
            using (var reader = new BinaryReader(stream))
            {
                do
                {
                    var zipCode = reader.ReadInt32();
                    var latitude = reader.ReadDouble();
                    var longitude = reader.ReadDouble();
                    coordinates.Add(new Coordinate(new ZipCode(zipCode), latitude, longitude));
                } while (reader.BaseStream.Position < reader.BaseStream.Length);
            }
            return coordinates;
        }
    }
}

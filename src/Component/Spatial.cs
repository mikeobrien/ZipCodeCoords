using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace ZipCodes
{
    public static class Spatial
    {
        #region Private Constants

            private static List<Coordinate> coordinates = new List<Coordinate>();

        #endregion

        #region Constructor

            static Spatial()
            {
                coordinates = LoadCoordinates("ZipCodes.Data.ZipSpatial.dat");
            }

        #endregion

        #region Public Methods

            public static Coordinate Search(double latitude, double longitude)
            {
                var results = from coordinate in coordinates
                              orderby Math.Sqrt(Math.Pow(latitude - coordinate.Latitude, 2) +
                                        Math.Pow(longitude - coordinate.Longitude, 2))
                              select coordinate;

                return results.First();
            }

            public static Coordinate Search(string zipCode)
	        {
                ZipCode zip = new ZipCode(zipCode);

                var query = from coordinate in coordinates
                            where coordinate.Zip == zip &&
                            !coordinate.Zip.Region
                            select coordinate;

                if (query.Count() > 0) return query.First();

                query = from coordinate in coordinates
                        where coordinate.Zip == ((zip / 100) * 100)
                        && coordinate.Zip.Region 
                        select coordinate;

                if (query.Count() > 0) return query.First();
                else return null;
            }

        #endregion

        #region Private Methods

            private static List<Coordinate> LoadCoordinates(string ResourceName)
            {
                List<Coordinate> coordinates = new List<Coordinate>();

                using (BinaryReader reader = new BinaryReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName)))
                {
                    do
                    {
                        int zipCode = reader.ReadInt32();
                        bool region = reader.ReadBoolean();
                        double latitude = reader.ReadDouble();
                        double longitude = reader.ReadDouble();

                        coordinates.Add(
                            new Coordinate(
                                new ZipCode(zipCode, region),
                                latitude,
                                longitude
                                )
                            );
                    } while (reader.BaseStream.Position < reader.BaseStream.Length);
                }

                return coordinates;
            }

        #endregion
    }
}

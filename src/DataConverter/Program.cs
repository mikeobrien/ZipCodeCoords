using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(string.Join(" ", args));
            string folder = Path.GetDirectoryName(path);

            List<Record> records = new List<Record>();

	        using (TextReader reader = new StreamReader(path))
            {
                string data;

	            do 
                {

                    data = reader.ReadLine();

                    if (data != null && data.Trim().Length == 157)
                    {
                        string zipField = data.Substring(2, 5);
                        string latitudeField = data.Substring(136, 10);
                        string longitudeField = data.Substring(146, 11);

                        int zip;
                        double latitude;
                        double longitude; 
                        bool region = false;

                        if (zipField.Substring(3, 2) == "XX" ||
                            zipField.Substring(3, 2) == "HH")
                        {
                            zipField = zipField.Substring(0, 3) + "00";
                            region = true;
                        }

                        if (int.TryParse(zipField, out zip) &&
                            double.TryParse(latitudeField, out latitude) &&
                            double.TryParse(longitudeField, out longitude))
                        {
                            records.Add(new Record()
                                { ZipCode = zip,
                                  Region = region,
                                  Latitude = latitude,
                                  Longitude = longitude});
                        }
                    }

	            } while (data != null);
            }

            using (
                BinaryWriter zipWriter = new BinaryWriter(
                    new FileStream(
                        Path.Combine(folder, "ZipSpatial.dat"), FileMode.Create)))
            {
                var query = from record in records
                            orderby record.ZipCode
                            select record;

                foreach (var record in query)
                {
                    zipWriter.Write(record.ZipCode);
                    zipWriter.Write(record.Region);
                    zipWriter.Write(record.Latitude);
                    zipWriter.Write(record.Longitude);
                }
            }
        }
    }

    class Record
    {
        public int ZipCode { get; set; }
        public bool Region { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

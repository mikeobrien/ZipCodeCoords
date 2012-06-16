using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture(Ignore = true)]
    public class GenerateDataFile
    {
        [Test]
        public void generate_data_file()
        {
            var source = Path.GetFullPath("../../../../misc/Gaz_zcta_national.txt");
            var output = Path.GetFullPath("../../../ZipCodeCoords/Data/ZipSpatial.dat");

            var data = File.ReadAllLines(source)
                .Where((x, i) => i != 0 && !string.IsNullOrEmpty(x))
                .Select(x => x.Split('\t'))
                .Where(x => !x.All(string.IsNullOrEmpty)).ToList();

            using (var writer = new BinaryWriter(new FileStream(output, FileMode.Create)))
            {
                foreach (var line in data)
                {
                    writer.Write(int.Parse(line[0]));
                    writer.Write(double.Parse(line[5]));
                    writer.Write(double.Parse(line[6]));
                }
            }
        }
    }
}
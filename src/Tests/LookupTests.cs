using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Should;
using ZipCodeCoords;

namespace Tests
{
    [TestFixture]
    public class LookupTests
    {
        [Test]
        public void should_lookup_zip_code_by_coords()
        {
            var coordinate = Spatial.Search(-108.491240, 39.079600);

            coordinate.Zip.ToString().ShouldEqual("81504");
            coordinate.Latitude.ShouldEqual(-108.491247);
            coordinate.Longitude.ShouldEqual(39.079602);
        }

        [Test]
        public void should_lookup_coords_by_zip_code()
        {
            var coordinate = Spatial.Search("81504");

            coordinate.Zip.ToString().ShouldEqual("81504");
            coordinate.Latitude.ShouldEqual(-108.491247);
            coordinate.Longitude.ShouldEqual(39.079602);
        }

        [Test]
        public void should_retun_null_when_zip_code_not_found()
        {
            Spatial.Search("81500").ShouldBeNull();
        }
    }
}

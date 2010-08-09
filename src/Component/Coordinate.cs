using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZipCodes
{
    public class Coordinate
    {
        #region Private Fields

            private ZipCode zip;
            private double latitude;
            private double longitude;

        #endregion

        #region Constructor

            internal Coordinate(ZipCode zip, double latitude, double longitude)
            {
                this.zip = zip;
                this.latitude = latitude;
                this.longitude = longitude;
            }

        #endregion

        #region Public Properties

            public ZipCode Zip
            { get { return zip; } }
     
            public double Latitude
            { get { return latitude; } }

            public double Longitude
            { get { return longitude; } }

        #endregion
    }
}

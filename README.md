Zip Code Cooridnates
=============

The Zip Code to Coordinates .NET Component enables you to retrieve the global coordinates (Latitude and Longitude) of a Zip Code. This small component, with a footprint of only 650k, has mapping data embedded as a binary resource so it does not require any external data.

Overview
------------

The data embedded in this component was obtained from the US Census Bureau's 2000 Census. The Census Bureau makes much of their data freely available to the public. Keep this in mind before purchasing high priced demographic data from a 3rd party. As of the 2000 Census the USCB has made available a new statistical entity called ZIP Code Tabulation Areas or ZCTA for short. ZCTA's are geographical areas which loosely coorespond to postal zip codes. Assigning geographical coordinates to a postal zip code is a difficult task since they coorespond to postal routes and not physical boundries. Postal routes are not confined to any region and may cross state and county lines. ZCTA's are physical regions, with defined coordinates, that are assigned the prevailing zip code in that region. In other words a 10 square block section could be designated as a ZCTA. This region could contian 90% of zip code A and 10% zip code B. This region would be assigned zip code A since it is the prevailing zip code in the region.  

Although the data in the component is dated and uses ZCTA's it will attempt to return the exact match first, then if it cant find an exact match it will search for a "region" which encompasses a larger area. In order to get very precise, up to date zip code to coordinate mapping you may need to consider purchasing 3rd party data via a subscription or subscribe to a web service. Although for the vast majority of implementations this component will provide adequate zip code to coordinate mapping.  

This code demonstrates how to retrieve the coordinates for a particular zip code.  

    ZipCode.Coordinate coordinate = ZipCode.Spatial.Search("81504");

    Debug.WriteLine("Zip: " + coordinate.Zip.ToString());
    Debug.WriteLine("Lat: " + coordinate.Latitude);
    Debug.WriteLine("Long: " + coordinate.Longitude);

Installation
------------

    nu install zipcodecoords
	
Resources
------------

[ZCTA Main](http://zipcodecoords.codeplex.com/wikipage?title=http%3a%2f%2fwww.census.gov%2fgeo%2fZCTA%2fzcta.html&referringTitle=Home): U.S. Census Bureau ZCTA main page. This page contains links to information regaring ZCTA. 
[ZCTA Downloads](http://zipcodecoords.codeplex.com/wikipage?title=http%3a%2f%2fwww.census.gov%2fgeo%2fwww%2fgazetteer%2fplaces2k.html&referringTitle=Home): U.S. Census Bureau data downloads and file format specs.
[Understanding ZCTA's](http://zipcodecoords.codeplex.com/wikipage?title=http%3a%2f%2fwww.census.gov%2fgeo%2fZCTA%2fshort_sdc_zcta.pdf&referringTitle=Home): U.S. Census Bureau brochure explaining ZCTA's.
[U.S. Census Bureau](http://zipcodecoords.codeplex.com/wikipage?title=http%3a%2f%2fwww.census.gov&referringTitle=Home)
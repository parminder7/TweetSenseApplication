using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TweetSenseApplication.src.reverseGeoCoding
{
    class Country
    {
        /// <summary>
        /// This method retruns the country name as per given lattitude and longitude
        /// Using Google Geocoding API
        /// </summary>
        /// <param name="Lattitude"></param>
        /// <param name="Longitude"></param>
        /// <returns></returns>
        public static string getCountryByLongitudeLattitude(double Lattitude, double Longitude)
        {
            string url = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + Lattitude.ToString() + "," + Longitude.ToString() + "&sensor=false";
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(url);
                XmlNode node = document.SelectSingleNode("/GeocodeResponse/status");
                if (node.InnerText == "ZERO_RESULTS")
                {
                    Console.WriteLine("None");
                    return null;
                }

                XmlNodeList nodeList = document.SelectNodes("/GeocodeResponse/result/address_component");
                foreach (XmlNode n in nodeList)
                {
                    if ((n["type"].InnerText) == "country")
                    {
                        return n["long_name"].InnerText;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}

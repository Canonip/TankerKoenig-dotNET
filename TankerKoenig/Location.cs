using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankerKoenig
{
    public class Location
    {
        public double Latitude { get; private set; }
        public double Longtitude { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string HouseNumber { get; private set; }
        public int PostCode { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="lat">The Latitude.</param>
        /// <param name="lng">The Longtitude.</param>
        /// <param name="city">The city.</param>
        /// <param name="street">The street.</param>
        /// <param name="houseNumber">The house number.</param>
        /// <param name="postCode">The post code.</param>
        public Location (double lat, double lng, string city, string street, string houseNumber, int postCode)
        {
            Latitude = lat;
            Longtitude = lng;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PostCode = postCode;
        }
        public override string ToString()
        {
            return $"{Street}, {HouseNumber}\n{PostCode} {City}";
        }
    }
}

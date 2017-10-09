using System;

namespace TankerKoenig.Api
{
    public class SearchOptions
    {
        private double latitude;
        private double longtitude;
        private double radius;
        private SearchGasType type;
        private SearchSort sort;

        public double Latitude { get => latitude; set => latitude = value; }
        public double Longtitude { get => longtitude; set => longtitude = value; }
        public double Radius { get => radius; set => radius = value; }
        public SearchGasType Type { get => type; set => type = value; }
        public SearchSort Sort { get => sort; set => sort = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchOptions"/> class.
        /// </summary>
        /// <param name="lat">The latitude.</param>
        /// <param name="lng">The longtitude.</param>
        /// <param name="rad">The radius.</param>
        /// <exception cref="ArgumentException">If Radius is invalid</exception>
        public SearchOptions(double lat, double lng, double rad)
        {
            //Limit by API
            if (rad > 25 || rad < 0) throw new ArgumentException("Radius has to be between 0 and 25km");

            latitude = lat;
            longtitude = lng;
            radius = rad;
            sort = SearchSort.dist;
            type = SearchGasType.all;
        }

        /// <summary>
        /// Sets the sorting.
        /// </summary>
        /// <param name="sortType">Type of the sorting.</param>
        /// <returns></returns>
        public SearchOptions SetSorting(SearchSort sortType)
        {
            sort = sortType;
            return this;
        }

        /// <summary>
        /// Sets the type of the gas.
        /// </summary>
        /// <param name="gasType">Type of the gas.</param>
        /// <returns></returns>
        public SearchOptions SetGasType(SearchGasType gasType)
        {
            type = gasType;
            return this;
        }

        public string GetQueryUri()
        {
            return $"/list.php?lat={latitude}&lng={longtitude}&rad={radius}&sort={sort}&type={type}";
        }

        public enum SearchSort
        {
            price, dist
        }

        public enum SearchGasType
        {
            e5, e10, diesel, all
        }
    }
}

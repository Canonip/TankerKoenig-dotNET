using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TankerKoenig.Dto;
using TankerKoenig.Exceptions;

namespace TankerKoenig.Api
{
    public class TankerKoenigManager
    {

        public string ApiKey { get; private set; }

        public static readonly Uri BaseUrl = new Uri("https://creativecommons.tankerkoenig.de");
        /// <summary>
        /// Gets or sets the gas stations.
        /// </summary>
        /// <value>
        /// The gas stations.
        /// </value>
        public Dictionary<Guid, GasStation> GasStations
        {
            get { return gasStations; }
            set { gasStations = value; }
        }
        private Dictionary<Guid, GasStation> gasStations = new Dictionary<Guid, GasStation>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TankerKoenigManager"/> class with given API-Key.
        /// </summary>
        /// <param name="apikey">The API-Key.</param>
        public TankerKoenigManager(string apikey)
        {
            ApiKey = apikey;
        }

        /// <summary>
        /// Adds given Gas Station to List.
        /// </summary>
        /// <exception cref="InvalidGasStationException">If Gas Station does not exist</exception>
        /// <param name="gasStation">The gas station.</param>
        public async Task AddGasStation(GasStation gasStation)
        {
            if (GasStations.ContainsKey(gasStation.ID))
                throw new AlreadyExistsException($"Gas Station with ID {gasStation.ID} already exists in List");

            GasStations.Add(gasStation.ID, gasStation);
            try { await LoadDetails(gasStation); }
            catch (InvalidGasStationException e)
            {
                gasStations.Remove(gasStation.ID);
                throw e;
            }
        }

        /// <summary>
        /// Adds new Gas Station via Identifier
        /// </summary>
        /// <exception cref="InvalidGasStationException">If Gas Station does not exist</exception>
        /// <param name="id">The gas station identifier.</param>
        public async Task AddGasStation(Guid id)
        {
            await AddGasStation(new GasStation(id));
        }
        /// <summary>
        /// Loads the detailed information for a Gas Station.
        /// </summary>
        /// <param name="gasStation">The gas station.</param>
        /// <returns>The updated Gas Station</returns>
        /// <exception cref="ArgumentException">If Gas Station has not been added beforehand</exception>
        /// <exception cref="InvalidApiKeyException">If Apikey is invalid</exception>
        /// <exception cref="InvalidGasStationException">If Gas Station does not exist</exception>
        /// <exception cref="ServerException">On other error</exception>
        public async Task<GasStation> LoadDetails(GasStation gasStation)
        {
            if (!gasStations.ContainsValue(gasStation)) throw new ArgumentException($"Gas Station with ID {gasStation.ID} is not in List, please add");
            //if not exists than 
            var query = $"json/detail.php?id={gasStation.ID}&apikey={ApiKey}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUrl;
                var response = await client.GetAsync(query);
                if (!response.IsSuccessStatusCode)
                    throw new ServerException($"Returned Status Code is {response.StatusCode}");

                JObject responseObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                var station = responseObject.ToObject<StationDetailDto>();
                //check if ok
                if (!station.Ok)
                {
                    var message = station.Message ?? "Unknown Error";
                    if (message.ToLower().Contains("key")) throw new InvalidApiKeyException(message);
                    if (message.ToLower().Contains("nicht vorhanden")) throw new InvalidGasStationException($"Gas Station with ID {gasStation.ID} does not exist");
                    throw new ServerException(message);
                }
                var detailledGasStation = station.Station.ToBusinessObject();
                gasStations[gasStation.ID] = detailledGasStation;
                return detailledGasStation;
            }
        }

        /// <summary>
        /// Searches for Available Gas Stations via SearchOptions
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>List of found <see cref="GasStation"/>s</returns>
        /// <exception cref="ServerException">On other Error
        /// </exception>
        /// <exception cref="InvalidApiKeyException">If Apikey is invalid</exception>
        public async Task<Dictionary<Guid, GasStation>> SearchForGasStations(SearchOptions options)
        {
            var c = CultureInfo.InvariantCulture;
            var query = $"json/list.php?lat={options.Latitude.ToString(c)}&lng={options.Longtitude.ToString(c)}&rad={options.Radius.ToString(c)}&sort={options.Sort}&type={options.Type}&apikey={ApiKey}";
            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseUrl;
                var response = await client.GetAsync(query);
                if (!response.IsSuccessStatusCode)
                    throw new ServerException($"Returned Status Code is {response.StatusCode}");

                JObject responseObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                var stationList = responseObject.ToObject<StationListDto>();
                //check if ok
                if (!stationList.Ok)
                {
                    var message = stationList.Message ?? "Unknown Error";
                    if (message.ToLower().Contains("key")) throw new InvalidApiKeyException(message);
                    throw new ServerException(message);
                }
                var foundStations = new Dictionary<Guid, GasStation>();
                foreach (var station in stationList.Stations)
                {
                    foundStations.Add(new Guid(station.Id), station.ToBusinessObject());
                }
                return foundStations;
            }
        }

        /// <summary>
        /// Search for available Gas Stations via coordinates and radius
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lng">Longtitude</param>
        /// <param name="rad">Radius</param>
        /// <returns>List of found <see cref="GasStation"/>s</returns>
        public async Task<Dictionary<Guid, GasStation>> SearchForGasStations(double lat, double lng, double rad)
        {
            var options = new SearchOptions(lat, lng, rad);
            return await SearchForGasStations(options);
        }


        /// <summary>
        /// Refreshes the gas prices.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ServerException">On other Error
        /// </exception>
        /// <exception cref="InvalidApiKeyException">If Apikey is invalid</exception>
        public async Task<Dictionary<Guid, GasStation>> RefreshGasPrices()
        {//split to 10 allways max
            var lists = SplitList(gasStations.Keys.ToList(), 10);
            foreach (var list in lists)
            {
                //string with max 10 guids seperated with comma
                var ids = string.Join(",",
                          list.Select(x => x.ToString()).ToArray());

                var query = $"json/prices.php?ids={ids}&apikey={ApiKey}";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = BaseUrl;
                    var response = await client.GetAsync(query);
                    if (!response.IsSuccessStatusCode)
                        throw new ServerException($"Returned Status Code is {response.StatusCode}");

                    JObject responseObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    var stationList = responseObject.ToObject<PriceListDto>();
                    //check if ok
                    if (!stationList.Ok)
                    {
                        var message = stationList.Message ?? "Unknown Error";
                        if (message.ToLower().Contains("key")) throw new InvalidApiKeyException(message);
                        throw new ServerException(message);
                    }
                    foreach (var station in stationList.Prices)
                    {
                        GasStations[new Guid(station.Key)].Prices = station.Value.ToBusinessObject();
                    }

                }
            }

            return gasStations;
        }
        /// <summary>
        /// Splits the list into sublists of n size.
        /// </summary>
        /// <param name="locations">The locations.</param>
        /// <param name="nSize">Size of the wanted lists</param>
        private static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize)
        {
            //https://stackoverflow.com/questions/11463734/split-a-list-into-smaller-lists-of-n-size
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
    }
}
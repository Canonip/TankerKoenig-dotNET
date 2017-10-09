using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TankerKoenig.Api;
using System.Threading.Tasks;
using TankerKoenig.Exceptions;

namespace TankerKoenigTest
{
    [TestClass]
    public class DefaultTests
    {
        TankerKoenigManager manager;
        [TestInitialize]
        public void Initialize()
        {
            manager = new TankerKoenigManager("00000000-0000-0000-0000-000000000002");
        }

        /// <summary>
        /// Tests the Search API with Sample Data.
        /// </summary>
        [TestMethod]
        public async Task DefaultSearch()
        {
            var result = await manager.SearchForGasStations(52.521, 13.438, 1.5);
            Assert.AreEqual(result[new Guid("474e5046-deaf-4f9b-9a32-9797b778f047")].Location.Street, "MARGARETE-SOMMER-STR.");
            Assert.AreEqual(result[new Guid("4429a7d9-fb2d-4c29-8cfe-2ca90323f9f8")].Location.PostCode, 10243);
            Assert.AreEqual(result[new Guid("278130b1-e062-4a0f-80cc-19e486b4c024")].ID, new Guid("278130b1-e062-4a0f-80cc-19e486b4c024"));
            Assert.AreEqual(result[new Guid("e1a15081-25a3-9107-e040-0b0a3dfe563c")].Brand, "HEM");
        }


        [TestMethod, ExpectedException(typeof(InvalidApiKeyException))]
        public async Task SearchWithoutApiKey()
        {
            manager = new TankerKoenigManager("");
            await manager.SearchForGasStations(52.521, 13.438, 1.5);
        }

        [TestMethod, ExpectedException(typeof(InvalidApiKeyException))]
        public async Task SearchWithInvalidApiKey()
        {
            manager = new TankerKoenigManager("278130b1-e062-4a0f-80cc-19e486b4c024");
            await manager.SearchForGasStations(52.521, 13.438, 1.5);
        }
        [TestMethod]
        public async Task DefaultPriceRefresh()
        {
            await manager.AddGasStation(new Guid("4429a7d9-fb2d-4c29-8cfe-2ca90323f9f8"));
            await manager.AddGasStation(new Guid("446bdcf5-9f75-47fc-9cfa-2c3d6fda1c3b"));
            await manager.AddGasStation(new Guid("60c0eefa-d2a8-4f5c-82cc-b5244ecae955"));
            await manager.RefreshGasPrices();
        }
        [TestMethod, ExpectedException(typeof(AlreadyExistsException))]
        public async Task AddExistingStation()
        {
            await manager.AddGasStation(new Guid("4429a7d9-fb2d-4c29-8cfe-2ca90323f9f8"));
            await manager.AddGasStation(new Guid("4429a7d9-fb2d-4c29-8cfe-2ca90323f9f8"));
        }

        [TestMethod, ExpectedException(typeof(InvalidGasStationException))]
        public async Task InvalidGasStation()
        {
            await manager.AddGasStation(new Guid("44444444-4444-4444-4444-444444444444"));
        }
    }
}

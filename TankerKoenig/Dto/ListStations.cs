using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankerKoenig.Api;

namespace TankerKoenig.Dto
{

    internal class StationListDto
    {
        public bool Ok { get; set; }
        public string License { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public StationDto[] Stations { get; set; }
    }

    internal class StationDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Street { get; set; }
        public string Place { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public float Dist { get; set; }
        public float Diesel { get; set; }
        public float E5 { get; set; }
        public float E10 { get; set; }
        public bool IsOpen { get; set; }
        public string HouseNumber { get; set; }
        public int PostCode { get; set; }

        /// <summary>
        /// To the business object.
        /// </summary>
        /// <returns></returns>
        public virtual GasStation ToBusinessObject()
        {
            //if not open, price == null
            var price = IsOpen ? new GasPrice()
            {
                Diesel = Diesel,
                E10 = E10,
                E5 = E5
            } : null;

            var loc = new Location(
                Lat,
                Lng,
                Place,
                Street,
                HouseNumber,
                PostCode
            );

            return new GasStation(new Guid(Id), Name, Brand, loc)
            {
                Prices = price
            };
        }


    }
    internal class StationDetailDto
    {
        public bool Ok { get; set; }
        public string License { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DetailedStationDto Station { get; set; }
    }

    internal class DetailedStationDto : StationDto
    {
        public OpeningTime[] OpeningTimes { get; set; }
        public string[] Overrides { get; set; }
        public bool WholeDay { get; set; }
        public string State { get; set; }
        public override GasStation ToBusinessObject()
        {
            var station =  base.ToBusinessObject();
            station.Details = new Details()
            {
                OpeningTimes = OpeningTimes,
                Overrides = Overrides,
                WholeDay = WholeDay,
                State = State
            };
            return station;
        }
    }
}

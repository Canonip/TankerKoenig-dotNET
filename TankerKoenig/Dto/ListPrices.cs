using System.Collections.Generic;
using TankerKoenig.Api;

namespace TankerKoenig.Dto
{

    internal class PriceListDto
    {
        public bool Ok { get; set; }
        public string License { get; set; }
        public string Data { get; set; }
        public string Message { get; set; }
        public Dictionary<string, PriceDto> Prices { get; set; }
    }

    internal class PriceDto
    {
        public string Status { get; set; }
        public string E5 { get; set; }
        public string E10 { get; set; }
        public string Diesel { get; set; }

        /// <summary>
        /// To the business object.
        /// </summary>
        /// <returns></returns>
        public GasPrice ToBusinessObject()
        {
            if (Status.Contains("closed")) return null;

            double.TryParse(E5, out double e5);
            double.TryParse(E10, out double e10);
            double.TryParse(Diesel, out double diesel);
            return new GasPrice()
            {
                E5 = e5,
                E10 = e10,
                Diesel = diesel
            };
        }
    }
}

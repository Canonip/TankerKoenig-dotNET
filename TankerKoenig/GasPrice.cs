using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankerKoenig.Api
{
    public class GasPrice
    {
        public double E10 { get; set; }
        public double E5 { get; set; }
        public double Diesel { get; set; }

        public double GetPrice(GasType type)
        {
            switch (type)
            {
                case GasType.diesel:
                    return Diesel;
                case GasType.e10:
                    return E10;
                case GasType.e5:
                    return E5;
            }
            return E5;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TankerKoenig.Api
{
    public class GasStation
    {
        public Guid ID { get; private set; }
        public bool Open
        {
            get { return Prices != null; } //true if prices exist
            set { if (!value) Prices = null; } //
        }
        public string Name { get; private set; }
        public GasPrice Prices { get; set; }
        public string Brand { get; private set; }
        //public float Dist { get; set; } //Unknown if needed..... probably not
        public Location Location { get; private set; }
        public Details Details { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GasStation"/> class with id only.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public GasStation(Guid id)
        {
            ID = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GasStation"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="brand">The brand.</param>
        /// <param name="location">The location.</param>
        public GasStation(Guid id, string name, string brand, Location location)
        {
            ID = id;
            Name = name;
            Location = location;
            Brand = brand;
        }

        public void SetOpenStatus(bool open)
        {
            Open = open;
            if (!open)
            {
                Prices = null;
            }
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            GasStation g = obj as GasStation;
            if (g == null)
            {
                return false;
            }

            if (g.ID.Equals(ID)) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }

   

    struct JsonAnswer
    {
        bool Ok { get; set; }
        string Message { get; set; }
        JsonGasStation[] Prices { get; set; }

    }
    struct JsonGasStation
    {
        string Uuid { get; set; }
        string Name { get; set; }
        string Brand { get; set; }
        string Street { get; set; }
        string Place { get; set; }
        double Lat { get; set; }
        double Lng { get; set; }

    }
}

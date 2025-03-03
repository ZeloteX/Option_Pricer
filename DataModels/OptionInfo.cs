using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Option_Pricer.Objects
{
    internal class OptionInfo
    {
        private double _strike_price;
        private double _spot_price;
        private double _maturity;                // in Year, 1 = 1 year until maturity
        private double _volatility;              // in %
        private double _free_interest_rate;      // in %
        
        private readonly string _option_type;    // [CALL, PUT]
        private readonly string _underlying;     // Ex: AAPL
        private readonly string _name;

        
        private double? _premium;

        public OptionInfo(string option_type, string underlying)
        {
            this._option_type = option_type;
            this._underlying = underlying;

            string[] name = { Underlying, Option_type, "Option" };
            this._name = String.Join("_", name);
        }

        [JsonIgnore]
        public string Name { get => _name; }

        public string Underlying { get => _underlying; }
        public string Option_type { get => _option_type; }

        public double Strike_price { get => _strike_price; set => _strike_price = value; }
        public double Spot_price { get => _spot_price; set => _spot_price = value; }
        public double Volatility { get => _volatility; set => _volatility = value; }
        public double Free_interest_rate { get => _free_interest_rate; set => _free_interest_rate = value; }
        public double Maturity { get => _maturity; set => _maturity = value; }

        public double? Premium { get => _premium; set => _premium = value; }
    }
}
    
    

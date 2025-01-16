using MND = MathNet.Numerics.Distributions.Normal;
namespace Option_Pricer.Computation
{
    internal class ComputationOptionPrice
    {
        private double strike_price;         
        private double spot_price;
        private double maturity;                // in Years
        private double volatility;              // [0;1]
        private double free_interest_rate;      // [0;1] -> r
        private string option_type;             // CALL or PUT (not case sensitive)

        //Constructor
        public ComputationOptionPrice(double strike_price,
                                      double spot_price,
                                      double maturity,
                                      double volatility,
                                      double free_interest_rate,
                                      string option_type)
        {
            this.strike_price = strike_price;
            this.spot_price = spot_price;
            this.maturity = maturity;
            this.volatility = volatility / 100;
            this.free_interest_rate = free_interest_rate / 100;
            this.option_type = option_type.ToUpper();
        }

        //Getters and Setters 
        public double Strike_price { get => strike_price; set => strike_price = value; }
        public double Spot_price { get => spot_price; set => spot_price = value; }
        public double Maturity { get => maturity; set => maturity = value; }
        public double Volatility { get => volatility; set => volatility = value; }
        public double Free_interest_rate { get => free_interest_rate; set => free_interest_rate = value; }
        public string Option_type { get => option_type; set => option_type = value; }


        // Cumulative Distribution Function of a standard normal random variable, From John D. Cook: https://www.johndcook.com/blog/csharp_phi/
        static double CDF(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x) / Math.Sqrt(2.0);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double erf = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return 0.5 * (1.0 + sign * erf);
        }


        // Write a GlobalComputation class. ComputationOptionPrice(GlobalComputation)
        // Computation to get the price of an option given every variablesÜ
        public double computation()
        {
            double d1 = ((Math.Log(Spot_price) - Math.Log(Strike_price)) + (Free_interest_rate + (Math.Pow(Volatility, 2) / 2)) * Maturity) / Volatility * Math.Sqrt(Maturity);
            double d2 = d1 - Volatility * Math.Sqrt(Maturity);

            //Peut être faire une factory (pattern desing)
            // N(d1) -> N() = Cumulative distribution function, for a standard normale distribution
            if (Option_type == "CALL")
            {
                double option_price = Spot_price * CDF(d1) - Strike_price * Math.Exp((- Free_interest_rate) * Maturity) * CDF(d2);
                return option_price;
            }

            if (Option_type == "PUT")
            {
                double option_price = Strike_price * Math.Exp((-Free_interest_rate) * Maturity) * MND.CDF(0,1,-d2) - Spot_price * MND.CDF(0, 1, -d1);
                return option_price;
            }

            throw new ArgumentException("Parameter 'option_type' should be CALL or PUT got : ", nameof(option_type));
        }
    }
}
/*
 * Main for this project  
 * 
 * Date == Datetime
 */
using System.Data;
using Option_Pricer.Computation;
using Option_Pricer.Utils;

namespace Option_Pricer
{
    internal class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Start: Program");

            /** Initialisation **/
            // Need Global information 
            DateTime date = DateTime.Now;
            string underlying = "AAPL"; // ToDo: should not be harcoded or not here 


            /** Values **/
            double strike_price = 40;
            double spot_price = 42;
            double maturity = 0.5;              // 2 years
            double volatility = 20;             // 20%
            double free_interest_rate = 10;     // 10%
            string option_type = "Call";

            

            /** Computation **/
            ComputationVolatility calculator_vol = new ComputationVolatility(date, underlying);
            double vol = calculator_vol.Computation();

            ComputationOptionPrice calculator = new ComputationOptionPrice(strike_price, spot_price, maturity, volatility, free_interest_rate, option_type);

            double option_price = calculator.computation();
            
            Console.WriteLine($"Done: Program at {DateTime.Now}");
            Console.WriteLine($"Option '{option_type}' price = {option_price}");

        }
        

            
        
    }
}
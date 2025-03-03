/*
 * Main for this project  
 */
using Option_Pricer.Computation;
using Option_Pricer.Objects;
using Newtonsoft.Json;
using Option_Pricer.DataModels;
using Config = Option_Pricer.Configurations.CustomConfiguration;
using Option_Pricer.Configurations;

namespace Option_Pricer
{
    internal class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Start: Program");

            /** Initialization **/
            var jsonInfo = File.ReadAllText(Config._input_config);

            var input_config = JsonConvert.DeserializeObject<InputGlobal>(jsonInfo);
            DateTime date = DateTime.Now;

            /** Computation **/
            foreach (OptionInfo option in input_config.Option_infos)
            {
                Console.WriteLine(option.Name);

                if(option.Volatility == 0)  //Volatility = 0 -> No vol given 
                {
                    ComputationVolatility calculator_volatility = new ComputationVolatility(date, option.Underlying);
                    option.Volatility = calculator_volatility.Computation();
                }

                ComputationOptionPrice calculator_premium = new ComputationOptionPrice(date, option.Strike_price, option.Spot_price, option.Maturity, option.Volatility, option.Free_interest_rate, option.Option_type);
                option.Premium = calculator_premium.Computation();

            }

            /** Conclusion **/
            string jsonOutput = JsonConvert.SerializeObject(input_config, Formatting.Indented);
            File.WriteAllText(Config._mocks_path + "Outputs/" + $"OptionList_{date.ToString(Config._date_format)}.txt", jsonOutput);

            Console.WriteLine($"Done: Program at {DateTime.Now}");
        }        
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Option_Pricer.Computations;
using Option_Pricer.Extractors;
using Option_Pricer.Utils;

namespace Option_Pricer.Computation
{
    internal class ComputationVolatility: ComputationGlobal
    {   
        private string _stock_name;

        public ComputationVolatility(DateTime date, string stock_name): base(date) 
        {
            this._stock_name = stock_name;
        }

        protected override DataTable GetDatas()
        {
            ExtractorStockClosure extractor_stocks = new ExtractorStockClosure(Date);

            DataTable dt = extractor_stocks.Extract_data();
            return dt;
        }

        protected override double Processing(DataTable dt) 
        {
            DataTable dt_res = dt.Copy();

            // Compute Daily return
      
            dt_res.Columns.Add($"Daily return {this._stock_name}", typeof(double));
                
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dt_res.Rows[i][$"Daily return {this._stock_name}"] = DBNull.Value;
                }
                else
                {
                    dt_res.Rows[i][$"Daily return {this._stock_name}"] = 
                    Math.Log((double)dt.Rows[i][this._stock_name]) - 
                    Math.Log((double)dt.Rows[i - 1][this._stock_name]);
                }
            }


            // Compute Variance & Std
            List<double> daily_returns = dt_res.AsEnumerable()
                .Where(row => row[$"Daily return {this._stock_name}"] != DBNull.Value)
                .Select(row => row.Field<double>($"Daily return {this._stock_name}"))
                .ToList();

            double stdev = StandardDeviation(daily_returns.ToArray());

            double volatility = stdev * Math.Sqrt(252); // Annualisation of Volatility

            return Math.Round(volatility * 100, 2);
                 
        }

        private static double StandardDeviation(double[] values) 
        {

            double mean = values.Average();
            
            double sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += Math.Pow(values[i] - mean, 2);
            }
            
            return Math.Sqrt(sum / values.Length);
                
        }
    }
}

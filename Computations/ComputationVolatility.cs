using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Option_Pricer.Utils;

namespace Option_Pricer.Computation
{
    internal class ComputationVolatility
    {
        private static FilesManagement tools = new FilesManagement(DateTime.Today);
        private DataTable stocks_closure = tools.CsvToDataTable("YahooFinanceExtractor_2024-12-01");
        
        private string ticket;  // name of the asset

        public ComputationVolatility(string ticket) 
        {
            Ticket = ticket;
        }

        public string Ticket { get => ticket; set => ticket = value; }


        public double computation() 
        {  
            DataTable dt = this.stocks_closure.Copy();
            dt.Columns.Add("Daily return", typeof(double));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dt.Rows[i]["Daily return"] = DBNull.Value;
                }
                else 
                {
                    dt.Rows[i]["Daily return"] = Math.Log((double)dt.Rows[i][Ticket]) - Math.Log((double)dt.Rows[i - 1][Ticket]);
                }
                
            }

            return -1;
                 
        }
    }
}

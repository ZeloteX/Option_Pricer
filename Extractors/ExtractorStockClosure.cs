using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Option_Pricer.Utils;

namespace Option_Pricer.Extractors
{
    internal class ExtractorStockClosure: ExtractorGlobal
    {
        public ExtractorStockClosure(DateTime date) 
            :base(date) { }

        //CHANGE: argument in 'CsvToDataTable' to be not hard coded
        protected override DataTable Call_api()
        {
            FilesManagement tools = new FilesManagement(DateTime.Today);
            DataTable stocks_closure = tools.CsvToDataTable("YahooFinanceExtractor_2024-12-01");
            
            return stocks_closure;
        }

        protected override DataTable Post_processing(DataTable dt)
        {
            DataTable dt_res = new DataTable();

            //Convert data type to the right data type
            foreach (DataColumn col in dt.Columns)
            { 
                if (col.ColumnName == "Date")
                {
                    dt_res.Columns.Add(col.ColumnName, typeof(DateTime));
                }
                else
                {
                    dt_res.Columns.Add(col.ColumnName, typeof(double));
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                dt_res.ImportRow(row);
            }

            return dt_res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Option_Pricer.Configurations;

namespace Option_Pricer.Extractors
{
    internal abstract class ExtractorGlobal
    {
        private DateTime _date;
        private string extraction_name;

        public DateTime Date { get => _date; set => _date = value; }
        public string Extraction_name { get => extraction_name; set => extraction_name = value; }

        public ExtractorGlobal(DateTime date) 
        {
            Date = date;
            Extraction_name = string.Join
                (
                    "_",
                    this.GetType().Name, 
                    Date.ToString(CustomConfiguration._date_format)
                );
        }       

        public DataTable Extract_data()
        {
            Console.WriteLine("Start: Extraction for " + Extraction_name);
            DataTable dt = Call_api();
            Console.WriteLine("Done: Extraction for " + Extraction_name);

            /**
             * 
             * 
             * Here we can create Mocks
             * 
             * 
             **/

            Console.WriteLine("Start: Post processing for " + Extraction_name);
            DataTable res_dt = Post_processing(dt);
            Console.WriteLine("Done: Post processing for " + Extraction_name);

            return res_dt;

        }

        protected abstract DataTable Call_api();
      
        protected abstract DataTable Post_processing(DataTable dt);
        
    }
}

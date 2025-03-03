using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Text.Json;
using System.Data.OleDb;
using Newtonsoft.Json;
using System.Data;
using Option_Pricer.Configurations;

namespace Option_Pricer.Utils
{
    internal class FilesManagement
    {   
        // REMOVE if you don't do anything with the _date 
        private DateTime date;

        public DateTime Date { get =>  date; set => date = value; }

        public FilesManagement(DateTime date) 
        {
            Date = date;
        }

        public DataTable CsvToDataTable(string file_name)
        {
            DataTable dt = new DataTable();
            using (StreamReader reader = new StreamReader(CustomConfiguration._mocks_path + file_name)) 
            {
                string[] headers = reader.ReadLine().Split(';'); 
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!reader.EndOfStream) 
                {
                    string[] rows = reader.ReadLine().Split(';'); 
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i].Trim();
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt; 
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Option_Pricer.Computations
{
    internal abstract class ComputationGlobal
    {
        private DateTime _date;
        protected DataTable? _dt;

        public DateTime Date { get => _date; set => _date = value; }

        public ComputationGlobal(DateTime date) 
        {
            Date = date;
        }

        //Main methode for the calculator
        public double Computation()
        {
            Console.WriteLine("Starting: Computation for " + this.GetType().Name); 
            this._dt = GetDatas();


            Console.WriteLine("Starting: Processing for " + this.GetType().Name);
            double result = Processing();


            Console.WriteLine("Done: Computation for " + this.GetType().Name);
            return result; 

        }

        protected abstract DataTable GetDatas();

        protected abstract double Processing();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public Engine Engine { get; set; }
        public int Year { get; set; }
        public string EngineType { get; set; }
        public Car() { }
        public Car(string model, Engine engine, int year)
        {
            Model = model;
            Engine = engine;
            Year = year;
            
        }
        public void engineTypeDo()
        {
            if (Engine.Model.Contains("TDI"))
                EngineType = "diesel";
            else
                EngineType = "petrol";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U22
{
    class Fridges
    {
        public string Facturer { get; set; }        //Facturer of the fridges
        public string Model { get; set; }           //The model of the fridges
        public double Capacity { get; set; }        //The capacity of the fridges
        public string EnergyClass { get; set; }     //The energy class of the fridges
        public string AssemblyType { get; set; }    //The assembly type of the fridges
        public string Color { get; set; }           //The color of the fridges
        public string Attribute { get; set; }       //The attribute ("Has a freezer") of the fridges
        public double Cost { get; set; }            //The cost of the fridges       

        //Constructor with parameters
        public Fridges(
            string facturer,
            string model,
            double capacity,
            string energyClass,
            string assemblyType,
            string color,
            string attribute,
            double cost)
        {
            Facturer = facturer;
            Model = model;
            Capacity = capacity;
            EnergyClass = energyClass;
            AssemblyType = assemblyType;
            Color = color;
            Attribute = attribute;
            Cost = cost;
        }
        public override String ToString()
        {
            return String.Format("|{0,20}|{1,12}|{2,12}|{3,6}|{4,8}|{5,9}|{6,10}|{7,9}",
    Facturer, Model, Capacity, EnergyClass, AssemblyType, Color, Attribute, Cost);
        }
    }
}

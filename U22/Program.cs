using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace U22
{
    class Program
    {
        const string whiteFileName = @"Baltieji.csv";
        const string boshFileName = @"Bosh.csv";
        const string fileName = @"Data.csv";
        const string dataFile = @"DataCopy.txt";
        const int MaxNumberOfFridges = 50;
        public const int NumberOfBranches = 3;
        /// <summary>
        /// Main program where all methods are called
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //References a Program() class
            Program p = new Program();

            Branch[] branches = new Branch[NumberOfBranches];
            branches[0] = new Branch("Kauno šaldytuvai", "Barstyčio g. 21", "+3706750000");
            branches[1] = new Branch("Vilniaus šaldytuvai", "Garstyčio g. 21", "+3706550000");
            branches[2] = new Branch("Šiaulių šaldytuvai", "Varstyčio g. 21", "+37067444000");

            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());

            //Lists for the fridge class
            FridgeContainer fridges = new FridgeContainer(MaxNumberOfFridges);
            FridgeContainer fridgesCopy = new FridgeContainer(MaxNumberOfFridges);
            FridgeContainer differentCapacity = new FridgeContainer(MaxNumberOfFridges);
            FridgeContainer FittingFridges = new FridgeContainer(MaxNumberOfFridges);
            FridgeContainer LowCostFridges = new FridgeContainer(MaxNumberOfFridges);
            FridgeContainer WhatToPrint = new FridgeContainer(MaxNumberOfFridges);
            FridgeContainer bosh = new FridgeContainer(MaxNumberOfFridges);

            //Reads the data of the file
            p.ReadAllData(files, branches);
            //Prints the fridge data to a .txt file
            p.PrintData(branches, dataFile);
            //Creates a copy of the fridge list so it does not have it's parameters changed
            fridgesCopy = fridges;
            //Writes to the console what will be written
            Console.WriteLine("Different fridge capacities:");
            //Writes to the console all the different capacities of the fridges
            p.WritetoConsoleDifferentCapacities(branches);
            //Creates an empty line
            Console.WriteLine();
            //Writes to the console what will be written
            Console.WriteLine("Cheapest fridges that meet the requirements:");
            //Assigns the list fridges with the parameters of the method FittingFridges
            fridges = p.FittingFridges(fridgesCopy);
            //Writes to the console the facturer, model, capacity and cost of the fridges that meet the requirements
            p.PrintCheapestToConsole(branches);
            //Writes to a file all of the data of white fridges that are white and have an A++ energy class
            p.PrintAll(fridgesCopy, whiteFileName, "White");
            //Writes to a file all of the data of fridges that are manufactured by Bosh
            p.PrintAll(fridgesCopy, boshFileName, "Bosh");
            Console.ReadKey();
        }
        private Branch GetBranchByShopsName(Branch[] branches, string shopsName)
        {
            for (int i = 0; i < NumberOfBranches; i++)
            {
                if (branches[i].ShopsName == shopsName)
                {
                    return branches[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Reads the file and sets values to the fridge class
        /// </summary>
        /// <returns>The data about the fridges from the csv file</returns>
        void ReadData(string fileName, Branch[] branches)
        {
            string shopsName = null;
            using (StreamReader reader = new StreamReader(@fileName))
            {
                string line = null;
                line = reader.ReadLine();
                if (line != null)
                    shopsName = line;
                Branch branch = GetBranchByShopsName(branches, shopsName);
                if (branch == null)
                    return;
                string address = reader.ReadLine();
                string phoneNumber = reader.ReadLine();
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(';');
                    string facturer = values[0];
                    string model = values[1];
                    double capacity = double.Parse(values[2]);
                    string energyClass = values[3];
                    string assemblyType = values[4];
                    string color = values[5];
                    string attribute = values[6];
                    double cost = double.Parse(values[7]);
                    Fridges fridge = new Fridges(facturer, model, capacity, energyClass, assemblyType, color, attribute, cost);
                    branch.fridges.AddFridge(fridge);
                }
            }

        }
        void ReadAllData (string[] files, Branch[] branches)
        {
             foreach (string file in files)
             {
                 ReadData(file, branches);
             }
        }
        /// <summary>
        /// Finds all the different capacities of the fridges
        /// </summary>
        /// <param name="fridges">List of the fridge class</param>
        /// <returns>A list of different capacities</returns>
        FridgeContainer DifferentCapacityFridges(Branch branches)
        {
            FridgeContainer differentCapacity = new FridgeContainer(MaxNumberOfFridges);
            differentCapacity.AddFridge(branches.fridges.GetFridge(0));
            for (int i = 0; i < branches.fridges.Count; i++)
            {
                int counter = 0;
                for (int j = 0; j < differentCapacity.Count; j++)
                {
                    if (differentCapacity.GetFridge(j).Capacity == branches.fridges.GetFridge(i).Capacity)
                    {
                        counter = 1;
                        break;
                    }
                }
                if (counter != 1)
                {
                    differentCapacity.AddFridge(branches.fridges.GetFridge(i));
                }
            }
            return differentCapacity;
        }
        /// <summary>
        /// Writes the different capacities to the console
        /// </summary>
        /// <param name="fridges">All the data from the fridge class list</param>
        void WritetoConsoleDifferentCapacities(Branch[] branches)
        {
            for (int j = 0; j < branches.Length; j++)
            {
                Console.WriteLine("--------");
                FridgeContainer differentCapacityFridges = DifferentCapacityFridges(branches[j]);
                for (int i = 0; i < differentCapacityFridges.Count; i++)
                {
                    Console.Write("| {0,-5}|", branches);
                    Console.WriteLine();
                }
            }
            Console.WriteLine("--------");
        }

        /// <summary>
        /// Checks to see if there are fridges that meet the specific requirements
        /// </summary>
        /// <param name="fridges">All the data from the fridge class list</param>
        /// <returns>A list of fridges that have met the requirements</returns>
        FridgeContainer FittingFridges(FridgeContainer fridges)
        {
            FridgeContainer FittingFridges = new FridgeContainer(MaxNumberOfFridges);
            for (int i=0; i<fridges.Count; i++)
            {
                if (fridges.GetFridge(i).Attribute == "+" && fridges.GetFridge(i).AssemblyType == "Pastatomas")
                {
                    FittingFridges.AddFridge(fridges.GetFridge(i));
                }
            }
            return FittingFridges;
        }

        /// <summary>
        /// Finds the minimum cost of fridges that meet the requirements
        /// </summary>
        /// <param name="fridges">All the data from the fridge class list</param>
        /// <returns>The minimum cost of fridges</returns>
        double MinCost(FridgeContainer fridges)
        {
            double min;
            min = fridges.GetFridge(0).Cost;
            FridgeContainer tinkamiFridgai = FittingFridges(fridges);
            for (int i=1; i< tinkamiFridgai.Count; i++)
            {
                if (min > fridges.GetFridge(i).Cost)
                {
                    min = fridges.GetFridge(i).Cost;
                }
            }
            return min;
        }

        /// <summary>
        /// Finds the fridge or fridges that cost the least
        /// </summary>
        /// <param name="fridges">All the data from the fridge class list</param>
        /// <returns>A list of the cheapest fridges</returns>
        FridgeContainer LowCostFridges(Branch branches)
        {
            FridgeContainer LowCostFridges = new FridgeContainer(MaxNumberOfFridges);
            double min = MinCost(branches.fridges);
            for (int i = 0; i < FittingFridges(branches.fridges).Count; i++)
            {
                if (branches.fridges.GetFridge(i).Cost == min)
                {
                    LowCostFridges.AddFridge(branches.fridges.GetFridge(i));
                }
            }
            return LowCostFridges;
        }

        /// <summary>
        /// Prints the cheapest fridges to the console
        /// </summary>
        /// <param name="fridges">All the fridges from the fridge class list</param>
        void PrintCheapestToConsole(Branch[] branches)
        {
            for (int j = 0; j < branches.Length; j++)
            {

                FridgeContainer Cheap = LowCostFridges(branches[j]);
                if (Cheap.Count == 0)
                {
                    Console.WriteLine(" ---------------------------------- ");
                    Console.WriteLine("| No fridges meet the requirements |");
                    Console.WriteLine(" ---------------------------------- ");
                }
                else
                {
                    Console.WriteLine(" ------------------------------- ");
                    for (int i = 0; i < Cheap.Count; i++)
                    {
                        Console.WriteLine("|Facturer | {0,-20}|\n|Model    | {1,-20}|\n|Capacity | {2,-20}|\n|Cost     | {3,-20}|", branches[j].fridges.GetFridge(i).Facturer, branches[j].fridges.GetFridge(i).Model, branches[j].fridges.GetFridge(i).Capacity, branches[j].fridges.GetFridge(i).Cost);
                        Console.WriteLine(" ------------------------------- ");
                    }
                }
            }
        }

        /// <summary>
        /// Prints the data to a .csv file
        /// </summary>
        /// <param name="fridges">All the fridges from the fridge class list</param>
        void PrintAll(FridgeContainer fridges, string file, string print)     //The file variable is a constant that saves the result file name 
        {
            using (StreamWriter whiteWriter = new StreamWriter(file))
            {
                whiteWriter.WriteLine("Facturer; Model; Capacity; Energy class; Assembly type; Color; Has a freezer; Cost;");
                for (int i=0; i< WhatToPrint(fridges, print).Count; i++)
                {
                    whiteWriter.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7};", fridges.GetFridge(i).Facturer, fridges.GetFridge(i).Model, fridges.GetFridge(i).Capacity, fridges.GetFridge(i).EnergyClass, fridges.GetFridge(i).AssemblyType, fridges.GetFridge(i).Color, fridges.GetFridge(i).Attribute, fridges.GetFridge(i).Cost);
                }
            }
        }

        /// <summary>
        /// Determines which file to write out
        /// </summary>
        /// <param name="fridges">All the fridges from the fridge class list</param>
        /// <param name="print">A string that tells the method what to print</param>
        /// <returns>A list of fridges that need to be printed out</returns>
        FridgeContainer WhatToPrint(FridgeContainer fridges, string print)
        {
            FridgeContainer WhatToPrint = new FridgeContainer(MaxNumberOfFridges);
            if (print == "White")
                for(int i=0; i< WhiteFridges(fridges).Count; i++)
                {
                    WhatToPrint.AddFridge(fridges.GetFridge(i));
                }
            return WhatToPrint;
        }

        /// <summary>
        /// Creates a list of only white fridges
        /// </summary>
        /// <param name="fridges">All the fridges in the fridge class list</param>
        /// <returns>A list of all fridges that are white and have an A++ energy class (returns an empty list if no fridge meets the requirements)</returns>
        FridgeContainer WhiteFridges(FridgeContainer fridges)
        {
            FridgeContainer WhiteFridges = new FridgeContainer(MaxNumberOfFridges);
            for (int i=0; i < fridges.Count; i++)
            {
                if (fridges.GetFridge(i).Color == "Balta" && fridges.GetFridge(i).EnergyClass == "A++")
                {
                    WhiteFridges.AddFridge(fridges.GetFridge(i));
                }
            }
            return WhiteFridges;
        }

        /// <summary>
        /// Creates a list of only Bosh fridges
        /// </summary>
        /// <param name="fridges">All the fridges from the fridge class list</param>
        /// <returns>A list of fridges that are factured by Bosh (returns an empty list if no fridges are factured by Bosh)</returns>

        /// <summary>
        /// Prints the data in a .txt file
        /// </summary>
        /// <param name="fridges">All the fridges from the Fridge class list</param>
        /// <param name="file">The name of the .txt file</param>
        void PrintData(Branch[] branches, string file)
        {
            for (int j = 0; j < branches.Length; j++)
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.WriteLine(" -------------------------------------------------------------------------------------------------------------- ");
                    writer.WriteLine("| Facturer |       Model        |Capacity|Energy class|    Assembly type   |   Color  |Has a freezer|   Cost   |");
                    writer.WriteLine("|----------|--------------------|--------|------------|--------------------|----------|-------------|----------|");
                    for (int i = 0; i < branches[j].fridges.Count; i++)
                    {
                        writer.WriteLine(branches[j].fridges.GetFridge(i).ToString());
                        //writer.WriteLine("|{0,-10}|{1,-20}|{2,8}|{3,-12}|{4,-20}|{5,-10}|{6,-13}|{7,10}|", branches[j].fridges.GetFridge(i).Facturer, branches[j].fridges.GetFridge(i).Model, branches[j].fridges.GetFridge(i).Capacity, branches[j].fridges.GetFridge(i).EnergyClass, branches[j].fridges.GetFridge(i).AssemblyType, branches[j].fridges.GetFridge(i).Color, branches[j].fridges.GetFridge(i).Attribute, branches[j].fridges.GetFridge(i).Cost);
                    }
                    writer.WriteLine(" -------------------------------------------------------------------------------------------------------------- ");
                }
            }
        }
    }
}

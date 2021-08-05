using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Project4V2
{
    class Program
    {
        static void Main(string[] args)
        {
            doWork();
        }

        static void doWork()
        {
            var suppliers = new[]
{
                new {SN = 1, SName = "Smith", Status = 20, City = "London"},
                new {SN = 2, SName = "Jones", Status = 10, City = "Paris"},
                new {SN = 3, SName = "Blake", Status = 30, City = "Paris"},
                new {SN = 4, SName = "Clark", Status = 20, City = "London"},
                new {SN = 5, SName = "Adams", Status = 30, City = "Athens"}
            };

            var parts = new[]
            {
                new { PN = 1, PName = "Nut", Color = "Red", Weight = 12, City = "London" },
                new { PN = 2, PName = "Bolt", Color = "Green", Weight = 17, City = "Paris" },
                new { PN = 3, PName = "Screw", Color = "Blue", Weight = 17, City = "Rome" },
                new { PN = 4, PName = "Screw", Color = "Red", Weight = 14, City = "London" },
                new { PN = 5, PName = "Cam", Color = "Blue", Weight = 12, City = "Paris" },
                new { PN = 6, PName = "Cog", Color = "Red", Weight = 19, City = "London" }
            };

            var shipments = new[]
             {
                new {SN = 1, PN = 1, Qty = 300},
                new {SN = 1, PN = 2, Qty = 200},
                new {SN = 1, PN = 3, Qty = 400},
                new {SN = 1, PN = 4, Qty = 200},
                new {SN = 1, PN = 5, Qty = 100},
                new {SN = 1, PN = 6, Qty = 100},
                new {SN = 2, PN = 1, Qty = 300},
                new {SN = 2, PN = 2, Qty = 400},
                new {SN = 3, PN = 2, Qty = 200},
                new {SN = 4, PN = 2, Qty = 200},
                new {SN = 4, PN = 4, Qty = 300},
                new {SN = 4, PN = 5, Qty = 400}
            };

            Console.WriteLine("Suppliers: ");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"SN: {supplier.SN}, SName: {supplier.SName}, Status: {supplier.Status}, City: {supplier.City} ");
            }
            Console.WriteLine("\nParts: ");
            foreach (var part in parts)
            {
                Console.WriteLine($"PN: {part.PN}, PName: {part.PName}, Color: {part.Color}, Weight: {part.Weight}, City: {part.City} ");
            }
            Console.WriteLine("\nShipments: ");
            foreach (var shipment in shipments)
            {
                Console.WriteLine($"SN: {shipment.SN}, PN: {shipment.PN}, Qty: {shipment.Qty}");
            }
            //Console.WriteLine();
            bool colorValid = true;

            //Checks if user input of color is valid and will loop if invalid.
            while (colorValid)
            {
                Console.Write("\nPlease input a color from the Parts list: ");
                string colors = Console.ReadLine();
                //Checks if user input is empty or null, and returns error message.
                while(string.IsNullOrEmpty(colors))
                {
                    Console.Write("\nCannot be empty, please input a color from the Parts list: ");
                    colors = Console.ReadLine();
                }
                //Allows user to input valid colors in any letter case.
                colors = char.ToUpper(colors[0]) + colors.Substring(1).ToLower();
                //If color is valid, queries distinct city with color.
                switch (colors)
                {
                    case "Red":
                        Console.WriteLine("\nCities with red parts:");
                        var partName = parts.Where(p => String.Equals(p.Color, colors)).Select(p => p.City).Distinct();
                        foreach (var part in partName)
                        {
                            Console.WriteLine($"City: {part}");
                        }
                        colorValid = false;
                        break;
                    case "Green":
                        Console.WriteLine("\nCities with green parts:");
                        partName = parts.Where(p => String.Equals(p.Color, colors)).Select(p => p.City).Distinct();
                        foreach (var part in partName)
                        {
                            Console.WriteLine($"City: {part}");
                        }
                        colorValid = false;
                        break;
                    case "Blue":
                        Console.WriteLine("\nCities with blue parts:");
                        partName = parts.Where(p => String.Equals(p.Color, colors)).Select(p => p.City).Distinct();
                        foreach (var part in partName)
                        {
                            Console.WriteLine($"City: {part}");
                        }
                        colorValid = false;
                        break;
                    default:
                        colorValid = true;
                        break;
                }
                if (colorValid)
                {
                    Console.WriteLine("Error, invalid input. Please try again.");
                }
            }

            Console.WriteLine("\nQuery supplier names in ascending order: ");
            var suppName = suppliers.OrderBy(supp => supp.SName).Select(supp => supp.SName);
            foreach (var suppNames in suppName)
            {
                Console.WriteLine($"SName: {suppNames}");
            }

            Console.Write("\nPlease Enter a SN from the Shipments list: ");
            //Checks if input is a valid integer and returns true.
            bool valid = int.TryParse(Console.ReadLine(), out int sNum) && sNum > 0 && sNum < 5;

            //Loops if user has an invalid input
            while (!valid)
            {
                Console.Write("Error, please enter the integer value from the SN on the Shipments list: ");
                valid = int.TryParse(Console.ReadLine(), out sNum) && sNum > 0 && sNum < 5;
            }

            //Using common key "PN" from shipments and parts arrays to join and select "PName" and "Qty".
            var shipNames = shipments.Where(s => int.Equals(s.SN, sNum)).Select(s => new { s.SN, s.PN, s.Qty }).Join(parts, partN => partN.PN, shipN => shipN.PN, (partN, shipN) => new { partN.SN, partN.PN, partN.Qty, shipN.PName });
            Console.WriteLine("\nPName and Quantity: ");
            foreach (var shipName in shipNames)
            {
                Console.WriteLine($"PName: {shipName.PName}, Qty: {shipName.Qty}");
            }
            
            ToContinue();
            
        }
        static void ToContinue()
        {
            Console.Write("Input 'x' to exit. Otherwise, press 'Enter' :");
            string keepGoing = Console.ReadLine().ToLower(); 
            if (keepGoing == "x")
            {
                Environment.Exit(1);
            }
            else
            {
                doWork();
            }

        }

    }
}

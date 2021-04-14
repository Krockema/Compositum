using System;
using System.Collections.Generic;
using System.Linq;
using Compositum.Compositum;
using Compositum.Database;
using Compositum.Database.Tables;
using Compositum.LinqExtension;
using Compositum.Register;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Compositum
{
    class Program
    {
        static void Main(string[] args)
        {
            // simple();   // Compositum // Connected Objects, recursive yield Enumeration
            // counter();  // Compositum // Array Counter, NO Recursion! (Every recursion can be replaced by Iteration)
            // linqy();    // Cartesian  // Using Build in functions of Linq to build cartesian Product
            // complex();  // OR as well as AND Connected Composites // Combining the Cartesian and the recursive walk through the composite
            InitDB();

        }

        static void InitDB()
        {

            Console.WriteLine("/* ----------------------------------------------------");
            Console.WriteLine("----  Seeding local Database --------------------------");
            Console.WriteLine("-----------------------------------------------------*/");
            Console.WriteLine("");
            // Local DB Connection string
            var compositeDb = CompositeDb.GetContext(
                "Server=(localdb)\\mssqllocaldb;Database=Composite;Trusted_Connection=True;MultipleActiveResultSets=true");
            // seed Created Database with one model.
            CompositeDb.DbSeed(compositeDb);

            Console.WriteLine("/* ----------------------------------------------------");
            Console.WriteLine("----  retrieve the data from database -----------------");
            Console.WriteLine("-----------------------------------------------------*/");
            Console.WriteLine("");
            var start = compositeDb.Composites.Single(x => x.ParentId == null);
            compositeDb.GetStructureRecursively(start, start.Id).Wait();
            start.Print();

            Console.WriteLine("/* ----------------------------------------------------");
            Console.WriteLine("----  split alternatives into two separate tree's -----");
            Console.WriteLine("-----------------------------------------------------*/");
            Console.WriteLine("");
            var alternatives = start.GetEnumerableMember(1);


            Console.WriteLine("/* ----------------------------------------------------");
            Console.WriteLine("----  walk through each alternatives ------------------");
            Console.WriteLine("-----------------------------------------------------*/");
            Console.WriteLine("");
            // note: Json.Serializer (like in complex() example) does not work here due to the cyclomatic references

            var count = 0;
            foreach (var alternative in alternatives)
            {
                Console.Write("\r\n alternative "+ count++ + " : ");
                var properties =  ElementaryComposite.GetFlatComposites(alternative.GetEnumerator());
                foreach (var item in properties)
                {
                    Console.Write(item.Name+ ", ");
                }
            }

            Console.ReadKey();
        }

        static void simple()
        {
            Console.WriteLine("Hello Zählwerk!");
            // gegeben int[] input = {2, 4, 3};
            var v1 = new Vector(2, null);
            var v2 = new Vector(4, v1);
            var v3 = new Vector(3, v2);

            foreach (var item in v3.GetNext())
            {
                Console.WriteLine(JsonSerializer.Serialize(item));
            }

            Console.ReadKey();
        }

        static void counter()
        {
            Counter counter = new Counter(new int[] {2, 4, 3, 6});
            while (counter.hasNext())
            {
                Console.WriteLine(JsonSerializer.Serialize(counter.getNext()));
            }
            Console.ReadKey();
        }

        static void complex()
        {
            // Create a Base Level

            CompositeAnd root = new CompositeAnd("Root");
            var productionParameter = new CompositeAnd("ProductionParameterName");
            var environment = new CompositeOr("Environment");
            root.Add(productionParameter);
            root.Add(environment);

            // Start Environmnet 
            var priorityRule = new CompositeOr("Priorule", new [] { "Schlupf", "FiFo", "KoZ" });
            var decentral = new CompositeAnd( "Dezentral");
            decentral.Add(priorityRule);

            var lotsize = new CompositeOr("Losgroesse", new [] { 1, 5, 10 });
            var planningInterval = new CompositeOr("Planungsintervall");
            planningInterval.Add(@from: 8,to: 24, interval: 16);
            
            var central = new CompositeAnd("Zentral");
            central.Add(lotsize);
            central.Add(planningInterval);

            environment.Add(central);
            environment.Add(decentral);
            // End Environmnet 

            // Start Production Parameter 
            var deadline = new CompositeOr("Liefertermin", new [] {3, 5});
            var interArrivalTime = new CompositeOr("Zwischenankunftszeit", new [] { 0.025, 0.03});
            
            productionParameter.Add(deadline);
            productionParameter.Add(interArrivalTime);
            // End Production Parameter 

            var items = root.GetEnumerableMember(depth: 1);
            Console.WriteLine("Visited all nodes, Creating Parameter Sets");

            var count = 0;
            
            foreach (var x1 in items)
            {
                Console.WriteLine(JsonSerializer.Serialize(x1)); // To print one Line per Configuration 
                count++;
            }
             
            Console.WriteLine("Done; " + count + " possible configurations found!");
            // Wait for user
            Console.ReadKey(); 
        }

        static void linqy()
        {
            
            object[] letters = { new Leaf<int>("", 2),  new Leaf<int>("", 6), new Leaf<int>("", 8)  };
            
            int[] numbers = { 1, 2, 3, 4 };
            
            string[] colours = { "Red", "Blue" };


            List<object[]> chilren = new List<object[]>();
            chilren.Add(letters);
            
            object[] orConnected = {'A', 'B', 'C', 1, 2, 3, 4};
            chilren.Add(orConnected);


            var cartesianProduct = Cartesian.CartesianProduct<object>(chilren);
            foreach (var x1 in cartesianProduct)
            {
                Console.WriteLine(JsonSerializer.Serialize(x1));
            }
        }
    }
}


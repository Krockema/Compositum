using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Zaehlwerk.Compositum;
using Zaehlwerk.LinqExtension;
using Zaehlwerk.Register;

namespace Zaehlwerk
{
    class Program
    {
        static void Main(string[] args)
        {
            // simple(); // Compositum // Connected Objects, recursive yield Enumeration
            // counter(); // Compositum // Array Counter, NO Recursion! (Every recursion can be replaced by Iteration)
            // linqy();  // Cartesian // Using Build in functions of Linq to build cartesian Product
            complex(); // OR as well as AND Connected Composites // Combining the Cartesian and the recursive walk through the composite
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
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }

            Console.ReadKey();
        }

        static void counter()
        {
            Counter counter = new Counter(new int[] {2, 4, 3, 6});
            while (counter.hasNext())
            {
                Console.WriteLine(JsonConvert.SerializeObject(counter.getNext()));
            }
            Console.ReadKey();
        }

        static void complex()
        {
            // Create a tree structure

            CompositeAnd root = new CompositeAnd("root");
            var firstParameter = "TestOr_Time";
            var option1 = new Leaf(firstParameter, new Fact<object>(2880, "SimulationTime"));
            var option2 = new Leaf(firstParameter, new Fact<object>(1440, "SimulationTime"));
            
            var secondParameter = "TestOr_Arrival";
            var option3 = new Leaf(firstParameter, new Fact<object>(0.027, "OrderArrivalRate"));
            var option4 = new Leaf(firstParameter, new Fact<object>(0.025, "OrderArrivalRate"));

            var thirdParameter = "TestOr_Type";
            var option5 = new Leaf(thirdParameter, new Fact<object>("Central", "SimType"));
            var option6 = new Leaf(thirdParameter, new Fact<object>("Decentral", "SimType"));
            var option7 = new Leaf(thirdParameter, new Fact<object>("Queued", "SimType"));

            CompositeOr alt_start_1 = new CompositeOr(firstParameter);
            alt_start_1.Add(option1);
            alt_start_1.Add(option2);

            CompositeOr alt_start_2 = new CompositeOr(secondParameter);
            alt_start_2.Add(option3);
            alt_start_2.Add(option4);

            CompositeAnd andConnected = new CompositeAnd("TestAndConnector");
            andConnected.Add(alt_start_1);
            andConnected.Add(alt_start_2);

            CompositeOr orConnected = new CompositeOr(thirdParameter);
            orConnected.Add(option5);
            orConnected.Add(option6);
            orConnected.Add(option7);
            
            root.Add(andConnected);
            root.Add(orConnected);
            var items = root.GetNext(1);

            Console.WriteLine("Visited all nodes, Creating Parameter Sets");

            var cartesianProduct = Cartesian.CartesianProduct<Object>(items);

            foreach (var x1 in cartesianProduct)
            {
                Console.WriteLine(JsonConvert.SerializeObject(x1));
            }
            
 
            Console.WriteLine("Done");
            // Wait for user

            Console.ReadKey(); 
        }

        static void linqy()
        {
            
            object[] emty = { };

            Fact<int>[] letters = { new Fact<int>(2, ""),  new Fact<int>(6, ""), new Fact<int>(3, "") };
            
            int[] numbers = { 1, 2, 3, 4 };
            
            string[] colours = { "Red", "Blue" };


            List<object[]> chilren = new List<object[]>();
            chilren.Add(letters);
            
            object[] orConnected = {'A', 'B', 'C', 1, 2, 3, 4};
            chilren.Add(orConnected);


            var cartesianProduct = Cartesian.CartesianProduct<Object>(chilren);
            foreach (var x1 in cartesianProduct)
            {
                Console.WriteLine(JsonConvert.SerializeObject(x1));
            }
        }
    }
}

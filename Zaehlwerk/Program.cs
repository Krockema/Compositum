using System;
using System.Collections.Generic;
using System.Text.Json;
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
            var emvironmentName = "Environment";
            var productionParameterName = "ProductionParameterName";
            var productionParameter = new CompositeAnd(productionParameterName); 
            var environment = new CompositeOr(emvironmentName);
            root.Add(productionParameter);
            root.Add(environment);
            
            // Start Environmnet 

            var prioRuleName = "Priorule";
            var prioRule = new CompositeOr(prioRuleName);
            prioRule.Add(new Leaf(prioRuleName, new Fact<object>("Schlupf", prioRuleName)));
            prioRule.Add(new Leaf(prioRuleName, new Fact<object>("FiFo", prioRuleName)));
            prioRule.Add(new Leaf(prioRuleName, new Fact<object>("KOZ", prioRuleName)));


            var decentralName = "Dezentral";
            var decentral = new CompositeAnd(decentralName);
            decentral.Add(prioRule);

            var losgroeseName = "Losgröße";
            var losgroese = new CompositeOr(losgroeseName);
            losgroese.Add(new Leaf(losgroeseName, new Fact<object>(1, losgroeseName)));
            losgroese.Add(new Leaf(losgroeseName, new Fact<object>(5, losgroeseName)));
            losgroese.Add(new Leaf(losgroeseName, new Fact<object>(10, losgroeseName)));

            var planningIntervalName = "Planungsintervall";
            var planningInterval = new CompositeOr(planningIntervalName);
            planningInterval.Add(new Leaf(planningIntervalName, new Fact<object>(8, planningIntervalName)));
            planningInterval.Add(new Leaf(planningIntervalName, new Fact<object>(24, planningIntervalName)));

            var centralName = "Zentral";
            var central = new CompositeAnd(centralName);
            central.Add(losgroese);
            central.Add(planningInterval);

            environment.Add(central);
            environment.Add(decentral);

            // End Environmnet 

            // Start Production Parameter 
            
            var deadlineName = "Liefertermin";
            var deadline = new CompositeOr(deadlineName);
            deadline.Add(new Leaf(deadlineName, new Fact<object>(1, deadlineName)));
            deadline.Add(new Leaf(deadlineName, new Fact<object>(5, deadlineName)));

            var InterArivalTimeName = "Zwischenankunftszeit";
            var InterArivalTime = new CompositeOr(InterArivalTimeName);
            InterArivalTime.Add(new Leaf(InterArivalTimeName, new Fact<object>(0.025, InterArivalTimeName)));
            InterArivalTime.Add(new Leaf(InterArivalTimeName, new Fact<object>(0.03, InterArivalTimeName)));

            productionParameter.Add(deadline);
            productionParameter.Add(InterArivalTime);

            // End Production Parameter 

            var items = root.GetAll(1);

            Console.WriteLine("Visited all nodes, Creating Parameter Sets");


            foreach (var x1 in items)
            {
                Console.WriteLine(JsonSerializer.Serialize(x1));
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
                Console.WriteLine(JsonSerializer.Serialize(x1));
            }
        }
    }
}

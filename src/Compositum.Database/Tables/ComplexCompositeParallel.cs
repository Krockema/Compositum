using System;
using System.Collections.Generic;
using Compositum.LinqExtension;

namespace Compositum.Database.Tables
{
    public class ComplexCompositeParallel : Composite
    {
        public ComplexCompositeParallel(string name) : base(name)
        {
        }
        public override IEnumerable<IEnumerable<object>>  GetEnumerableMember(int depth)
        {
            Console.WriteLine(new String('-', depth) + " AND " + Name);
            var combination = new List<IEnumerable<object>>();
            foreach (var concat in Children)
            {
                var entries = new List<object>();
                foreach (var elements in concat.GetEnumerableMember(depth + 2))
                {
                    entries.Add(elements);
                }
                combination.Add(entries);
            }
            var returns = Cartesian.CartesianProduct(combination);
            return returns;
        }
    }
}
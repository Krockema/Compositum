using System;
using System.Collections.Generic;
using Compositum.LinqExtension;

namespace Compositum.SubTypes
{
    class CompositeAnd : Composite

    {
        public CompositeAnd(string name)
            : base(name)
        {
        }
        public CompositeAnd(string name, dynamic array)
            : base(name)
        {
            this.Add(array);
        }

        public override IEnumerable<IEnumerable<object>>  GetEnumerableMember(int depth)
        {
            Console.WriteLine(new String('-', depth) + " AND " + name);
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
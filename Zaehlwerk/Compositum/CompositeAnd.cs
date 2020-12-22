using System;
using System.Collections.Generic;
using System.Linq;
using Zaehlwerk.LinqExtension;

namespace Zaehlwerk.Compositum
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
            var combination = new List<List<object>>();
            foreach (var concat in Children)
            {
                var entries = new List<object>();
                foreach (var elements in concat.GetEnumerableMember(depth + 2))
                {
                    entries.Add(elements.ToList());
                }
                combination.Add(entries);

            }
            var returns = BuildCartesian(combination);
            return returns;
        }

        private IEnumerable<IEnumerable<object>> BuildCartesian(List<List<object>> items)
        {
            return Cartesian.CartesianProduct<Object>(items);
        }
    }
}
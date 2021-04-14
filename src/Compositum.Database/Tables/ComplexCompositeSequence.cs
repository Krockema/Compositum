using System;
using System.Collections.Generic;
using System.Linq;
using Compositum.LinqExtension;

namespace Compositum.Database.Tables
{
    public class ComplexCompositeSequence : Composite
    {
        public ComplexCompositeSequence(string name) : base(name)
        {
        }

        public void SortIfPossible()
        {
            if (Children.All(x => x.Predecessor == null) && Children.Count > 1)
                return; // No sort possible
            
            
            // new children
            List<Composite> children = new ();
            // First Element has no Predecessor, so fetch it.
            var next = Children.Single(x => x.Predecessor == null && x.Successor != null);
            children.Add(item: next);
            
            //as long as there are successors follow the path.
            while (next.Successor != null)
            {
                next = next.Successor;
                children.Add(next);
            }

            Children = children;
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
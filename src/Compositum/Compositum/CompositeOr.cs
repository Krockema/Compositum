using System;
using System.Collections.Generic;

namespace Compositum.Compositum
{
    class CompositeOr : Composite

    {
        // Constructor
        public CompositeOr(string name)
            : base(name)
        {
        }
        public CompositeOr(string name, dynamic array)
            : base(name)
        {
            this.Add(array);
        }

        public override IEnumerable<IEnumerable<object>> GetEnumerableMember(int depth)
        {
            Console.WriteLine(new String('-', depth) + " OR " + name);
            var entries = new List<IEnumerable<object>>();
            foreach (var child in Children)
            { 
               foreach (var item in child.GetEnumerableMember(depth + 2))                   
               {
                   entries.Add(item);
               }
            }
            return entries;
        }
    }
}
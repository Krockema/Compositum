using System;
using System.Collections.Generic;
using System.Linq;

namespace Zaehlwerk.Compositum
{
    class CompositeOr : Composite

    {
        // Constructor
        public CompositeOr(string name)
            : base(name)
        {
        }
        public override void Add(Component c)
        {
            Children.Add(c);
        }

        public CompositeOr(string name, dynamic array)
            : base(name)
        {
            this.Add(array);
        }

        public override IEnumerable<IEnumerable<object>> GetEnumerableMember(int depth)
        {
            Console.WriteLine(new String('-', depth) + " OR " + name);
            var entries = new List<List<object>>();
            //var comp = new List<Component>();
            foreach (var child in Children)
            { 
               var comp = new List<Component>();
               foreach (var item in child.GetEnumerableMember(depth + 2))                   
               {
                   entries.Add(item.ToList());
               }
               
            }
            return entries;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zaehlwerk
{
    class CompositeOr : Component

    {
        private List<Component> _children = new List<Component>();
        // Constructor
        public CompositeOr(string name)
            : base(name)
        {
        }
        public override void Add(Component c)
        {
            _children.Add(c);
        }

        public override List<Component[]>  GetNext(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
            var entries = new List<Component[]>();
            var comp = new List<Component>();
            foreach (var child in _children)
            {
               foreach (var item in child.GetNext(depth + 2))                   
               {
                  comp.AddRange(item);
               }
            }
            entries.Add(comp.ToArray());
            return entries;
        }
    }
}
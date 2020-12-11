﻿using System;
using System.Collections.Generic;
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

        public override IEnumerable<IEnumerable<object>> GetAll(int depth)
        {
            Console.WriteLine(new String('-', depth) + " OR " + name);
            var entries = new List<List<object>>();
            //var comp = new List<Component>();
            foreach (var child in _children)
            { 
               var comp = new List<Component>();
               foreach (var item in child.GetAll(depth + 2))                   
               {
                   entries.Add(item.ToList());
               }
               
            }
            return entries;
        }
    }
}
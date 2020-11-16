using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using Zaehlwerk.LinqExtension;

namespace Zaehlwerk
{
    class CompositeAnd : Component

    {
        private Component[] _childConcatinated = new Component[0];
        private List<Component> _children = new List<Component>();
        // Constructor

        public CompositeAnd(string name)
            : base(name)
        {
        }

        public override void Add(Component c)
        {
            _children.Add(c);
            _childConcatinated = _childConcatinated.Concat(new []{c}).ToArray();
        }

        public override IEnumerable<IEnumerable<object>>  GetAll(int depth)
        {
            Console.WriteLine(new String('-', depth) + " AND " + name);
            var combination = new List<List<object>>();
            foreach (var concat in _children)
            {
                var entries = new List<object>();
                foreach (var elements in concat.GetAll(depth + 2))
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
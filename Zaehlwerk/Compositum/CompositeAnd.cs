using System;
using System.Collections.Generic;
using System.Linq;

namespace Zaehlwerk
{
    class CompositeAnd : Component

    {
        private Component[] _childConcatinated = new Component[0];
        // Constructor

        public CompositeAnd(string name)
            : base(name)
        {
        }

        public override void Add(Component c)
        {
            //_childConcatinated.Append(c);
            _childConcatinated = _childConcatinated.Concat(new []{c}).ToArray();
        }

        public override List<Component[]>  GetNext(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
            var entries = new List<Component[]>();
            foreach (var concat in _childConcatinated)
            {
                entries.AddRange(concat.GetNext(depth + 2));
            }
            return entries;
        }
    }
}
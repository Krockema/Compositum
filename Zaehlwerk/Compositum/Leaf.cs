using System;
using System.Collections.Generic;
using Zaehlwerk.Compositum;

namespace Zaehlwerk
{
    class Leaf : Component

    {
        // Constructor
        public Fact<object> _fact { get; private set; }

        public Leaf(string name, Fact<object> fact)
            : base(name)
        {
            _fact = fact;
        }
 
        public override void Add(Component c)
        {
            // not possible to add more to a leave
        }

        public override List<Component[]> GetNext(int depth)
        {
            Console.WriteLine(new String('-', depth) + _fact.Value);
            return new List<Component[]>() { new []{ this } };
        }
    }
}
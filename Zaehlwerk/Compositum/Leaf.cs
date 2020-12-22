using System;
using System.Collections.Generic;

namespace Zaehlwerk.Compositum
{
    class Leaf<T> : Component
    {
        // Constructor
        public T Value { get; private set; }
        public string Name => base.Name();

        public Leaf(string name, T value) 
            : base(name) 
        {
            Value = value;
        }
 
        public override void Add(Component c)
        {
            // not possible to add more to a leave
        }

        public override IEnumerable<IEnumerable<object>> GetEnumerableMember(int depth)
        {
            Console.WriteLine(new String('-', depth) + " value: " + Value.ToString() );
            return new List<Component[]>() { new []{ this } };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Compositum.Compositum
{
    [DebuggerDisplay("{Name}:{Value}")]
    class Leaf<T> : Component
    {
        // Constructor
        public T Value { get; private set; } // Can store any Object
        public new string Name => base.Name(); // Required for Json Output

        public Leaf(string name, T value) 
            : base(name) 
        {
            Value = value;
        }

        public override IEnumerable<IEnumerable<object>> GetEnumerableMember(int depth)
        {
            Console.WriteLine(new String('-', depth) + " value: " + Value.ToString() );
            return new List<Component[]>() { new []{ this } };
        }
    }
}
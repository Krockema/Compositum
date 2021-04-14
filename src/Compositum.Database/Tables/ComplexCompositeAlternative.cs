using System;
using System.Collections.Generic;

namespace Compositum.Database.Tables
{
    public class ComplexCompositeAlternative : Composite
    {
        public ComplexCompositeAlternative(string name) : base(name)
        {
        }

        public override IEnumerable<IEnumerable<object>> GetEnumerableMember(int depth)
        {
            Console.WriteLine(new String('-', depth) + " OR " + Name);
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
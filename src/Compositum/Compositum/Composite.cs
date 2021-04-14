using System;
using System.Collections.Generic;

namespace Compositum.Compositum
{
    internal abstract class Composite : Component
    {
        internal List<Component> Children = new List<Component>();
        protected Composite(string name)
            : base(name)
        {
        }
        public void Add(Component c)
        {
            Children.Add(c);
        }
        public void Add<T>(T[] array)
        {
            foreach (var item in array)
                Children.Add(new Leaf<T>(this.name, item));
        }

        public void Add(double from, double to, double interval)
        {
            for (;from <= to; from+= interval)
                Children.Add(new Leaf<double>(this.name, from));    
        }

        public override IEnumerable<IEnumerable<object>> GetEnumerableMember(int depth)
        {
            throw new NotImplementedException();
        }
    }
}

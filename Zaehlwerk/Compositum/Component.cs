using System.Collections.Generic;

namespace Zaehlwerk.Compositum
{ 
    abstract class Component
    {
        protected string name;
        // Constructor
        public Component(string name)
        {
            this.name = name;
        }
        public abstract IEnumerable<IEnumerable<object>> GetEnumerableMember(int depth);
        public string Name()
        {
            return this.name;
        }
    }
}
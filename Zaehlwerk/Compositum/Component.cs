using System.Collections.Generic;

namespace Zaehlwerk
{ abstract class Component

    {
        protected string name;
        // Constructor
        public Component(string name)
        {
            this.name = name;
        }
 
        public abstract void Add(Component c);
        public abstract List<Component[]> GetNext(int depth);
        public string Name()
        {
            return this.name;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Compositum.Database.Tables
{
    /// <summary>
    /// Base Composite Element
    /// </summary>
    public class ElementaryComposite : Composite
    {
        public ElementaryComposite(string name) : base(name) 
        {
        }
        public override IEnumerable<IEnumerable<object>> GetEnumerableMember(int depth)
        {
            Console.WriteLine(new String('-', depth) + " value: " + this.Name );
            return new List<Composite[]>() { new []{ this } };
        }

        public static List<ElementaryComposite> GetFlatComposites(IEnumerator<object> obj, List<ElementaryComposite> results = null)
        {
            results ??= new();
           
            while (obj.MoveNext())
            {
                if (obj.Current is IEnumerable<object> current)
                {
                    GetFlatComposites(current.GetEnumerator(), results);
                }
                else
                {
                    var elementary = obj.Current as ElementaryComposite;
                    results.Add(elementary);
                }
            }
            return results;
        }
    }
}
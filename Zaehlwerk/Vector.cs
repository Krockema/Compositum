using System.Collections.Generic;

namespace Zaehlwerk
{
    public class Vector
    {
        private int _maxSize { get; set; }
        private Vector _child { get; set; }
        public Vector(int maxSize, Vector child)
        {
            _maxSize = maxSize;
            _child = child;
        }
        private List<object> internalList(object item)
        {
            var l = new List<object>();
            l.Add(item);
            return l;
        }

        public IEnumerable<List<object>> GetNext()
        {
            
            for (int i = 0; i < _maxSize; i++)
            {
                if (_child == null)
                    yield return internalList(i);
                else
                {
                    foreach (var integer in _child.GetNext())
                    {
                        var c = internalList(i);
                        c.AddRange(integer);
                        yield return c;
                    }
                }
            }
        }
           
    }
}
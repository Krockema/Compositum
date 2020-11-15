namespace Zaehlwerk.Register
{
    public class Counter {
 
        private int[] upperBounds;
 
        private int[] count;
    
        public Counter(int[] upperBounds) {
            this.upperBounds = upperBounds;
            count = new int[upperBounds.Length];
        }
    
        public int[] getNext() {
            int i = count.Length - 1;
            while (i >= 0)
                if (count[i] < upperBounds[i] - 1) {
                    count[i]++;
                    for (int j = i + 1; j < count.Length; j++) count[j] = 0;
                    break; // pöhse, pöhse, aba kürza ;-)
                } else i--;
            return count;
        }
 
        public bool hasNext() {
            bool result = false;
            int i = 0;
            while (!result && i < count.Length) {
                result = result || count[i] < upperBounds[i] - 1;
                i++;
            }
            return result;
        }
 
    }
}
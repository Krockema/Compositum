namespace Zaehlwerk.Compositum
{
    public class Fact<T>
    {
        public T Value { get; set; }
        public string Name { get; set; }
        public Fact(T value, string name)
        {
            Value = value;
            Name = name;
        }

    }
}
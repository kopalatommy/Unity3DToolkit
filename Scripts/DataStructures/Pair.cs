namespace Toolkit.DataStructures
{
    public class Pair<T, U>
    {
        public T Key { get; set; }
        public U Value { get; set; }

        public Pair()
        {

        }

        public Pair(T key, U value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return "(" + Key.ToString() + ", " + Value.ToString() + ")";
        }
    }
}

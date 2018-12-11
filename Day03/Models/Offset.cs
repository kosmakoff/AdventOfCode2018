namespace Day03.Models
{
    public struct Offset
    {
        public int X;
        public int Y;

        public Offset(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}

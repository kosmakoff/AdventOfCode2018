namespace Day03.Models
{
    public class Claim
    {
        public int Id { get; }
        public Offset Offset { get; }
        public Size Size { get; }

        public Claim(int id, Offset offset, Size size)
        {
            Id = id;
            Offset = offset;
            Size = size;
        }

        public override string ToString()
        {
            return $"#{Id} @ {Offset}: {Size}";
        }

        public bool ContainsPoint(int x, int y)
        {
            return x >= Offset.X && x < Offset.X + Size.Width &&
                   y >= Offset.Y && y < Offset.Y + Size.Height;
        }

        public static bool Intersects(Claim claim1, Claim claim2)
        {
            int PatchLeft(Claim claim) => claim.Offset.X;
            int PatchRight(Claim claim) => claim.Offset.X + claim.Size.Width;
            int PatchTop(Claim claim) => claim.Offset.Y;
            int PatchBottom(Claim claim) => claim.Offset.Y + claim.Size.Height;

            return !(PatchLeft(claim1) >= PatchRight(claim2) ||
                   PatchLeft(claim2) >= PatchRight(claim1) ||
                   PatchTop(claim1) >= PatchBottom(claim2) ||
                   PatchTop(claim2) >= PatchBottom(claim1));
        }
    }
}

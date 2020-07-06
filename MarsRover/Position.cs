namespace MarsRover
{
    internal class Position : Point
    {
        public Directions Direction;

        public bool IsWithinTheArea(Point point)
        {
            return (point.X >= this.X && point.Y >= this.Y);
        }

        public enum Directions
        {
            N = 0,
            E = 1,
            S = 2,
            W = 3
        }
    }
}

namespace Brickwork.Bricks
{
    // class for vertically oriented bricks
    public class VerticalBrick : Brick
    {
        // constructor that determines the second block of the brick
        public VerticalBrick(int value, int row, int col) : base(value, row, col)
        {
            this.SecondCoordinate = this.Row + 1;
        }
    }
}

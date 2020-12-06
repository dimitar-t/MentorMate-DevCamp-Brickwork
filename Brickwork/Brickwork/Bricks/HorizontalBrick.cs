namespace Brickwork.Bricks
{
    // class for horizontally oriented bricks
    public class HorizontalBrick : Brick
    {
        // constructor that determines the second block of the brick
        public HorizontalBrick(int value, int row, int col) : base(value, row, col)
        {
            this.SecondCoordinate = this.Col + 1;
        }
    }
}

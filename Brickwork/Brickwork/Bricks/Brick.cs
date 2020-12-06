namespace Brickwork.Bricks
{
    // base class for the brick entity
    public abstract class Brick
    {
        // constructor with all parameters
        public Brick(int value, int row, int col)
        {
            this.Value = value;
            this.Row = row;
            this.Col = col;
        }
        
        // value of the brick
        public int Value { get; set; }

        // row of the first block of the brick
        public int Row { get; set; }

        // column of the first block of the brick
        public int Col { get; set; }

        // second coordinate according to the orientation
        public int SecondCoordinate { get; set; }
    }
}

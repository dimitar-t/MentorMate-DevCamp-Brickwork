using Brickwork.Bricks;
using System;
using System.Collections.Generic;

namespace Brickwork
{
    // class that represents a layer of bricks
    public class Wall
    {
        public Wall(int rows, int cols)
        {
            this.Rows = rows;
            this.Cols = cols;
            this.Bricks = new List<Brick>();
        }

        // list to store all bricks
        public List<Brick> Bricks { get; set; }

        public int Rows { get; set; }

        public int Cols { get; set; }

        // stores the count of the bricks in the wall
        public int Count 
        {
            get 
            {
                return this.Bricks.Count;
            }
        }

        // adds brick to the list
        public void Add(Brick brick)
        {
            this.Bricks.Add(brick);
        }

        // returns 2-d array representation of the wall
        public int[,] GetTemplate()
        {
            int[,] template = new int[this.Rows, this.Cols];

            for (int i = 0; i < this.Count; i++)
            {
                Brick currentBrick = this.Bricks[i];

                template[currentBrick.Row, currentBrick.Col] = currentBrick.Value;

                if (currentBrick.GetType() == typeof(HorizontalBrick))
                {
                    template[currentBrick.Row, currentBrick.SecondCoordinate] = currentBrick.Value;
                }
                else // brick is vertical
                {
                    template[currentBrick.SecondCoordinate, currentBrick.Col] = currentBrick.Value;
                }
            }

            return template;
        }

        // prints out the layer
        public void PrintWall()
        {
            int[,] template = this.GetTemplate();

            for (int row = 0; row < this.Rows; row++)
            {
                // print line of symbols
                for (int col = 0; col < this.Cols; col++)
                {
                    if (row - 1 >= 0)
                    {
                        if (template[row, col] != template[row - 1, col])
                        {
                            Console.Write("**");
                        }
                        else if (col < this.Cols - 1)
                        {
                            Console.Write(" *");
                        }
                    }
                }

                Console.WriteLine();

                // print brick values
                for (int col = 0; col < this.Cols; col++)
                {
                    if (col + 1 < this.Cols)
                    {
                        if (template[row, col] == template[row, col + 1]) // neighbour value is part of the same brick
                        {
                            Console.Write(template[row, col] + " ");
                        }
                        else
                        {
                            Console.Write(template[row, col] + "*");
                        }
                    }
                    else
                    {
                        Console.Write(template[row, col]);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}

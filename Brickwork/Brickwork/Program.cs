using Brickwork.Bricks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brickwork
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensions = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            int rows = dimensions[0];
            int cols = dimensions[1];

            if (!ValidateDimensions(rows, cols))
            {
                Console.WriteLine("The dimensions must be between 2 and 100 and they must be even numbers.");

                return;
            }

            Wall firstLayer = CreateFirstLayer(rows, cols);

            // validate the bricks
            if (firstLayer.Count != (rows * cols) / 2)
            {
                Console.WriteLine("All bricks must be of size 1x2 or 2x1.");

                return;
            }

            Wall secondLayer = CreateSecondLayer(firstLayer, rows, cols);

            if (!ValidateLayer(secondLayer))
            {
                Console.WriteLine("No appropriate second layer of the wall can be created.");
                Console.WriteLine(-1);

                return;
            }

            Console.WriteLine();
            secondLayer.PrintWall();

            return;
        }

        // validate the inputted dimensions
        static bool ValidateDimensions(int rows, int cols)
        {
            if (rows < 2 || rows > 100 || cols < 2 || cols > 100)
            {
                return false;
            }

            if (rows % 2 != 0 || cols % 2 != 0)
            {
                return false;
            }

            return true;
        }

        // input the first layer of bricks
        static List<List<int>> InputFirstLayer(int rows, int cols)
        {
            List<List<int>> bricksInput = new List<List<int>>();

            // input the first layer
            for (int i = 0; i < rows; i++)
            {
                List<int> row = Console.ReadLine().Split(" ").Select(int.Parse).ToList();

                // validate the size of the rows
                if (row.Count != cols)
                {
                    Console.WriteLine($"The rows must be of size {cols}.");

                    Environment.Exit(1);
                }

                bricksInput.Add(row);
            }

            return bricksInput;
        }

        // create the bricks for the first layer
        static Wall CreateFirstLayer(int rows, int cols)
        {
            List<List<int>> bricks = InputFirstLayer(rows, cols);

            Wall firstLayer = new Wall(rows, cols);

            // create the bricks for the first layer
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // check for horizontal brick
                    if (col < cols - 1)
                    {
                        if (bricks[row][col] == bricks[row][col + 1])
                        {
                            firstLayer.Add(new HorizontalBrick(bricks[row][col], row, col));

                            col++; // skip the second block of the current brick
                        }
                    }

                    // check for vertical brick
                    if (row < rows - 1)
                    {
                        if (bricks[row][col] == bricks[row + 1][col])
                        {
                            firstLayer.Add(new VerticalBrick(bricks[row][col], row, col));
                        }
                    }
                }
            }

            return firstLayer;
        }

        // creates the bricks for the second layer
        static Wall CreateSecondLayer(Wall firstLayer, int rows, int cols)
        {
            int[,] firstLayerTemplate = firstLayer.GetTemplate();
            int[,] secondLayerTemplate = new int[rows, cols];
            Wall secondLayer = new Wall(rows, cols);

            int value = 1;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // position not already filled
                    if (secondLayerTemplate[row, col] == 0)
                    {
                        // check if brick can be placed vertically
                        if (col + 1 >= cols || firstLayerTemplate[row, col] == firstLayerTemplate[row, col + 1])
                        {
                            secondLayerTemplate[row, col] = secondLayerTemplate[row + 1, col] = value;
                            secondLayer.Add(new VerticalBrick(value, row, col));
                        }
                        else // brick can be placed horizontally
                        {
                            secondLayerTemplate[row, col] = secondLayerTemplate[row, col + 1] = value;
                            secondLayer.Add(new HorizontalBrick(value, row, col));
                        }

                        value++;
                    }
                }
            }

            return secondLayer;
        }

        static bool ValidateLayer(Wall layer)
        {
            int[,] template = layer.GetTemplate();

            for (int row = 0; row < layer.Rows; row++)
            {
                for (int col = 0; col < layer.Cols; col++)
                {
                    if (template[row, col] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

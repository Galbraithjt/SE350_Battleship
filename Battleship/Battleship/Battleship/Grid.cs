using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Grid
    {
        public Ship[] playerShips;
        public int[,] gridLocation;
        public int shipsSunk;
        public int gridWidthStart;
        public int gridHeightStart;
        public int hits;
        public int misses;
        public Grid()
        {
            playerShips = new Ship[5];
            shipsSunk = 0;
            gridWidthStart = 329;
            gridHeightStart = 88;
            gridLocation = new int [10,10];
            hits = 0;
            misses = 0;
            ClearGrid();
    }
            /* Equation to determine individual squares
           // Math.Floor((495 - 329) / 20);
            //Take the start point of the grid(left/top (the x/y) ) and 
            //subtract the x/y point from the coorisponding side.
            //Then divide that from the dimensions of the grid(size of the square)
            //Finally use Math.floor to remove the decimal 
           // Gridstart - point / dimension = value
           // math.floor(value)
            int width = 0;
            int height = 0;
            //Because the grid is 20 by 20 for each square, 
            //the square selected will be switched with the
            //1 in this example 
            //of the grid to select the correct square
            //Width = A-J
            //Height = 1 - 10
            width = (20 * 1);
            height = (20 * 1);
            */
        public void PlaceShips(Grid grid, Ship ship)
        {
            bool notPlace = false;
            do
            {
                Random ranCol = new Random();
                Random ranRow = new Random();
               
                if (ship.GetShipDirection() == true)
                {
                    int compare = 0;
                    int row = ranRow.Next(1, 10 - (ship.GetShipLength() - 1));
                    int col = ranCol.Next(1, 10);
                    while (compare < ship.GetShipLength())
                    {
                        if (grid.gridLocation[(row + compare), col] == 0)
                        {
                            compare++;
                            if (compare == ship.GetShipLength())
                            {
                                notPlace = true;
                                for (int x = 0; x < ship.GetShipLength(); x++)
                                {
                                    grid.gridLocation[(row + x), col] = 1;
                                    ship.startPoint.X = row;
                                    ship.startPoint.Y = col;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            } while (notPlace == false);
        }

        private void ClearGrid()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    gridLocation[x,y] = 0;
                }
            }
        }

        public void IsItAHit(System.Windows.Point firedLocation)
        {
            for (int x = 0; x < playerShips.Length; x++)
            {
                for (int y = 0; y < playerShips[x].GetShipLength(); y++)
                {
                    if (playerShips[x].GetShipDirection() == true)
                    {
                        if (playerShips[x].startPoint.X + y == firedLocation.X)
                        {
                            playerShips[x].SetShipHealth(-1);
                            if (playerShips[x].GetShipHealth() == 0)
                            {
                                System.Windows.MessageBox.Show("Enemy " + playerShips[x].GetShipName() + " Sunk Commander!");
                            }
                        }
                    }
                    else
                    {
                        if (playerShips[x].startPoint.Y + y == firedLocation.Y)
                        {
                            playerShips[x].SetShipHealth(-1);
                            if (playerShips[x].GetShipHealth() == 0)
                            {
                                System.Windows.MessageBox.Show("Enemy " + playerShips[x].GetShipName() + " Sunk Commander!");
                            }
                        }
                    }
                }
            }
        }
        public void ReportShipHealth(Ship ship)
        {

        }
    }
}
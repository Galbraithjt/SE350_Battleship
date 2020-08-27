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
        public double gridWidthStart;
        public double gridHeightStart;
        public int hits;
        public int misses;
        //grid width and height determined by UI canvas Left Margin and Upper Margin
        public Grid()
        {
            playerShips = new Ship[5];
            shipsSunk = 0;
            gridWidthStart = 665;
            gridHeightStart = 156;
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
            //Ship direction returing as true = ship is oriented as horizontal||false = vertical
            if(ship.GetShipDirection() == true)
            {
                HorizontalPlacement(grid, ship);
            }
            else
            {
                VerticalPlacement(grid, ship);
            }
        }
        private void HorizontalPlacement(Grid grid, Ship ship)
        {
            bool shipPlaced = false;
            int selectedRow;
            int selectedColumn;
            Random randomPlacement = new Random();
            int shipLength = ship.GetShipLength();
            bool occupiedSpot;
            /// Until the ship finds a place that is not occupied, following code will:
            /// Generate a random row that accounts for the ship's length (to prevent clipping and array index out of bounds)
            /// Check subsequent spots from the origin point for other entities occupying the space ( 1 = occupied space in grid location array)
            /// Place ship and break the loop
            while (shipPlaced == false)
            {
                occupiedSpot = false;
                selectedRow = randomPlacement.Next(1, 10 - (shipLength - 1));
                selectedColumn = randomPlacement.Next(1, 10);
                if (grid.gridLocation[selectedRow, selectedColumn] == 0)
                {
                    for (int x = 0; x < shipLength; x++)
                    {
                        if (grid.gridLocation[selectedRow + x, selectedColumn] == 1)
                        {
                            occupiedSpot = true;
                            break;
                        }
                    }
                    if (occupiedSpot == true)
                    {
                        continue;
                    }
                    else
                    {
                        for (int x = 0; x < shipLength; x++)
                        {
                            grid.gridLocation[selectedRow + x, selectedColumn] = 1;
                            ship.startPoint.X = selectedRow;
                            ship.startPoint.Y = selectedColumn;
                        }
                        shipPlaced = true;
                    }
                }
            }
        }
        private void VerticalPlacement(Grid grid, Ship ship)
        {

            bool shipPlaced = false;
            int selectedRow;
            int selectedColumn;
            Random randomPlacement = new Random();
            int shipLength = ship.GetShipLength();
            //Ship direction = true means it is horizontal
            bool occupiedSpot;
            /// Until the ship finds a place that is not occupied, following code will:
            /// Generate a random row that accounts for the ship's length (to prevent clipping and array index out of bounds)
            /// Check subsequent spots from the origin point for other entities occupying the space ( 1 = occupied space in grid location array)
            /// Place ship and break the loop
            while (shipPlaced == false)
            {
                occupiedSpot = false;
                selectedRow = randomPlacement.Next(1, 10);
                selectedColumn = randomPlacement.Next(1, 10 - (shipLength - 1));
                if (grid.gridLocation[selectedRow, selectedColumn] == 0)
                {
                    for (int x = 0; x < shipLength; x++)
                    {
                        if (grid.gridLocation[selectedRow, selectedColumn + x] == 1)
                        {
                            occupiedSpot = true;
                            break;
                        }
                    }
                    if (occupiedSpot == true)
                    {
                        continue;
                    }
                    else
                    {

                        for (int x = 0; x < shipLength; x++)
                        {
                            grid.gridLocation[selectedRow, selectedColumn + x] = 1;
                            ship.startPoint.X = selectedRow;
                            ship.startPoint.Y = selectedColumn;
                        }
                        shipPlaced = true;
                    }

                }
            }
        }
        public void ClearGrid()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    gridLocation[x,y] = 0;
                }
            }
        }
        public void ReportShipDamage(System.Windows.Point firedLocation)
        {
            ///damageFound is being used for breaking out of loops
            ///when the target ship's damage is found
            bool damageFound = false;
            for (int x = 0; x < playerShips.Length; x++)
            {
                //break the loop if damage is found
                if (damageFound == true)
                {
                    break;
                }
                //To prevent checking the ship's length from the method every itoration
                int currentShipLength = playerShips[x].GetShipLength();
                for (int y = 0; y < currentShipLength; y++)
                {
                    //Ship direction returing true = ship horizontal on grid
                    if (playerShips[x].GetShipDirection() == true)
                    {
                        if (playerShips[x].startPoint.X + y == firedLocation.X)
                        {
                            playerShips[x].SetShipHealth(-1);
                            damageFound = true;
                            if (playerShips[x].GetShipHealth() == 0)
                            {
                                System.Windows.MessageBox.Show("Enemy " + playerShips[x].GetShipName() + " Sunk Commander!");
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (playerShips[x].startPoint.Y + y == firedLocation.Y)
                        {
                            playerShips[x].SetShipHealth(-1);
                            damageFound = true;
                            if (playerShips[x].GetShipHealth() == 0)
                            {
                                System.Windows.MessageBox.Show("Enemy " + playerShips[x].GetShipName() + " Sunk Commander!");
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
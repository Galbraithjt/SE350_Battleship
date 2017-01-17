using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Grid
    {
        public Piece[] playerShips;
        public int shipsSunk;
        public int numHits;
        public int numMiss;
        public int gridWidthStart;
        public int gridHeightStart;
        public int gridWidthEnd;
        public int gridHeightEnd;
        public int removeLocation { get; set; }
        public double x_fire_coordinate { get; set; }
        public double y_fire_coordinate { get; set; }
        public Grid()
        {
            playerShips = new Piece[5];
           

            shipsSunk = 0;
            numHits = 0;
            numMiss = 0;
            gridWidthStart = 329;
            gridHeightStart = 88;
            gridWidthEnd = 529;
            gridHeightEnd = 288;
            x_fire_coordinate = 0;
            y_fire_coordinate = 0;
            removeLocation = 0;
    }
        public void algorithm()
        {
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

        }
        public void isHit(Location coordinate)
        {

        }
        public void reportShipHealth(Piece inputShip)
        {

        }
        public void updateDisplay()
        {

        }
      
       public void storeFireLocation(double x_coordinate, double y_coordinate)
        {
            x_fire_coordinate = x_coordinate;
            y_fire_coordinate = y_coordinate;
        }

    }
}
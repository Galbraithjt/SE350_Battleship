using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Location
    {
        public double startLocationX;
        public double startLocationY;
        public int[] length;
        public bool hitMiss;
        public bool ship;

        public Location()
        {
            startLocationX = 0;
            startLocationY = 0;
            length = new int[0];
            hitMiss = false;
            ship = false;
        }
        public Location(double startCoordinateX = 0, double startCoordinateY = 0, int shipLength = 0)
        {
            startLocationX = startCoordinateX;
            startLocationY = startCoordinateY;
            length = new int[shipLength];
            hitMiss = false;
            ship = false;
            placeShip();
        }

        public void placeShip()
        {
            for (int x = 0; x < length.Length; x++)
            {
                length[x] = Convert.ToInt32(startLocationX) + x;
            }



        }
        public bool hit()
        {
            if (hitMiss == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

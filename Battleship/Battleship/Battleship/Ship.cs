using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Ship
    {
        public int health;
        public int length;
        public String name;
        public bool direction; // true = horizontal; false = vertical
        public enum ShipLocation : int
        {
            ROW,
            COL
        };
        public System.Windows.Point startPoint;

        public Ship()
        {
            length = 0;
            health = length;
            name = " ";
            startPoint = new System.Windows.Point();
            direction = true;
        }
        public Ship(double startLocationX = 0, double startLocationY = 0, int length = 0, String name = " ")
        {
            health = length;
            this.length = length;
            this.name = name;
            direction = true;
        }
        
     
    }
}


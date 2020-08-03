using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Ship
    {
        private int health;
        private int length;
        private String name;
        private bool direction; // true = horizontal; false = vertical
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
            startPoint = new System.Windows.Point();
        }
        public int GetShipHealth()
        {
            return health;
        }
        public void SetShipHealth(int healthAdjustment)
        {
            health = health + healthAdjustment;
        }
        public int GetShipLength()
        {
            return length;
        }
        public bool GetShipDirection()
        {
            return direction;
        }
        public String GetShipName()
        {
            return name;
        } 
    }
}


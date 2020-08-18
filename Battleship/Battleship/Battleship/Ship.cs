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
        private Random random;
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
            SetShipDirection();
        }
        public Ship(double startLocationX = 0, double startLocationY = 0, int length = 0, String name = " ")
        {
            health = length;
            this.length = length;
            this.name = name;
            random = new Random();
            SetShipDirection();
            startPoint = new System.Windows.Point();
            startPoint.X = startLocationX;
            startPoint.Y = startLocationY;
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
        private void SetShipDirection()
        {
            
            int ranNum = random.Next(1, 101);
            Console.WriteLine("Random Number = " + ranNum);
            if (ranNum % 2 == 0)
            {
                direction = true;
            }
            else
            {
                direction = false;
            }
        }
        public String GetShipName()
        {
            return name;
        } 
    }
}


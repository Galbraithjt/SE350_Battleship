using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Piece
    {
        public int health;
        public double length;
        public String name;
        public Location location;
        public bool direction; // true = horizontal; false = vertical
        public bool status;


        public Piece()
        {
            health = 0;
            length = 0;
            name = " ";
            location = new Location();
            direction = true;
            status = true;
        }
        public Piece(double startLocationX = 0, double startLocationY = 0, int length = 0, String name = " ")
        {
            health = length;
            this.length = length;
            this.name = name;
            location = new Location(startLocationX,startLocationY, length);
            direction = true;
            status = true;
        }
        public void place()
        {

        }
        public void takeHit()
        {
            health = health - 1;
        }
    }
}


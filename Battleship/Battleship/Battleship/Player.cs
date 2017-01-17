using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Player
    {
        
        public Piece submarine;
        public Piece patrol;
        public Piece carrier;
        public Piece cruiser;
        public Piece battleship;
        public Piece[] ships;
        public int totalShips;

        public Player()
        {
            submarine = new Piece();
            patrol = new Piece();
            carrier = new Piece();
            cruiser = new Piece();
            battleship = new Piece();
            ships = new Piece[5];
            totalShips = ships.Length;
        }

        public Player(System.Windows.Point submarine, System.Windows.Point patrol,
                              System.Windows.Point carrier, System.Windows.Point cruiser,
                              System.Windows.Point battleship)
        {
            setShips(submarine, patrol, carrier, cruiser, battleship);
        }
        public void setShips(System.Windows.Point submarine, System.Windows.Point patrol,
                              System.Windows.Point carrier, System.Windows.Point cruiser,
                              System.Windows.Point battleship)
        {
            this.submarine = new Piece(submarine.X, submarine.Y, 3, "Submarine");
            this.patrol = new Piece(patrol.X, patrol.Y, 2, "Patrol Boat");
            this.carrier = new Piece(carrier.X, carrier.Y, 5, "Carrier");
            this.cruiser = new Piece(cruiser.X, cruiser.Y, 3, "Cruiser");
            this.battleship = new Piece(battleship.X, battleship.Y, 4, "Battleship");

            ships = new Piece[5];
            ships[0] = this.patrol;
            ships[1] = this.submarine;
            ships[2] = this.carrier;
            ships[3] = this.cruiser;
            ships[4] = this.battleship;
            totalShips = ships.Length;
        }

    }
}

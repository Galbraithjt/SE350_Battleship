using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Player
    {

        public Grid playerGrid;
        public bool CPU;
        public bool active;
        public Player()
        {
            playerGrid = new Grid();
            CPU = false;
            active = false;
        }

        public Player(System.Windows.Point submarine, System.Windows.Point patrol,
                              System.Windows.Point carrier, System.Windows.Point cruiser,
                              System.Windows.Point battleship)
        {
            SetShips(submarine, patrol, carrier, cruiser, battleship);
        }
        public void SetShips(System.Windows.Point submarine, System.Windows.Point patrol,
                              System.Windows.Point carrier, System.Windows.Point cruiser,
                              System.Windows.Point battleship)
        {
          
          
        }

    }
}

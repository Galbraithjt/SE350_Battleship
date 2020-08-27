using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Player
    {

        /* Refactor notes for player:
         * Create functionality within player to set ships if process is not acomplished in grid class. If Accomplished in grid class, remove set shits from player
         * Seperation between human player and AI player into individual classes
         * Have the player class keep track of their own ships possibly and remaining amount of ships
         * 
         * 
         */
        public Grid playerGrid;
        private readonly bool CPU;
        public bool active;
        public Player()
        {
            playerGrid = new Grid();
            CPU = false;
            active = false;
        }
        public Player(bool cpuActive = false, bool activePlayer = false)
        {
            playerGrid = new Grid();
            CPU = cpuActive;
            active = activePlayer;
        }
    }
}

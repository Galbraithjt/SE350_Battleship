using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Board
    {
       public Player playerOne;
       public Player playerTwo;
       public System.Windows.Point fireLocation;

        public Board()
        {
            playerOne = new Player();
            playerTwo = new Player();
                
        }
        public void startGame()
        {
            playerTwo.CPU = true;
        }
        public void selectSquare(System.Windows.Point selectedSquare, Grid grid)
        {
            fireLocation.X = Math.Floor(Math.Abs(selectedSquare.X - grid.gridWidthStart) / 20);
            fireLocation.Y = Math.Floor(Math.Abs(selectedSquare.Y - grid.gridHeightStart) / 20);
        }
        public void placeShips(Grid grid)
        {
            grid.playerShips[0] = new Ship(0, 0, 2,"Patrol");
            grid.playerShips[1] = new Ship(0, 0, 3, "Submarine");
            grid.playerShips[2] = new Ship(0, 0, 3, "Cruiser");
            grid.playerShips[3] = new Ship(0, 0, 4, "Battleship");
            grid.playerShips[4] = new Ship(0, 0, 5, "Carrier");
            for(int x = 0; x < grid.playerShips.Length; x++)
            {
                grid.placeShips(grid, grid.playerShips[x]);
            }


        }
        public int fireShot(Grid targetGrid)
        {
            //Check to see if a hit or miss occurs on firing location. Return true if the selection has not
            //been previously fired upon; false if it has.
            if (targetGrid.GridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] == 0)
            {
                targetGrid.GridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] = 3;
                return 0;
            }
            else
            if (targetGrid.GridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] == 1)
            {
                targetGrid.GridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] = 4;
                targetGrid.isHit(fireLocation);
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}

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
       private enum gridSquareStatus : int
        {
            EMPTYGRIDSQUARE, //0
            OCCUPGRIDSQUARE, //1
            FIREDANDHIT, //2
            FIREDANDMISS //3
        };
        /*
         * Notes for board refactor:
         * 
         * 
         * Explain Math Equation in all instances used
         *
         */

        public Board()
        {
            playerOne = new Player(false, true);
            playerTwo = new Player(true, false);
        }
        
        public void SelectSquare(System.Windows.Point selectedSquare, Grid grid)
        {
            fireLocation.X = Math.Floor(Math.Abs(selectedSquare.X - grid.gridWidthStart) / 40);
            fireLocation.Y = Math.Floor(Math.Abs(selectedSquare.Y - grid.gridHeightStart) / 40);

            if (fireLocation.X > 9)
            {
                fireLocation.X = 9;
            }
            if(fireLocation.Y > 9)
            {
                fireLocation.Y = 9;
            }
        }

        public void PlaceShips(Grid grid)
        {
            grid.playerShips[0] = new Ship(0, 0, 2,"Patrol");
            grid.playerShips[1] = new Ship(0, 0, 3, "Submarine");
            grid.playerShips[2] = new Ship(0, 0, 3, "Cruiser");
            grid.playerShips[3] = new Ship(0, 0, 4, "Battleship");
            grid.playerShips[4] = new Ship(0, 0, 5, "Carrier");
            for(int x = 0; x < grid.playerShips.Length; x++)
            {
                grid.PlaceShips(grid, grid.playerShips[x]);
            }
        }

        public int FireShot(Grid targetGrid)
        {
            //Check to see if a hit or miss occurs on firing location and return values related to aforementioned hit or miss
            // 0 = Square is not occupied and should result in a miss (3)
            // 1 = Square has an entity of a ship and should result in a hit (2)
            // Default to miss for any potential errors in math
            switch (targetGrid.gridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)])
            {
                case (0):
                    targetGrid.gridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] = 3;
                    return Convert.ToInt32(gridSquareStatus.FIREDANDMISS);
                case (1):
                    targetGrid.gridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] = 2;
                    return Convert.ToInt32(gridSquareStatus.FIREDANDHIT);
                default:
                    return Convert.ToInt32(gridSquareStatus.FIREDANDMISS);
            }

            /* OLD IF STATEMENT CODE
             * 
            if (targetGrid.gridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] == Convert.ToInt32(gridSquareStatus.EMPTYGRIDSQUARE))
            {
                targetGrid.gridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] = Convert.ToInt32(gridSquareStatus.FIREDANDMISS);
                return Convert.ToInt32(gridSquareStatus.FIREDANDMISS);
            }
            else
            if (targetGrid.gridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] == Convert.ToInt32(gridSquareStatus.OCCUPGRIDSQUARE))
            {
                targetGrid.gridLocation[Convert.ToInt32(fireLocation.X), Convert.ToInt32(fireLocation.Y)] = Convert.ToInt32(gridSquareStatus.FIREDANDHIT);
                targetGrid.ReportShipDamage(fireLocation);
                return Convert.ToInt32(gridSquareStatus.FIREDANDHIT);
            }
            else
            {
                return Convert.ToInt32(gridSquareStatus.FIREDANDMISS);
            }
            */
        }
        public bool PerformTurn()
        {
            //If firing status = 2, a hit was achieved
            
            ///CHANGE THE TWO FOR LOOPS INTO AN ALGORITHM THAT CAN 
            ///SEARCH THE ARRAYS MORE EFFICENTLY 
            bool didItHit = false;
            if (playerOne.active == true)
            {
                int firingStatus = FireShot(playerTwo.playerGrid);

                if (firingStatus == 2)
                {
                    didItHit = true;
                    playerTwo.playerGrid.ReportShipDamage(fireLocation);
                }
                else
                {
                    didItHit = false;
                }
            }
            else
            {
                int firingStatus = FireShot(playerOne.playerGrid);
                if (firingStatus == 2)
                {
                    didItHit = true;
                }
                else
                {
                    didItHit = false;
                }
            }
            return didItHit;
        }
    }

}


/*
 * 
 * for (int x = 0; x < playerTwo.playerGrid.playerShips.Length; x++)
                    {
                        for (int y = 0; y < playerTwo.playerGrid.playerShips[x].GetShipLength(); y++)
                        {
                            if(playerTwo.playerGrid.playerShips[x].GetShipDirection() == true)
                            {
                                    if (playerTwo.playerGrid.playerShips[x].startPoint.X + y == fireLocation.X
                                        && playerTwo.playerGrid.playerShips[x].startPoint.Y == fireLocation.Y
                                        )
                                    {
                                        playerTwo.playerGrid.playerShips[x].SetShipHealth(-1);
                                        break;
                                    }
                            }
                            else
                            {
                                    if (playerTwo.playerGrid.playerShips[x].startPoint.X == fireLocation.X
                                       && playerTwo.playerGrid.playerShips[x].startPoint.Y + y == fireLocation.Y
                                       )
                                    {
                                        playerTwo.playerGrid.playerShips[x].SetShipHealth(-1);
                                        break;
                                    }
                            }
                        }
                        
                    }
 * 
 */

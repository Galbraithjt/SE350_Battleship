using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battleship
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /*
         * Refactor notes for Main Window
         * 
         * Consider renaiming the class itself?
         * COMMENTS PAST ME, ADD MORE COMMENTS
         * Rename player ships and enemy ships
         * Seperate enemy and player ships more clearly in the code
         * WHY WAS I SUCH AN IDIOT, ADD MORE DAMN COMMENTS  
         * 
         * */
        Board gameBoard;
        int gridSquareSize;
        public MainWindow()
        {
            InitializeComponent();
            gameBoard = new Board();
            gridSquareSize = 40;
        }
        
        public void Fire_Missile_Click(object sender, RoutedEventArgs e)
        {
            var sele = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "selection");
            Enemy_Canvas.Children.Remove(sele);
            ///Clean this if statement up and convert to a switch statement
            //Create a rectangle that is dependant on if a hit or miss is achieved
            if (gameBoard.PerformTurn() == false)
            {
                if (gameBoard.playerOne.active == true)
                {
                    PlaceHitOrMiss(Enemy_Canvas, false);
                    Miss_Ratio.Content = "Miss: " + gameBoard.playerOne.playerGrid.misses;
                }
                else
                {
                    PlaceHitOrMiss(Player_Canvas, false);
                    Miss_Ratio.Content = "Miss: " + gameBoard.playerTwo.playerGrid.misses;
                }
            }
            else
            {
                Rectangle hit = CreateRectangle(true);
                Enemy_Canvas.Children.Add(hit);
                PlaceRectangle(hit);
                Fire_Missile.IsEnabled = false;
                Hit_Ratio.Content = "Hit: " + gameBoard.playerOne.playerGrid.hits;
            }
           
        }

        public void PlaceShips_Click(object sender, RoutedEventArgs e)
        {
            gameBoard.PlaceShips(gameBoard.playerOne.playerGrid);
            Confirm_Ships_Button.IsEnabled = true;
            Player_Canvas.Children.Clear();
            //Two for loops to clear out previous ship locations before creating new ships
            //Assumes that ships have been created once before by the user
            gameBoard.playerOne.playerGrid.ClearGrid();
            for (int x = 0; x < gameBoard.playerOne.playerGrid.playerShips.Length; x++)
            {
                DrawShipOnGrid(CreateShip(gameBoard.playerOne.playerGrid.playerShips[x].GetShipName(),
                                     gameBoard.playerOne.playerGrid.playerShips[x].GetShipLength(),
                                     gameBoard.playerOne.playerGrid.playerShips[x].GetShipDirection()), 
                                     Player_Canvas, gameBoard.playerOne.playerGrid.playerShips[x]);
            }
            //Player battleship visibility
            Player_Battleship.Visibility = Visibility.Hidden;
            Player_Carrier.Visibility = Visibility.Hidden;
            Player_Destroyer.Visibility = Visibility.Hidden;
            Player_PatrolBoat.Visibility = Visibility.Hidden;
            Player_Sub.Visibility = Visibility.Hidden;
            //Enemy Battleship visibility
            Enemy_Battleship.Visibility = Visibility.Visible;
            Enemy_Carrier.Visibility = Visibility.Visible;
            Enemy_Destroyer.Visibility = Visibility.Visible;
            Enemy_PatrolBoat.Visibility = Visibility.Visible;
            Enemy_Sub.Visibility = Visibility.Visible;
        }
        public void Confirm_Ships_Button_Click(object sender, RoutedEventArgs e)
        {
            ///Enemy canvas code copied from place ships code
            ///currently not removing old ships from enemy shipyard after placement
            
            //Two for loops to clear out previous ship locations before creating new ships
            //Assumes that ships have been created once before by the user
            gameBoard.playerTwo.playerGrid.ClearGrid();
            gameBoard.PlaceShips(gameBoard.playerTwo.playerGrid);
            Enemy_Canvas.Children.Clear();

            /*
            for (int x = 0; x < gameBoard.playerTwo.playerGrid.playerShips.Length; x++)
            {
                DrawShipOnGrid(CreateShip(gameBoard.playerTwo.playerGrid.playerShips[x].GetShipName(),
                                        gameBoard.playerTwo.playerGrid.playerShips[x].GetShipLength(),
                                        gameBoard.playerTwo.playerGrid.playerShips[x].GetShipDirection()),
                                        Enemy_Canvas, gameBoard.playerTwo.playerGrid.playerShips[x]);
            }
            */
            Enemy_Battleship.Visibility = Visibility.Hidden;
            Enemy_Carrier.Visibility = Visibility.Hidden;
            Enemy_Destroyer.Visibility = Visibility.Hidden;
            Enemy_Sub.Visibility = Visibility.Hidden;
            Enemy_PatrolBoat.Visibility = Visibility.Hidden;

            PlaceShips.Visibility = Visibility.Hidden;
            Confirm_Ships_Button.Visibility = Visibility.Hidden;
            Miss_Ratio.Visibility = Visibility.Visible;
            Miss_Ratio_Enemy.Visibility = Visibility.Visible;
            Hit_Ratio.Visibility = Visibility.Visible;
            Hit_Ratio_Enemy.Visibility = Visibility.Visible;
            Fire_Missile.Visibility = Visibility.Visible;
            Retreat.Visibility = Visibility.Visible;
            Enemy_Grid.IsEnabled = true;
        }
        public void Start_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Menu_Label.Visibility = Visibility.Hidden;
            Start_Game_Button.Visibility = Visibility.Hidden;

            //Player Visibility Text Boxes, Grid and Boats
            Player_A_TextBox.Visibility = Visibility.Visible;
            Player_B_TextBox.Visibility = Visibility.Visible;
            Player_C_TextBox.Visibility = Visibility.Visible;
            Player_D_TextBox.Visibility = Visibility.Visible;
            Player_E_TextBox.Visibility = Visibility.Visible;
            Player_F_TextBox.Visibility = Visibility.Visible;
            Player_G_TextBox.Visibility = Visibility.Visible;
            Player_H_TextBox.Visibility = Visibility.Visible;
            Player_I_TextBox.Visibility = Visibility.Visible;
            Player_J_TextBox.Visibility = Visibility.Visible;
            Player_Shipyard.Visibility = Visibility.Visible;
            Player_Battleship.Visibility = Visibility.Visible;
            Player_Carrier.Visibility = Visibility.Visible;
            Player_Destroyer.Visibility = Visibility.Visible;
            Player_PatrolBoat.Visibility = Visibility.Visible;
            Player_Sub.Visibility = Visibility.Visible;
            Player_Canvas.Visibility = Visibility.Visible;
            Player_One_Label.Visibility = Visibility.Visible;
            Player_Two_Label.Visibility = Visibility.Visible;
            Player_Shipyard_Text_Block.Visibility = Visibility.Visible;
            Player_Grid.Visibility = Visibility.Visible;
            //Enemy Grid,Text Boxes and Boats
            Enemy_Canvas.Visibility = Visibility.Visible;
            Enemy_Grid.Visibility = Visibility.Visible;
            Enemy_Shipyard.Visibility = Visibility.Visible;
            Enemy_Battleship.Visibility = Visibility.Visible;
            Enemy_Carrier.Visibility = Visibility.Visible;
            Enemy_Destroyer.Visibility = Visibility.Visible;
            Enemy_PatrolBoat.Visibility = Visibility.Visible;
            Enemy_Sub.Visibility = Visibility.Visible;
            Enemy_Grid.IsEnabled = false;
            Enemy_A_TextBox.Visibility = Visibility.Visible;
            Enemy_B_TextBox.Visibility = Visibility.Visible;
            Enemy_C_TextBox.Visibility = Visibility.Visible;
            Enemy_D_TextBox.Visibility = Visibility.Visible;
            Enemy_E_TextBox.Visibility = Visibility.Visible;
            Enemy_F_TextBox.Visibility = Visibility.Visible;
            Enemy_G_TextBox.Visibility = Visibility.Visible;
            Enemy_H_TextBox.Visibility = Visibility.Visible;
            Enemy_I_TextBox.Visibility = Visibility.Visible;
            Enemy_J_TextBox.Visibility = Visibility.Visible;
            Enemy_Shipyard_Text_Block.Visibility = Visibility.Visible;
            //General Game Asset Visibility
            PlaceShips.Visibility = Visibility.Visible;
            One_TextBox.Visibility = Visibility.Visible;
            Two_TextBox.Visibility = Visibility.Visible;
            Three_TextBox.Visibility = Visibility.Visible;
            Four_TextBox.Visibility = Visibility.Visible;
            Five_TextBox.Visibility = Visibility.Visible;
            Six_TextBox.Visibility = Visibility.Visible;
            Seven_TextBox.Visibility = Visibility.Visible;
            Eight_TextBox.Visibility = Visibility.Visible;
            Nine_TextBox.Visibility = Visibility.Visible;
            Ten_TextBox.Visibility = Visibility.Visible;
            Confirm_Ships_Button.Visibility = Visibility.Visible;
            Ship_Descriptions_Label.Visibility = Visibility.Visible;
            Confirm_Ships_Button.IsEnabled = false;
        }

        private void Enemy_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Point is a object that holds a x and y value.
            //Point is then set to the current position of the click origin in 
            //the current window.
            Point point = Mouse.GetPosition(Application.Current.MainWindow);
            //Apply the algorith covered in grid to the stored points
            gameBoard.SelectSquare(point, gameBoard.playerTwo.playerGrid);
            Fire_Missile.IsEnabled = true;

            Rectangle selection = new Rectangle();
            selection.Name = "selection";
            var sele = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "selection");
            Enemy_Canvas.Children.Remove(sele);
            selection.Width = 40;
            selection.Height = 40;
            selection.Fill = new SolidColorBrush(Colors.Violet);
            selection.Opacity = .7;
            Enemy_Canvas.Children.Add(selection);
            Canvas.SetTop(selection, (gameBoard.fireLocation.Y * 40));
            Canvas.SetLeft(selection, (gameBoard.fireLocation.X * 40));
        }

        private void Retreat_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void End_State()
        {
            Fire_Missile.Visibility = Visibility.Hidden;
            Retreat.Visibility = Visibility.Hidden;
            Restart_Game_Button.Visibility = Visibility.Visible;
            Exit_Game_Button.Visibility = Visibility.Visible;


            if(gameBoard.playerTwo.playerGrid.shipsSunk == gameBoard.playerTwo.playerGrid.playerShips.Length)
            {
                MessageBox.Show("You've defeated the enemy!");
            }
            else
            {
                MessageBox.Show("You've been defeated :( ");
            }
        }
        private void Exit_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private Rectangle CreateRectangle(bool hitMiss)
        {
            Rectangle rect = new Rectangle
            {
                Width = 40,
                Height = 40
            };

            if (hitMiss == false)
            {
                rect.Opacity = .7;
                rect.Fill = new SolidColorBrush(Colors.Aquamarine);
            }
            else
            {
                rect.Opacity = .9;
                rect.Fill = new SolidColorBrush(Colors.Red);
            }
            return rect;
        }

        private void PlaceRectangle(Rectangle rect)
        {
            Canvas.SetTop(rect, (gameBoard.fireLocation.Y * gridSquareSize));
            Canvas.SetLeft(rect, (gameBoard.fireLocation.X * gridSquareSize));
        }
        private Ellipse CreateShip(String shipName, int length, bool direction)
        {
            Ellipse ship = new Ellipse();

            if (direction == true)
            {
                ship.Width = gridSquareSize * length;
                ship.Height = gridSquareSize;
                ship.Fill = new SolidColorBrush(Colors.Teal);
                ship.Name = shipName;
                ship.IsEnabled = false;
            }
            else
            {
                ship.Width = gridSquareSize;
                ship.Height = gridSquareSize * length;
                ship.Fill = new SolidColorBrush(Colors.Teal);
                ship.Name = shipName;
                ship.IsEnabled = false;
            }
            return ship;
        }
        private void DrawShipOnGrid(Ellipse drawShip, Canvas canvas, Ship ship)
        {
            canvas.Children.Add(drawShip);
            Canvas.SetLeft(drawShip, (ship.startPoint.X * gridSquareSize));
            Canvas.SetTop(drawShip, (ship.startPoint.Y * gridSquareSize));
        }
        private void PlaceHitOrMiss(Canvas canvas, bool hitMiss)
        {
            Rectangle rect = CreateRectangle(hitMiss);
            canvas.Children.Add(rect);
            PlaceRectangle(rect);
            Fire_Missile.IsEnabled = false;
        }
    }
}

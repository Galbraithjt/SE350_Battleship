﻿using System;
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
        Board gameBoard;
        public MainWindow()
        {
            InitializeComponent();
            gameBoard = new Board();
        }
        
        private void Fire_Missile_Click(object sender, RoutedEventArgs e)
        {
            //Create a rectangle that is dependant on if a hit or miss is achieved
            if (Perform_Turn() == true)
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
                    var sele = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "selection");
                    Enemy_Canvas.Children.Remove(sele);
                }
            }
            else
            {
                Rectangle hit = CreateRectangle(true);
                Enemy_Canvas.Children.Add(hit);
                PlaceRectangle(hit);
                Fire_Missile.IsEnabled = false;
                Hit_Ratio.Content = "Hit: ";
            }
           
        }

        private void PlaceShips_Click(object sender, RoutedEventArgs e)
        {
            gameBoard.placeShips(gameBoard.playerOne.playerGrid);
            Confirm_Ships_Button.IsEnabled = true;
            Player_Canvas.Children.Clear();
            for (int x = 0; x < gameBoard.playerOne.playerGrid.playerShips.Length; x++)
            {
                PlaceShip(CreateShip(gameBoard.playerOne.playerGrid.playerShips[x].name,
                                     gameBoard.playerOne.playerGrid.playerShips[x].length,
                                     gameBoard.playerOne.playerGrid.playerShips[x].direction), 
                                     Player_Canvas, gameBoard.playerOne.playerGrid.playerShips[x]);
            }
            Player_Battleship.Visibility = Visibility.Hidden;
            Player_Carrier.Visibility = Visibility.Hidden;
            Player_Destroyer.Visibility = Visibility.Hidden;
            Player_PatrolBoat.Visibility = Visibility.Hidden;
            Player_Sub.Visibility = Visibility.Hidden;
            Enemy_Battleship.Visibility = Visibility.Hidden;
            Enemy_Carrier.Visibility = Visibility.Hidden;
            Enemy_Destroyer.Visibility = Visibility.Hidden;
            Enemy_PatrolBoat.Visibility = Visibility.Hidden;
            Enemy_Sub.Visibility = Visibility.Hidden;
        }

        private void Start_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            Main_Menu_Label.Visibility = Visibility.Hidden;
            Start_Game_Button.Visibility = Visibility.Hidden;
            A_TextBox.Visibility = Visibility.Visible;
            B_TextBox.Visibility = Visibility.Visible;
            C_TextBox.Visibility = Visibility.Visible;
            D_TextBox.Visibility = Visibility.Visible;
            E_TextBox.Visibility = Visibility.Visible;
            F_TextBox.Visibility = Visibility.Visible;
            G_TextBox.Visibility = Visibility.Visible;
            H_TextBox.Visibility = Visibility.Visible;
            I_TextBox.Visibility = Visibility.Visible;
            J_TextBox.Visibility = Visibility.Visible;
            Enemy_Canvas.Visibility = Visibility.Visible;
            Enemy_Grid.Visibility = Visibility.Visible;
            Enemy_Shipyard.Visibility = Visibility.Visible;
            Enemy_Battleship.Visibility = Visibility.Visible;
            Enemy_Carrier.Visibility = Visibility.Visible;
            Enemy_Destroyer.Visibility = Visibility.Visible;
            Enemy_PatrolBoat.Visibility = Visibility.Visible;
            Enemy_Sub.Visibility = Visibility.Visible;
            Enemy_Grid.IsEnabled = false;
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
            Player_Grid.Visibility = Visibility.Visible;
            A_TextBox_Copy.Visibility = Visibility.Visible;
            B_TextBox_Copy.Visibility = Visibility.Visible;
            C_TextBox_Copy.Visibility = Visibility.Visible;
            D_TextBox_Copy.Visibility = Visibility.Visible;
            E_TextBox_Copy.Visibility = Visibility.Visible;
            F_TextBox_Copy.Visibility = Visibility.Visible;
            G_TextBox_Copy.Visibility = Visibility.Visible;
            H_TextBox_Copy.Visibility = Visibility.Visible;
            I_TextBox_Copy.Visibility = Visibility.Visible;
            J_TextBox_Copy.Visibility = Visibility.Visible;
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
            Enemy_Shipyard_Text_Block.Visibility = Visibility.Visible;
            Confirm_Ships_Button.Visibility = Visibility.Visible;
            Ship_Descriptions_Label.Visibility = Visibility.Visible;
            Confirm_Ships_Button.IsEnabled = false;
        }

        private void Enemy_Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            //Point is a object that holds a x and y value.
            //Point is then set to the current position of the click origin in 
            //the current window.
            Point point = Mouse.GetPosition(Application.Current.MainWindow);
            //Apply the algorith covered in grid to the stored points
            gameBoard.selectSquare(point, gameBoard.playerTwo.playerGrid);
            Fire_Missile.IsEnabled = true;

            Rectangle selection = new Rectangle();
            selection.Name = "selection";
            var sele = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "selection");
            Enemy_Canvas.Children.Remove(sele);
            selection.Width = 20;
            selection.Height = 20;
            selection.Fill = new SolidColorBrush(Colors.Violet);
            selection.Opacity = .7;
            Enemy_Canvas.Children.Add(selection);
            Canvas.SetTop(selection, (gameBoard.fireLocation.Y * 20));
            Canvas.SetLeft(selection, (gameBoard.fireLocation.X * 20));
        }

        private void Confirm_Ships_Button_Click(object sender, RoutedEventArgs e)
        {
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

        private void Retreat_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private bool Perform_Turn()
        {
            bool hitOrMiss = false;
            if (gameBoard.playerOne.active == true)
            {
                int firingStatus = gameBoard.fireShot(gameBoard.playerTwo.playerGrid);
                if (firingStatus != 2)
                {
                    if (firingStatus == 0)
                    {
                        hitOrMiss = true;
                    }
                    else
                    {
                        hitOrMiss = false;
                    }
                }
            }
            else
            {
                int firingStatus = gameBoard.fireShot(gameBoard.playerOne.playerGrid);
                if (firingStatus != 2)
                {
                    if (firingStatus == 0)
                    {
                        hitOrMiss = true;
                    }
                    else
                    {
                        hitOrMiss = false;
                    }
                }
            }
            return hitOrMiss;
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
            Rectangle rect = new Rectangle();
            rect.Width = 20;
            rect.Height = 20;

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
            Canvas.SetTop(rect, (gameBoard.fireLocation.Y * 20));
            Canvas.SetLeft(rect, (gameBoard.fireLocation.X * 20));
        }
        private Ellipse CreateShip(String shipName, int length, bool direction)
        {
            Ellipse ship = new Ellipse();

            if (direction == true)
            {
                ship.Width = 20 * length;
                ship.Height = 20;
                ship.Fill = new SolidColorBrush(Colors.Teal);
                ship.Name = shipName;
                ship.IsEnabled = false;
            }
            else
            {
                ship.Width = 20;
                ship.Height = 20 * length;
                ship.Fill = new SolidColorBrush(Colors.Teal);
                ship.Name = shipName;
                ship.IsEnabled = false;
            }
            return ship;
        }
        private void PlaceShip(Ellipse drawShip, Canvas canvas, Ship ship)
        {
            canvas.Children.Add(drawShip);
            Canvas.SetTop(drawShip, (ship.startPoint.Y * 20));
            Canvas.SetLeft(drawShip, (ship.startPoint.X * 20));
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

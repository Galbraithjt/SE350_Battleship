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
        Grid testgrid = new Grid();
        Piece testPiece = new Piece();
        int hitTarget = 0;
        int missShot = 0;
        int enemyMissShot = 0;
        int enemyHitTarget = 0;
        ComputerPlayer CPU = new ComputerPlayer();
        Player humanPlayer = new Player();
        public MainWindow()
        {
            InitializeComponent();
 
        }
        
        private void ShipGridDefinitions()
        {
            
        }

        private void MainPlayerGrid_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Fire_Missile_Click(object sender, RoutedEventArgs e)
        {
            //Create a rectangle that is dependant on if a hit or miss is achieved
            if (Perform_Turn() == true)
            {
                Rectangle miss = new Rectangle();
                miss.Width = 20;
                miss.Height = 20;
                miss.Fill = new SolidColorBrush(Colors.Aquamarine);
                miss.Opacity = .7;
                Enemy_Canvas.Children.Add(miss);
                Canvas.SetTop(miss, (testgrid.y_fire_coordinate * 20));
                Canvas.SetLeft(miss, (testgrid.x_fire_coordinate * 20));
                Fire_Missile.IsEnabled = false;

                missShot++;
                Miss_Ratio.Content = "Miss: " + missShot;

            }
            else
            {
                Rectangle hit = new Rectangle();
                hit.Width = 20;
                hit.Height = 20;
                hit.Fill = new SolidColorBrush(Colors.Red);
                hit.Opacity = .9;
                Enemy_Canvas.Children.Add(hit);
                Canvas.SetTop(hit, (testgrid.y_fire_coordinate * 20));
                Canvas.SetLeft(hit, (testgrid.x_fire_coordinate * 20));
                Fire_Missile.IsEnabled = false;

                hitTarget++;
                Hit_Ratio.Content = "Hit: " + hitTarget;
            }
            var sele = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "selection");
            Enemy_Canvas.Children.Remove(sele);
            Enemy_Turn();
        }

        private void PlaceShips_Click(object sender, RoutedEventArgs e)
        {
            //Submarine placement
            Ellipse submarine = new Ellipse();
            Random rnd = new Random();
            bool match = false;
            Point submarine_location = new Point();
            submarine.Name = "SSBN";
            submarine.Width = 60;
            submarine.Height = 20;
            submarine.Fill = new SolidColorBrush(Colors.Teal);
            submarine.IsEnabled = false;
            var sub = (UIElement)LogicalTreeHelper.FindLogicalNode(Player_Canvas, "SSBN");
            Player_Canvas.Children.Remove(sub);
            Player_Canvas.Children.Add(submarine);
            Canvas.SetTop(submarine, (rnd.Next(0, 10) * 20));
            Canvas.SetLeft(submarine, (rnd.Next(0, 8) * 20));
            submarine_location.X = (Canvas.GetLeft(submarine) / 20);
            submarine_location.Y = (Canvas.GetTop(submarine) / 20);
            
            //Cruiser placement
            Ellipse Cruiser = new Ellipse();
            Cruiser.Name = "Cruiser";
            Cruiser.Width = 60;
            Cruiser.Height = 20;
            Cruiser.Fill = new SolidColorBrush(Colors.Teal);
            Cruiser.IsEnabled = false;
            var cru = (UIElement)LogicalTreeHelper.FindLogicalNode(Player_Canvas, "Cruiser");
            Player_Canvas.Children.Remove(cru);
            Player_Canvas.Children.Add(Cruiser);
            Point cruiser_location = new Point();
            //After the first ship is placed, check if the ship intersects with any previous ships placed
            //by checking if it overlaps or if the last place on the new ship intersects with the beginning place of
            //the placed ship
           do
            {
                cruiser_location.X = rnd.Next(0, 8);
                cruiser_location.Y = rnd.Next(0, 10);
                for (int x = 0; x < 3; x++)
                {
                    if ((submarine_location.Y == cruiser_location.Y &&
                        submarine_location.X + x == cruiser_location.X + x) && (submarine_location.X == cruiser_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    else
                    {
                        match = false;
                    }
                }
            } while (match == true);
            Canvas.SetTop(Cruiser, cruiser_location.Y * 20);
            Canvas.SetLeft(Cruiser, cruiser_location.X * 20);

            //Patrol boat placement
            Ellipse patrol = new Ellipse();
            patrol.Name = "Patrol";
            patrol.Width = 40;
            patrol.Height = 20;
            patrol.Fill = new SolidColorBrush(Colors.Teal);
            patrol.IsEnabled = false;
            var pat = (UIElement)LogicalTreeHelper.FindLogicalNode(Player_Canvas, "Patrol");
            Player_Canvas.Children.Remove(pat);
            Player_Canvas.Children.Add(patrol);
            Point patrol_location = new Point();
            //After the first ship is placed, check if the ship intersects with any previous ships placed
            //by checking if it overlaps or if the last place on the new ship intersects with the beginning place of
            //the placed ship
            do
            {
                patrol_location.X = rnd.Next(0, 9);
                patrol_location.Y = rnd.Next(0, 10);
                for (int x = 0; x < 2; x++)
                {
                    if ((submarine_location.Y == patrol_location.Y &&
                        submarine_location.X + x == patrol_location.X + x) && (submarine_location.X == patrol_location.X + x))
                    {
                        match = true;
                        break;
                    }

                    if ((cruiser_location.Y == patrol_location.Y &&
                    cruiser_location.X + x == patrol_location.X + x) && (cruiser_location.X == patrol_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    
                    else
                    {
                        match = false;
                    }
                }
            } while (match == true);
            Canvas.SetTop(patrol, (patrol_location.Y * 20));
            Canvas.SetLeft(patrol, (patrol_location.X * 20));

            //Carrier placement
            Ellipse carrier = new Ellipse();
            carrier.Name = "Carrier";
            carrier.Width = 100;
            carrier.Height = 20;
            carrier.Fill = new SolidColorBrush(Colors.Teal);
            carrier.IsEnabled = false;
            var car = (UIElement)LogicalTreeHelper.FindLogicalNode(Player_Canvas, "Carrier");
            Player_Canvas.Children.Remove(car);
            Player_Canvas.Children.Add(carrier);
            Point carrier_location = new Point();
            //After the first ship is placed, check if the ship intersects with any previous ships placed
            //by checking if it overlaps or if the last place on the new ship intersects with the beginning place of
            //the placed ship
            do
            {
                carrier_location.X = rnd.Next(0, 6);
                carrier_location.Y = rnd.Next(0, 10);
                for (int x = 0; x < 5; x++)
                {
                    if ((submarine_location.Y == carrier_location.Y &&
                        submarine_location.X + x == carrier_location.X + x) && (submarine_location.X == carrier_location.X + x))
                    {
                        match = true;
                        break;
                    }

                    if ((cruiser_location.Y == carrier_location.Y &&
                    cruiser_location.X + x == carrier_location.X + x) && (cruiser_location.X == carrier_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    if ((patrol_location.Y == carrier_location.Y &&
                    patrol_location.X + x == carrier_location.X + x) && (patrol_location.X == carrier_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    else
                    {
                        match = false;
                    }
                }
            } while (match == true);
            Canvas.SetTop(carrier, (carrier_location.Y * 20));
            Canvas.SetLeft(carrier, (carrier_location.X * 20));

            //Battleship placement
            Ellipse battleship = new Ellipse();
            battleship.Name = "Battleship";
            battleship.Width = 80;
            battleship.Height = 20;
            battleship.Fill = new SolidColorBrush(Colors.Teal);
            battleship.IsEnabled = false;
            var bat = (UIElement)LogicalTreeHelper.FindLogicalNode(Player_Canvas, "Battleship");
            Player_Canvas.Children.Remove(bat);
            Player_Canvas.Children.Add(battleship);
            Point battleship_location = new Point();
            //After the first ship is placed, check if the ship intersects with any previous ships placed
            //by checking if it overlaps or if the last place on the new ship intersects with the beginning place of
            //the placed ship
            do
            {
                battleship_location.X = rnd.Next(0, 6);
                battleship_location.Y = rnd.Next(0, 10);
                for (int x = 0; x < 4; x++)
                {
                    if ((submarine_location.Y == battleship_location.Y &&
                        submarine_location.X + x == battleship_location.X + x) && (submarine_location.X == battleship_location.X + x))
                    {
                        match = true;
                        break;
                    }

                    if ((cruiser_location.Y == battleship_location.Y &&
                    cruiser_location.X + x == battleship_location.X + x) && (cruiser_location.X == battleship_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    if ((patrol_location.Y == battleship_location.Y &&
                    patrol_location.X + x == battleship_location.X + x) && (patrol_location.X == battleship_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    if ((carrier_location.Y == battleship_location.Y &&
                    carrier_location.X + x == battleship_location.X + x) && (carrier_location.X == battleship_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    else
                    {
                        match = false;
                    }
                }
            } while (match == true);

            Canvas.SetTop(battleship, (battleship_location.Y * 20));
            Canvas.SetLeft(battleship, (battleship_location.X * 20));
            //Create the player with the ships and ship locations
            humanPlayer = new Player(submarine_location, patrol_location, carrier_location, 
                                     cruiser_location, battleship_location);
            //Allow for confirmation of ship positions
            Confirm_Ships_Button.IsEnabled = true;
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
            double valueX = Math.Floor(Math.Abs(point.X - testgrid.gridWidthStart) / 20);
            double valueY = Math.Floor(Math.Abs(point.Y - testgrid.gridHeightStart) / 20);

            testgrid.storeFireLocation(valueX, valueY);
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
            Canvas.SetTop(selection, (testgrid.y_fire_coordinate * 20));
            Canvas.SetLeft(selection, (testgrid.x_fire_coordinate * 20));
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




            Ellipse submarine = new Ellipse();
            Random rnd = new Random();
            bool match = false;
            Point submarine_location = new Point();
            submarine.Name = "SSBN";
            submarine.Width = 60;
            submarine.Height = 20;
            submarine.Fill = new SolidColorBrush(Colors.Teal);
            submarine.IsEnabled = false;
            submarine.Visibility = Visibility.Hidden;
            var sub = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "SSBN");
            Enemy_Canvas.Children.Remove(sub);
            Enemy_Canvas.Children.Add(submarine);


            Canvas.SetTop(submarine, (rnd.Next(0, 10) * 20));
            Canvas.SetLeft(submarine, (rnd.Next(0, 8) * 20));
            submarine_location.X = (Canvas.GetLeft(submarine) / 20);
            submarine_location.Y = (Canvas.GetTop(submarine) / 20);

            Ellipse Cruiser = new Ellipse();
            Cruiser.Name = "Cruiser";
            Cruiser.Width = 60;
            Cruiser.Height = 20;
            Cruiser.Fill = new SolidColorBrush(Colors.Teal);
            Cruiser.Visibility = Visibility.Hidden;
            Cruiser.IsEnabled = false;
            var cru = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "Cruiser");
            Enemy_Canvas.Children.Remove(cru);
            Enemy_Canvas.Children.Add(Cruiser);
            Point cruiser_location = new Point();
            //After the first ship is placed, check if the ship intersects with any previous ships placed
            //by checking if it overlaps or if the last place on the new ship intersects with the beginning place of
            //the placed ship
            do
            {
                cruiser_location.X = rnd.Next(0, 8);
                cruiser_location.Y = rnd.Next(0, 10);
                for (int x = 0; x < 3; x++)
                {
                    if ((submarine_location.Y == cruiser_location.Y &&
                        submarine_location.X + x == cruiser_location.X + x) && (submarine_location.X == cruiser_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    else
                    {
                        match = false;
                    }
                }
            } while (match == true);

            Canvas.SetTop(Cruiser, cruiser_location.Y * 20);
            Canvas.SetLeft(Cruiser, cruiser_location.X * 20);




            Ellipse patrol = new Ellipse();
            patrol.Name = "Patrol";
            patrol.Width = 40;
            patrol.Height = 20;
            patrol.Fill = new SolidColorBrush(Colors.Teal);
            patrol.Visibility = Visibility.Hidden;
            patrol.IsEnabled = false;
            var pat = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "Patrol");
            Enemy_Canvas.Children.Remove(pat);
            Enemy_Canvas.Children.Add(patrol);
            Point patrol_location = new Point();
            //After the first ship is placed, check if the ship intersects with any previous ships placed
            //by checking if it overlaps or if the last place on the new ship intersects with the beginning place of
            //the placed ship
            do
            {
                patrol_location.X = rnd.Next(0, 9);
                patrol_location.Y = rnd.Next(0, 10);
                for (int x = 0; x < 2; x++)
                {
                    if ((submarine_location.Y == patrol_location.Y &&
                        submarine_location.X + x == patrol_location.X + x) && (submarine_location.X == patrol_location.X + x))
                    {
                        match = true;
                        break;
                    }

                    if ((cruiser_location.Y == patrol_location.Y &&
                    cruiser_location.X + x == patrol_location.X + x) && (cruiser_location.X == patrol_location.X + x))
                    {
                        match = true;
                        break;
                    }

                    else
                    {
                        match = false;
                    }
                }
            } while (match == true);

            Canvas.SetTop(patrol, (patrol_location.Y * 20));
            Canvas.SetLeft(patrol, (patrol_location.X * 20));





            Ellipse carrier = new Ellipse();
            carrier.Name = "Carrier";
            carrier.Width = 100;
            carrier.Height = 20;
            carrier.Fill = new SolidColorBrush(Colors.Teal);
            carrier.Visibility = Visibility.Hidden;
            carrier.IsEnabled = false;
            var car = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "Carrier");
            Enemy_Canvas.Children.Remove(car);
            Enemy_Canvas.Children.Add(carrier);
            Point carrier_location = new Point();
            //After the first ship is placed, check if the ship intersects with any previous ships placed
            //by checking if it overlaps or if the last place on the new ship intersects with the beginning place of
            //the placed ship
            do
            {
                carrier_location.X = rnd.Next(0, 6);
                carrier_location.Y = rnd.Next(0, 10);
                for (int x = 0; x < 5; x++)
                {
                    if ((submarine_location.Y == carrier_location.Y &&
                        submarine_location.X + x == carrier_location.X + x) && (submarine_location.X == carrier_location.X + x))
                    {
                        match = true;
                        break;
                    }

                    if ((cruiser_location.Y == carrier_location.Y &&
                    cruiser_location.X + x == carrier_location.X + x) && (cruiser_location.X == carrier_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    if ((patrol_location.Y == carrier_location.Y &&
                    patrol_location.X + x == carrier_location.X + x) && (patrol_location.X == carrier_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    else
                    {
                        match = false;
                    }
                }
            } while (match == true);

            Canvas.SetTop(carrier, (carrier_location.Y * 20));
            Canvas.SetLeft(carrier, (carrier_location.X * 20));



            Ellipse battleship = new Ellipse();
            battleship.Name = "Battleship";
            battleship.Width = 80;
            battleship.Height = 20;
            battleship.Fill = new SolidColorBrush(Colors.Teal);
            battleship.Visibility = Visibility.Hidden;
            battleship.IsEnabled = false;
            var bat = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "Battleship");
            Enemy_Canvas.Children.Remove(bat);
            Enemy_Canvas.Children.Add(battleship);
            Point battleship_location = new Point();
            //After the first ship is placed, check if the ship intersects with any previous ships placed
            //by checking if it overlaps or if the last place on the new ship intersects with the beginning place of
            //the placed ship
            do
            {
                battleship_location.X = rnd.Next(0, 6);
                battleship_location.Y = rnd.Next(0, 10);
                for (int x = 0; x < 4; x++)
                {
                    if ((submarine_location.Y == battleship_location.Y &&
                        submarine_location.X + x == battleship_location.X + x) && (submarine_location.X == battleship_location.X + x))
                    {
                        match = true;
                        break;
                    }

                    if ((cruiser_location.Y == battleship_location.Y &&
                    cruiser_location.X + x == battleship_location.X + x) && (cruiser_location.X == battleship_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    if ((patrol_location.Y == battleship_location.Y &&
                    patrol_location.X + x == battleship_location.X + x) && (patrol_location.X == battleship_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    if ((carrier_location.Y == battleship_location.Y &&
                    carrier_location.X + x == battleship_location.X + x) && (carrier_location.X == battleship_location.X + x))
                    {
                        match = true;
                        break;
                    }
                    else
                    {
                        match = false;
                    }
                }
            } while (match == true);

            Canvas.SetTop(battleship, (battleship_location.Y * 20));
            Canvas.SetLeft(battleship, (battleship_location.X * 20));



            CPU = new ComputerPlayer(submarine_location, patrol_location, carrier_location, 
                                     cruiser_location, battleship_location);
        }

        private void Retreat_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Enemy_Turn()
        {
            bool missOrHit = true;
            Random rnd = new Random();
            int guessX = 0;
            int guessY = 0;

            guessX = rnd.Next(0, 10);
            guessY = rnd.Next(0, 10);

            //Check if the random guess hit the player ship
            //and if the hit ship is destroyed
            for (int x = 0; x < humanPlayer.ships.Length; x++)
            {
                if (guessY == humanPlayer.ships[x].location.startLocationY)
                {
                    for (int y = 0; y < humanPlayer.ships[x].location.length.Length; y++)
                    {
                        if (guessX == humanPlayer.ships[x].location.length[y])
                        {
                            humanPlayer.ships[x].takeHit();
                            if (humanPlayer.ships[x].health == 0)
                            {
                                MessageBox.Show("Your " + humanPlayer.ships[x].name + " has been destroyed!");
                                humanPlayer.totalShips--;
                            }
                            if (humanPlayer.totalShips == 0)
                            {
                                End_State();
                            }
                            missOrHit = false;
                            break;
                        }
                    }
                }
            }


            if (missOrHit == true)
            {
                Rectangle miss = new Rectangle();
                miss.Width = 20;
                miss.Height = 20;
                miss.Fill = new SolidColorBrush(Colors.Aquamarine);
                miss.Opacity = .7;

                Player_Canvas.Children.Add(miss);

                Canvas.SetTop(miss, (guessY * 20));
                Canvas.SetLeft(miss, (guessX * 20));
                enemyMissShot++;
                Miss_Ratio_Enemy.Content = "Miss: " + enemyMissShot;

            }
            else
            {
                Rectangle hit = new Rectangle();
                hit.Width = 20;
                hit.Height = 20;
                hit.Fill = new SolidColorBrush(Colors.Red);
                hit.Opacity = .9;

                Player_Canvas.Children.Add(hit);

                Canvas.SetTop(hit, (guessY * 20));
                Canvas.SetLeft(hit, (guessX * 20));
                enemyHitTarget++;
                Hit_Ratio_Enemy.Content = "Hit: " + enemyHitTarget;
            }
        }
        private bool Perform_Turn()
        {
            bool missOrHit = true;
            var sele = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "selection");
            //Check if the random guess hit the player ship
            //and if the hit ship is destroyed
            for (int x = 0; x < CPU.ships.Length; x++)
            {
                if (CPU.ships[x] != null)
                {
                    if ((Canvas.GetTop(sele) / 20) == CPU.ships[x].location.startLocationY)
                    {
                        for (int y = 0; y < CPU.ships[x].location.length.Length; y++)
                        {
                            if (Canvas.GetLeft(sele) / 20 == CPU.ships[x].location.length[y])
                            {
                                CPU.ships[x].takeHit();
                                if (CPU.ships[x].health == 0)
                                {
                                    MessageBox.Show("You destroyed the " + CPU.ships[x].name + "!");
                                    CPU.totalShips--;
                                    //Show the cooresponding destroyed ship
                                    switch (x)
                                    {
                                        //SSBN = Submarine 
                                        case 0:
                                            var sub = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "SSBN");
                                            sub.Visibility = Visibility.Visible;
                                            break;
                                        case 1:
                                            var cru = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "Cruiser");
                                            cru.Visibility = Visibility.Visible;
                                            break;
                                        case 2:
                                            var pat = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "Patrol");
                                            pat.Visibility = Visibility.Visible;
                                            break;
                                        case 3:
                                            var car = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "Carrier");
                                            car.Visibility = Visibility.Visible;
                                            break;
                                        case 4:
                                            var bat = (UIElement)LogicalTreeHelper.FindLogicalNode(Enemy_Canvas, "Battleship");
                                            bat.Visibility = Visibility.Visible;
                                            break;
                                    }
                                }
                                if (CPU.totalShips == 0)
                                {
                                    End_State();
                                }
                                missOrHit = false;
                                break;
                            }
                        }
                    }
                }
            }
            return missOrHit;
        }
        private void End_State()
        {
            Fire_Missile.Visibility = Visibility.Hidden;
            Retreat.Visibility = Visibility.Hidden;
            Restart_Game_Button.Visibility = Visibility.Visible;
            Exit_Game_Button.Visibility = Visibility.Visible;


            if(CPU.totalShips == 0)
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
    }
}

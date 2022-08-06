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
using System.Windows.Threading;

namespace WpfPakmenGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        int score = 0;
        int packmenSpeed = 8;
        int ghostSpeed = 6;
        double ghost2speed = 3;
        bool leftTern = true;
        bool gameIsStart = false, startPuse = false;
        bool LeftTurnG3 = false;

        public MainWindow()
        {
            InitializeComponent();
            GameSetup();

        }

        private void speedClick(object sender, RoutedEventArgs e)
        {
            ghostSpeed++;
            speedGhost.Content = ghostSpeed;
            ghost2speed = ghost2speed + 0.5;

        }

        private void startclick(object sender, RoutedEventArgs e)
        {
            if (startPuse == false)
            {
                gameIsStart = true;
                startButton.Content = "Puse";
                startPuse = true;
            }
            else if (startPuse == true)
            {
                gameIsStart = false;
                startButton.Content = "Start";
                startPuse = false;
            }




        }



        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            if (gameIsStart)
            {
                if (e.Key == Key.Left && noLeft == false)
                {
                    goRight = goUp = goDown = false;
                    noRight = noUp = noDown = false;
                    goLeft = true;
                    pacmen.RenderTransform = new RotateTransform(-180, pacmen.Width / 2, pacmen.Height / 2);
                }
                if (e.Key == Key.Right && noRight == false)
                {
                    goLeft = goUp = goDown = false;
                    noLeft = noUp = noDown = false;
                    goRight = true;
                    pacmen.RenderTransform = new RotateTransform(0, pacmen.Width / 2, pacmen.Height / 2);
                }
                if (e.Key == Key.Up && noUp == false)
                {
                    goLeft = goRight = goDown = false;
                    noLeft = noRight = noDown = false;
                    goUp = true;
                    pacmen.RenderTransform = new RotateTransform(-90, pacmen.Width / 2, pacmen.Height / 2);
                }
                if (e.Key == Key.Down && noDown == false)
                {
                    goLeft = goUp = goRight = false;
                    noLeft = noUp = noRight = false;
                    goDown = true;
                    pacmen.RenderTransform = new RotateTransform(90, pacmen.Width / 2, pacmen.Height / 2);
                }

            }

        }
        private void GameSetup()
        {
            myCanvas.Focus();
            timer.Tick += TimerLoop;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
            ImageBrush pacCloseMouth = new ImageBrush();
            pacCloseMouth.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacMouthClose.png"));
            ImageBrush cherryImage = new ImageBrush();
            cherryImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/cherry.png"));
            cherry.Fill = cherryImage;
            ImageBrush pacmanImage = new ImageBrush();
            pacmanImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacmen.png"));
            pacmen.Fill = pacmanImage;
            ImageBrush ghost1Img = new ImageBrush();
            ghost1Img.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/ghost1.png"));
            ghost1.Fill = ghost1Img;
            ImageBrush ghost2Img = new ImageBrush();
            ghost2Img.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/ghost2.png"));
            ghost2.Fill = ghost2Img;
            ImageBrush ghost3Img = new ImageBrush();
            ghost3Img.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/ghost3.png"));
            ghost3.Fill = ghost3Img;
        }

        private void TimerLoop(object sender, EventArgs e)
        {
            scoreTexst.Content = "Score : " + score;
            if (gameIsStart)
            {
                if (goLeft)
                {
                    Canvas.SetLeft(pacmen, Canvas.GetLeft(pacmen) - packmenSpeed);
                }
                if (goRight)
                {
                    Canvas.SetLeft(pacmen, Canvas.GetLeft(pacmen) + packmenSpeed);
                }
                if (goUp)
                {
                    Canvas.SetTop(pacmen, Canvas.GetTop(pacmen) - packmenSpeed);
                }
                if (goDown)
                {
                    Canvas.SetTop(pacmen, Canvas.GetTop(pacmen) + packmenSpeed);
                }
                if (goRight && Application.Current.MainWindow.Width <= Canvas.GetLeft(pacmen) + 50)
                {
                    goRight = false;
                    noRight = true;
                }
                if (goLeft && 3 >= Canvas.GetLeft(pacmen))
                {
                    goLeft = false;
                    noLeft = true;
                }
                if (goUp && 3 >= Canvas.GetTop(pacmen))
                {
                    goUp = false;
                    noUp = true;
                }
                if (goDown && Application.Current.MainWindow.Height <= Canvas.GetTop(pacmen) + 60)
                {
                    goDown = false;
                    noDown = true;
                }
                Rect pacmenHitBox = new Rect(Canvas.GetLeft(pacmen), Canvas.GetTop(pacmen), pacmen.Width, pacmen.Height);
                foreach (var ric in myCanvas.Children.OfType<Rectangle>())
                {
                    Rect hitbox = new Rect(Canvas.GetLeft(ric), Canvas.GetTop(ric), ric.Width, ric.Height);

                    if ((string)ric.Tag == "Wall")
                    {

                        if (goLeft && pacmenHitBox.IntersectsWith(hitbox))
                        {
                            Canvas.SetLeft(pacmen, Canvas.GetLeft(pacmen) + 10);
                            goLeft = false;
                            noLeft = true;
                        }
                        if (goRight && pacmenHitBox.IntersectsWith(hitbox))
                        {
                            Canvas.SetLeft(pacmen, Canvas.GetLeft(pacmen) - 10);
                            goRight = false;
                            noRight = true;
                        }
                        if (goUp && pacmenHitBox.IntersectsWith(hitbox))
                        {
                            Canvas.SetTop(pacmen, Canvas.GetTop(pacmen) + 10);
                            goUp = false;
                            noUp = true;
                        }
                        if (goDown && pacmenHitBox.IntersectsWith(hitbox))
                        {
                            Canvas.SetTop(pacmen, Canvas.GetTop(pacmen) - 10);
                            goDown = false;
                            noDown = true;
                        }
                    }
                    if ((string)ric.Tag == "coin")
                    {
                        if (pacmenHitBox.IntersectsWith(hitbox) && ric.Visibility == Visibility.Visible)
                        {
                            ric.Visibility = Visibility.Hidden;
                            score++;
                            if (score == 92)
                            {
                                EndGame("YOU WON!");
                                scoreBox.Text = "Max score - win!";
                            }
                        }
                    }
                    if ((string)ric.Tag == "coinS")
                    {
                        if (pacmenHitBox.IntersectsWith(hitbox) && ric.Visibility == Visibility.Visible)
                        {
                            ric.Visibility = Visibility.Hidden;
                            packmenSpeed = packmenSpeed + 2;
                        }
                    }


                    if ((string)ric.Tag == "ghost")
                    {
                        if (ric.Name == "ghost1")
                        {
                            Ghost1Move(ric);
                        }
                        if (ric.Name == "ghost2")
                        {
                            Ghost2Move(ric);
                        }
                        if (ric.Name == "ghost3")
                        {
                            Ghost3Move(ric);
                        }
                        if (pacmenHitBox.IntersectsWith(hitbox))// ghost hit player
                        {
                            EndGame($"youLose , you had only {92 - score} pices to collect");
                        }
                    }


                }
            }

        }
        private void Ghost2Move(Rectangle g2)
        {

            if (Canvas.GetLeft(pacmen) > Canvas.GetLeft(g2))//ghost go right
            {
                Canvas.SetLeft(g2, Canvas.GetLeft(g2) + ghost2speed);

            }
            if (Canvas.GetLeft(pacmen) < Canvas.GetLeft(g2))//ghost go left
            {
                Canvas.SetLeft(g2, Canvas.GetLeft(g2) - ghost2speed);
            }
            if (Canvas.GetTop(pacmen) < Canvas.GetTop(g2))//ghost go up
            {
                Canvas.SetTop(g2, Canvas.GetTop(g2) - ghost2speed);
            }
            if (Canvas.GetTop(pacmen) > Canvas.GetTop(g2))//ghost go down
            {
                Canvas.SetTop(g2, Canvas.GetTop(g2) + ghost2speed);
            }

        }
        private void Ghost1Move(Rectangle g1)
        {

            if (Canvas.GetLeft(g1) >= 100 && leftTern)
            {
                Canvas.SetLeft(g1, Canvas.GetLeft(g1) - ghostSpeed);
                if (Canvas.GetLeft(g1) <= 100)
                {
                    leftTern = false;
                }
            }
            if ((Canvas.GetLeft(g1) <= 700) && leftTern == false)
            {
                Canvas.SetLeft(g1, Canvas.GetLeft(g1) + ghostSpeed);
                if (Canvas.GetLeft(g1) >= 700)
                {
                    leftTern = true;
                }
            }
        }
        private void Ghost3Move(Rectangle g3)
        {

            if (Canvas.GetLeft(g3) >= 30 && LeftTurnG3)
            {
                Canvas.SetLeft(g3, Canvas.GetLeft(g3) - ghostSpeed);
                if (Canvas.GetLeft(g3) <= 201)
                {
                    LeftTurnG3 = false;
                }
            }
            if ((Canvas.GetLeft(g3) < 530) && LeftTurnG3 == false)
            {
                Canvas.SetLeft(g3, Canvas.GetLeft(g3) + ghostSpeed);
                if (Canvas.GetLeft(g3) >= 530)
                {
                    LeftTurnG3 = true;
                }
            }
        }
        private void EndGame(string str)
        {
            timer.Stop();
            MessageBox.Show(str, "End of the game");
            Application.Current.Shutdown();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            scoreBox.Text = (92 - score).ToString();
        }
        private void ghostMove()
        {

        }
    }
}

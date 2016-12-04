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
using System.Windows.Shapes;

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Gameboard : Window
    {
        public Brush[] coloursToGuess = new Brush[4];
        Ellipse[,] bigPieces = new Ellipse[4, 11];
        Ellipse[,] smallPieces = new Ellipse[4, 11];
        int guess = -1;
        public Gameboard()
        {
              
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


            //rysowanie dużych kółek
            for (int y = 0; y < 4; y++)
            {

                for (int x = 0; x < 11; x++)
                {
                    Ellipse piece = new Ellipse();
                    piece.Style = (Style)Application.Current.FindResource("BigPiece");
                    piece.Visibility = System.Windows.Visibility.Hidden;
                    this.theGrid.Children.Add(piece);
                    Grid.SetColumn(piece, y);
                    Grid.SetRow(piece, x);
                    bigPieces[y, x] = piece;
                    piece.Tag = 0;
                    piece.MouseDown += new MouseButtonEventHandler(Piece_Clicked);

                }

            }

            //rysowanie małych kółek
            for (int y = 0; y <= 3; y += 2)
            {

                for (int x = 0; x < 10; x++)
                {
                    StackPanel stack = new StackPanel();
                    theGrid.Children.Add(stack);
                    Grid.SetColumn(stack, y + 5);
                    Grid.SetRow(stack, x);

                    Ellipse piece = new Ellipse();
                    piece.Style = (Style)FindResource("SmallPiece");
                    piece.Visibility = System.Windows.Visibility.Hidden;
                    stack.Children.Add(piece);
                    smallPieces[y, x] = piece;

                    Ellipse piece2 = new Ellipse();
                    piece2.Style = (Style)FindResource("SmallPiece");
                    piece2.Visibility = System.Windows.Visibility.Hidden;
                    stack.Children.Add(piece2);
                    smallPieces[y+1, x] = piece2;

                }
            }

            //widoczność do testów
            bigPieces[0, 10].Visibility = Visibility.Visible;
            bigPieces[1, 10].Visibility = Visibility.Visible;
            bigPieces[2, 10].Visibility = Visibility.Visible;
            bigPieces[3, 10].Visibility = Visibility.Visible;

            //rysownaie planszy
            SetupGame();

            //wywołanie sprawdzania odpowiedzi i uruchomienie nastepnej proby
                NextGuess();

            //wypełnianie klucza
                bigPieces[0, 10].Fill = coloursToGuess[0];
                bigPieces[1, 10].Fill = coloursToGuess[1];
                bigPieces[2, 10].Fill = coloursToGuess[2];
                bigPieces[3, 10].Fill = coloursToGuess[3];
            }
      
        //zmiana koloru kółka po kliknieciu
        public void Piece_Clicked(object sender, MouseButtonEventArgs e)
        {
            Ellipse piece = sender as Ellipse;
            piece.Tag = (int)piece.Tag + 1;
            if ((int)piece.Tag > 5)
            {
                piece.Tag = 0;
            }
            piece.Fill = intToBrush((int)piece.Tag);

        }

        //logika sprawdzania odpowiedzi
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int correct = 0;
            bool[] corrects = new bool[4];

            int correctColour = 0;
            bool[] correctColours = new bool[4];

            for (int x = 0; x < 4; x++)
            {
                if (coloursToGuess[x].Equals(bigPieces[x, guess].Fill))
                {
                    correct += 1;
                    corrects[x] = true;
                }
                
                if (correct == 4)
                {
                    MessageBox.Show("Wygrales");
                    RestartGame();
                }
            }
            for (int x = 0; x < 4; x++)
            {
                for (int i = 0; i < 4 ; i++)
                {
                    if (corrects[i] == false && corrects[x] == false)
                    {
                        if (coloursToGuess[x].Equals(bigPieces[i, guess].Fill))
                        {
                            correctColour += 1;
                            correctColours[i] = true;
                        }
                    }
                }
            }

            //wypełnianie małych kółek odpowiednim kolorem 
            for (int x = 0; x < correct; x++)
            {
                smallPieces[x, guess].Visibility = Visibility.Visible;
                smallPieces[x, guess].Fill = Brushes.Red;
            }
            
            for (int x = correct; x < ((correctColour + correct)); x++)
            {
                smallPieces[x, guess].Visibility = Visibility.Visible;
                smallPieces[x, guess].Fill = Brushes.Black;

            }

            NextGuess();
            
        }

        //zmiana koloru na int
        public Brush intToBrush(int num)
        {
            switch (num)
            {
                case 1:
                    return Brushes.Orange;
                case 2:
                    return Brushes.Blue;
                case 3:
                    return Brushes.LightGreen;
                case 4:
                    return Brushes.Red;
                case 5:
                    return Brushes.Yellow;
                default:
                    return Brushes.Purple;
            }

        }

        //losowanie bez powtorzen kolorow do klucza
        private Random SetupGame()
        {
            var nums = Enumerable.Range(0, 6).ToArray();
            var rnd = new Random();

            int i = 0;
            for (i = 0; i < nums.Length; ++i)
            {
                int randomIndex = rnd.Next(nums.Length);
                int temp = nums[randomIndex];
                nums[randomIndex] = nums[i];
                nums[i] = temp;
            }

            Random rand = new Random();
            i = 0;
            for (int x = 0; x < 4; x++)
            {
                coloursToGuess[x] = intToBrush(nums[i]);
                i++;
            }
            return null;

        }

        //odblokowywanie nastepnego wiersza, blokowanie poprzedniego
        public void NextGuess()
        {
            if (guess > -1)
            {
                for (int y = 0; y < 4; y++)
                {
                    bigPieces[y, guess].IsEnabled = false;
                }
            }
            guess += 1;
            for (int y = 0; y < 4; y++)
            {
                if (guess > 9)
                {
                    MessageBox.Show("Przegrałeś!");
                    break;
                   
                }
                bigPieces[y, guess].Visibility = Visibility.Visible;
               
            }
        }
        public void RestartGame()
        {      
            SetupGame();
        }

        //uruchamianie nowej gry
        private void Button_Click_Restart(object sender, RoutedEventArgs e)
        {
            Gameboard gameboard = new Gameboard();
            gameboard.Show();
            this.Close();
        }

        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
        

        }
    }
}

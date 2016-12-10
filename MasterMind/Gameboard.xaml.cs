using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for Gameboard.xaml
    /// </summary>
    public partial class Gameboard : UserControl
    {
        private Brush[] coloursToGuess = new Brush[4];
        Ellipse[,] bigPieces = new Ellipse[4, 11];
        Ellipse[,] smallPieces = new Ellipse[4, 11];
        int guess = -1;
        public Gameboard()
        {

            InitializeComponent();

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
                    smallPieces[y + 1, x] = piece2;
                }
            }

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
        private void Piece_Clicked(object sender, MouseButtonEventArgs e)
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
        private void Button_Click_Check(object sender, RoutedEventArgs e)
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
            }

            for (int x = 0; x < 4; x++)
            {
                for (int i = 0; i < 4; i++)
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
                bigPieces[x, guess].IsEnabled = false;
            }
            if (correct == 4)
            {
                MessageBox.Show("Wygrałeś!");
                this.Check.IsEnabled = false;             
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

            if (correct != 4)
            {
                NextGuess();
            }
        }


        //zmiana koloru na int
        private Brush intToBrush(int num)
        {
            switch (num)
            {
                case 1:
                    return Brushes.Orange;
                case 2:
                    return Brushes.Blue;
                case 3:
                    return Brushes.Green;
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
        private void NextGuess()
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
                bigPieces[y, guess].Visibility = Visibility.Visible;
            }

            if (guess > 9)
            {
                MessageBox.Show("Przegrałeś :(");
                this.Check.IsEnabled = false;

                for (int y = 0; y < 4; y++)
                {
                    bigPieces[y, guess].IsEnabled = false;
                }
            }
        }

        //powrót do menu
        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            this.Content = new Menu();
        }

        private void Button_Click_Key(object sender, RoutedEventArgs e)
        {
            //ujawnienie klucza
            for (int y = 0; y < 4; y++)
            {
                bigPieces[y, 10].Visibility = Visibility.Visible;
                bigPieces[y, 10].IsEnabled = false;
            }
        }
    }
}


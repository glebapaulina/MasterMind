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

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for Instruction.xaml
    /// </summary>
    public partial class Instruction : UserControl
    {
        public Instruction()
        {
            InitializeComponent();
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            var block = sender as TextBlock;
            block.Text = "Witaj w grze Mastermind! \n\nCelem gry jest odgadnięcie ukrytej kombinacji kolorów. Kombinacja składa się z czterech oczek. Każde oczko zostaje losowo pokryte jednym z sześciu następujących kolorów: pomarańczowy, niebieski, zielony, czerwony, żółty, fioletowy. Aby ułatwić Ci zadanie, kolory oczek w kombinacji nie powtarzają się Aby zgadywać kombinację, należy klikać na oczka aż każde z nich pokryje się wybranym kolorem. Następnie klikamy przycisk SPRAWDŹ, aby dowiedzieć się, czy ustawiona kombinacja jest poprawna. Na wysokości rzędu oczek sprawdzanej kombinacji pokazują się podpowiedzi. Czerwony punkt oznacza, że jedno oczko ma odpowiedni kolor. Czarny punkt oznacza, że kolor się zgadza, ale jest nim wypełnione niewłaściwe oczko. Jeśli się poddajesz, możesz podejrzeć kombinację, klikając przycisk POKAŻ KLUCZ. Na odgadnięcie kombinacji kolorów przysługuje 10 szans. \nPowodzenia!";
        }
    }
}

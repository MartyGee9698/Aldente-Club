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
using RistoranteApp;

namespace Aldente_Club
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menuWindow = new MainWindow();
            menuWindow.Show();
            this.Close();
        }

        private void Prenotazioni_Click(object sender, RoutedEventArgs e)
        {


            PrenotazioneWindow prenotazioneWindow = new PrenotazioneWindow();
            prenotazioneWindow.Show();
            this.Close();
        }
    }
}
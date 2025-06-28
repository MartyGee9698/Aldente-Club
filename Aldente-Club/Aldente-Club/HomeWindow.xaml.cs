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
            MessageBox.Show("Qui andrà la schermata del menu.");
            // Oppure: nuova finestra per il menu
        }

        private void Prenotazioni_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Qui andrà la schermata del menu.");
            // Oppure: nuova finestra per le prenotazioni

            //MainWindow prenotazioneWindow = new MainWindow();
            //prenotazioneWindow.Show();
            //this.Close();
        }
    }
}
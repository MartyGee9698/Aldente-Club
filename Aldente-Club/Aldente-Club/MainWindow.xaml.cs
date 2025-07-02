using System.Windows;
using RistoranteApp.Views;

namespace RistoranteApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Antipasto_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AntipastoPage();
        }

        private void Primo_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Primi();
        }

        private void Secondi_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new SecondiPage();
        }

        private void Contorni_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ContorniPage();
        }

        private void Dolci_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DolciPage();
        }
    }
}

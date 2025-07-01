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

        //private void Secondo_Click(object sender, RoutedEventArgs e)
        //{
        //    ContentArea.Content = new SecondoPage();
        //}

        //private void Dolce_Click(object sender, RoutedEventArgs e)
        //{
        //    ContentArea.Content = new DolcePage();
        //}

        //private void Bevande_Click(object sender, RoutedEventArgs e)
        //{
        //    ContentArea.Content = new BevandePage();
        //}
    }
}

using System.Windows;
using System.Windows.Controls;

namespace RistoranteApp.Views
{
    public partial class ContorniPage : UserControl
    {
        private MainWindow mainWindow;

        public ContorniPage(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
        }

        private void Aggiungi_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string nomePiatto)
            {
                mainWindow.AggiungiAlOrdine(nomePiatto);
                MessageBox.Show($"{nomePiatto} aggiunto all'ordine!");
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace RistoranteApp.Views
{
    public partial class SecondiPage : UserControl
    {
        public SecondiPage()
        {
            InitializeComponent();
        }

        private void Aggiungi_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string nomePiatto)
            {
                MessageBox.Show($"{nomePiatto} aggiunte all'ordine!");
            }
        }
    }
}

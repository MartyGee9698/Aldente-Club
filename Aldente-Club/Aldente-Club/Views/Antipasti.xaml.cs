using System.Windows;
using System.Windows.Controls;

namespace RistoranteApp.Views
{
    public partial class AntipastoPage : UserControl
    {
        public AntipastoPage()
        {
            InitializeComponent();
        }

        private void Aggiungi_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Bruschetta aggiunta all'ordine!");
        }
    }
}

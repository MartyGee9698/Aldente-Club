using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Controls;

namespace RistoranteApp.Views
{
    /// <summary>
    /// Interaction logic for Primi.xaml
    /// </summary>
    public partial class Primi : UserControl
    {
        public Primi()
        {
            InitializeComponent();
        }

        private void Aggiungi_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string nomePiatto)
            {
                MessageBox.Show($"{nomePiatto} aggiunto all'ordine!");
            }
        }
    }
}

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
    public partial class PrenotazioneWindow : Window
    {
        public PrenotazioneWindow()
        {
            InitializeComponent();

            DataPicker.DisplayDateStart = DateTime.Today;
            DataPicker.DisplayDateEnd = DateTime.Today.AddDays(30);
        }

        private void AggiungiPrenotazione_Click(object sender, RoutedEventArgs e)
        {
            string nome = NomeTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string telefono = TelefonoTextBox.Text.Trim();
            int numeroPersone = PersoneNumeric.Value ?? 1;
            DateTime? data = DataPicker.SelectedDate;
            string ora = (OraComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Validazioni base
            if (string.IsNullOrEmpty(nome))
            {
                MessageBox.Show("Inserisci il nome del cliente.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                MessageBox.Show("Inserisci un'email valida.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(telefono))
            {
                MessageBox.Show("Inserisci un numero di telefono.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (data == null)
            {
                MessageBox.Show("Seleziona una data.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(ora))
            {
                MessageBox.Show("Seleziona un orario.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string messaggio = $"Prenotazione registrata con successo:\n\n" +
                               $"👤 Nome: {nome}\n📧 Email: {email}\n📞 Telefono: {telefono}\n👥 Persone: {numeroPersone}\n📅 Data: {data:dd/MM/yyyy}\n🕒 Ora: {ora}";

            MessageBox.Show(messaggio, "Prenotazione Confermata", MessageBoxButton.OK, MessageBoxImage.Information);

            // Pulizia campi dopo conferma
            NomeTextBox.Text = "";
            EmailTextBox.Text = "";
            TelefonoTextBox.Text = "";
            PersoneNumeric.Value = 1;
            DataPicker.SelectedDate = null;
            OraComboBox.SelectedItem = null;
        }
    }
}


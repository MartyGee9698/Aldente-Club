using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Aldente_Club
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
                 DataPicker.DisplayDateStart = DateTime.Today;
            DataPicker.DisplayDateEnd = DateTime.Today.AddDays(30);
        }

        private void AggiungiPrenotazione_Click(object sender, RoutedEventArgs e)
        {
            // Recupera i valori inseriti
            string nome = NomeTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
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

            // Mostra popup di conferma
            string messaggio = $"Prenotazione registrata con successo:\n\n" +
                               $"👤 Nome: {nome}\n📧 Email: {email}\n👥 Persone: {numeroPersone}\n📅 Data: {data:dd/MM/yyyy}\n🕒 Ora: {ora}";

            MessageBox.Show(messaggio, "Prenotazione Confermata", MessageBoxButton.OK, MessageBoxImage.Information);

            // Opzionale: cancella i campi dopo la conferma
            NomeTextBox.Text = "";
            EmailTextBox.Text = "";
            PersoneNumeric.Value = 1;
            DataPicker.SelectedDate = null;
            OraComboBox.SelectedItem = null;
        }
    }

}

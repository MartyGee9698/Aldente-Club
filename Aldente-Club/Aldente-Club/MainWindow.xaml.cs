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
        }

        private void AggiungiPrenotazione_Click(object sender, RoutedEventArgs e)
        {
            string nome = NomeTextBox.Text.Trim();
            int persone = PersoneNumeric.Value ?? 1;
            DateTime? data = DataPicker.SelectedDate;
            string orario = (OraComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string email = EmailTextBox.Text.Trim();

            if (string.IsNullOrEmpty(nome))
            {
                MessageBox.Show("Inserisci il nome del cliente.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (persone < 1)
            {
                MessageBox.Show("Seleziona il numero di persone.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (data == null)
            {
                MessageBox.Show("Seleziona la data della prenotazione.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(orario))
            {
                MessageBox.Show("Seleziona un orario valido.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
            {
                MessageBox.Show("Inserisci una email valida.", "Errore", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Qui potresti salvare la prenotazione nel database o in una lista

            ConfermaTextBlock.Text = $"Prenotazione aggiunta con successo!\n" +
                                     $"Cliente: {nome}\n" +
                                     $"Persone: {persone}\n" +
                                     $"Data: {data.Value.ToShortDateString()} {orario}\n" +
                                     $"Email: {email}";

            // Resetta i campi (opzionale)
            NomeTextBox.Clear();
            PersoneNumeric.Value = 1;
            DataPicker.SelectedDate = null;
            OraComboBox.SelectedIndex = -1;
            EmailTextBox.Clear();
        }

    }
}
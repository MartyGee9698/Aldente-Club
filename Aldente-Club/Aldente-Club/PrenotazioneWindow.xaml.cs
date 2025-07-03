using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Aldente_Club
{
    public partial class PrenotazioneWindow : Window
    {
        private readonly string prenotazioniPath;

        public PrenotazioneWindow()
        {
            InitializeComponent();

            DataPicker.DisplayDateStart = DateTime.Today;
            DataPicker.DisplayDateEnd = DateTime.Today.AddDays(30);

            string cartellaDocumenti = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AldenteClub");
            Directory.CreateDirectory(cartellaDocumenti); // Crea la cartella se non esiste
            prenotazioniPath = Path.Combine(cartellaDocumenti, "prenotazioni.csv");
        }

        private void AggiungiPrenotazione_Click(object sender, RoutedEventArgs e)
        {
            string nome = NomeTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string telefono = TelefonoTextBox.Text.Trim();
            int numeroPersone = PersoneNumeric.Value ?? 1;
            DateTime? data = DataPicker.SelectedDate;
            string ora = (OraComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Validazioni
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

            // Ottieni nuovo ID prenotazione
            int idPrenotazione = GetNextPrenotazioneID();

            // Scrivi su file
            string record = $"{idPrenotazione};{nome};{email};{telefono};{numeroPersone};{data:yyyy-MM-dd};{ora}";
            try
            {
                File.AppendAllText(prenotazioniPath, record + Environment.NewLine);
                MessageBox.Show($"Prenotazione registrata con successo.\nID Prenotazione: {idPrenotazione}", "Conferma", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il salvataggio della prenotazione:\n{ex.Message}", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Reset campi
            NomeTextBox.Text = "";
            EmailTextBox.Text = "";
            TelefonoTextBox.Text = "";
            PersoneNumeric.Value = 1;
            DataPicker.SelectedDate = null;
            OraComboBox.SelectedItem = null;
        }

        private int GetNextPrenotazioneID()
        {
            if (!File.Exists(prenotazioniPath))
                return 1;

            var lines = File.ReadAllLines(prenotazioniPath);
            int maxId = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length > 0 && int.TryParse(parts[0], out int id))
                {
                    if (id > maxId) maxId = id;
                }
            }
            return maxId + 1;
        }

        private void TornaAllaHome_Click(object sender, RoutedEventArgs e)
        {
            var home = new Aldente_Club.HomeWindow();
            home.Show();
            this.Close();
        }
    }
}
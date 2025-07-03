using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using RistoranteApp;

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

        // Metodo per leggere l'ultimo ID numerico e restituire il prossimo
        private int GetNextID(string filePath)
        {
            if (!File.Exists(filePath))
                return 1;

            var lines = File.ReadAllLines(filePath);
            if (lines.Length <= 1) // solo intestazione o file vuoto
                return 1;

            int maxID = 0;
            for (int i = 1; i < lines.Length; i++) // salto intestazione
            {
                var parts = lines[i].Split(';');
                if (parts.Length > 0 && int.TryParse(parts[0], out int id))
                {
                    if (id > maxID)
                        maxID = id;
                }
            }
            return maxID + 1;
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

            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoryPath = System.IO.Path.Combine(documentsPath, "AlDenteClub");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filePath = System.IO.Path.Combine(directoryPath, "prenotazioni.csv");

                // Ottieni prossimo ID
                int nuovoID = GetNextID(filePath);

                string riga = $"{nuovoID};{nome};{email};{telefono};{numeroPersone};{data:dd/MM/yyyy};{ora}";

                if (!File.Exists(filePath))
                {
                    // Aggiungo intestazione con "ID" come prima colonna
                    File.WriteAllText(filePath, "ID;Nome;Email;Telefono;Persone;Data;Ora\n");
                }

                File.AppendAllText(filePath, riga + "\n");

                string messaggio = $"Prenotazione registrata con successo (ID {nuovoID}):\n\n" +
                                   $"👤 Nome: {nome}\n📧 Email: {email}\n📞 Telefono: {telefono}\n👥 Persone: {numeroPersone}\n📅 Data: {data:dd/MM/yyyy}\n🕒 Ora: {ora}";

                MessageBox.Show(messaggio, "Prenotazione Confermata", MessageBoxButton.OK, MessageBoxImage.Information);

               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore nel salvataggio della prenotazione:\n" + ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

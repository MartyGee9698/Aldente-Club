using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Aldente_Club;
using RistoranteApp.Views;

namespace RistoranteApp
{
    public partial class MainWindow : Window
    {
        private readonly string ordiniPath;

        public MainWindow()
        {
            InitializeComponent();

            string cartellaDocumenti = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AldenteClub");
            Directory.CreateDirectory(cartellaDocumenti);
            ordiniPath = Path.Combine(cartellaDocumenti, "ordini.csv");
        }

        public void AggiungiAlOrdine(string nomePiatto)
        {
            OrderListBox.Items.Add(nomePiatto);
        }

        private void Antipasto_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AntipastoPage(this);
            EvidenziaBottone(sender as Button);
        }

        private void Primo_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PrimiPage(this);
            EvidenziaBottone(sender as Button);
        }

        private void Secondi_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new SecondiPage(this);
            EvidenziaBottone(sender as Button);
        }

        private void Contorni_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ContorniPage(this);
            EvidenziaBottone(sender as Button);
        }

        private void Dolci_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DolciPage(this);
            EvidenziaBottone(sender as Button);
        }

        private void Bibite_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new BibitePage(this);
            EvidenziaBottone(sender as Button);
        }

        private void ClearOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderListBox.Items.Clear();
        }

        private void EvidenziaBottone(Button clickedButton)
        {
            foreach (var child in ((StackPanel)clickedButton.Parent).Children)
            {
                if (child is Button btn)
                {
                    btn.Background = (btn == clickedButton) ? System.Windows.Media.Brushes.DodgerBlue : System.Windows.Media.Brushes.LightGray;
                    btn.Foreground = (btn == clickedButton) ? System.Windows.Media.Brushes.White : System.Windows.Media.Brushes.Black;
                }
            }
        }

        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrderListBox.Items.Count == 0)
            {
                MessageBox.Show("L'ordine Ã¨ vuoto. Aggiungi almeno un piatto prima di confermare.", "Attenzione");
                return;
            }

            if (!int.TryParse(PrenotazioneIdTextBox.Text, out int idPrenotazione) || idPrenotazione <= 0)
            {
                MessageBox.Show("Inserisci un ID prenotazione valido.", "Errore");
                return;
            }

            var piatti = OrderListBox.Items.Cast<string>();
            string record = $"{idPrenotazione};{string.Join(",", piatti)}";

            try
            {
                File.AppendAllText(ordiniPath, record + Environment.NewLine);
                MessageBox.Show("Ordine confermato e salvato.", "Conferma");
                OrderListBox.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il salvataggio dell'ordine:\n{ex.Message}", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(PrenotazioneIdTextBox.Text, out int idPrenotazione) || idPrenotazione <= 0)
            {
                MessageBox.Show("Inserisci prima un ID prenotazione valido.", "Errore");
                return;
            }

            FeedbackWindow feedbackWindow = new FeedbackWindow(idPrenotazione);
            feedbackWindow.Show();
        }


        private void TornaAllaHome_Click(object sender, RoutedEventArgs e)
        {
            var home = new Aldente_Club.HomeWindow();
            home.Show();
            this.Close();
        }
    }
}
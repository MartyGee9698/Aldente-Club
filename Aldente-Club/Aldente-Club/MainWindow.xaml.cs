using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using RistoranteApp.Views;

namespace RistoranteApp
{
    public partial class MainWindow : Window
    {
        private int prenotazioneID;

        public MainWindow(int idPrenotazione)
        {
            InitializeComponent();
            prenotazioneID = idPrenotazione;
        }

        // Se vuoi un costruttore senza parametri per test o altro (opzionale)
        public MainWindow()
        {
            InitializeComponent();
            prenotazioneID = 0; // id non valido
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
                MessageBox.Show("L'ordine è vuoto. Aggiungi almeno un piatto prima di confermare.", "Attenzione");
                return;
            }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = System.IO.Path.Combine(documentsPath, "AlDenteClub");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = System.IO.Path.Combine(directoryPath, "ordini.csv");

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "ID_Prenotazione;Nome_Piatto\n");
            }

            foreach (var item in OrderListBox.Items)
            {
                string piatto = item.ToString();
                string riga = $"{prenotazioneID};{piatto}";
                File.AppendAllText(filePath, riga + "\n");
            }

            MessageBox.Show("Ordine salvato con successo.", "Conferma Ordine");

            OrderListBox.Items.Clear();
        }
    }
}

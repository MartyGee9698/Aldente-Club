using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace Aldente_Club
{
    public partial class FeedbackWindow : Window
    {
        private DispatcherTimer timer;
        private int idPrenotazione;
        private readonly string ordiniPath;

        public FeedbackWindow(int idPrenotazione)
        {
            InitializeComponent();
            this.idPrenotazione = idPrenotazione;

            string cartellaDocumenti = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AldenteClub");
            Directory.CreateDirectory(cartellaDocumenti);
            ordiniPath = Path.Combine(cartellaDocumenti, "ordini.csv");

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            RisultatoTextBlock.Visibility = Visibility.Collapsed;
            timer.Stop();
        }

        private void Star_Checked(object sender, RoutedEventArgs e)
        {
            var clickedStar = sender as ToggleButton;
            if (clickedStar == null) return;

            int starNumber = int.Parse(clickedStar.Name.Replace("Star", ""));
            SetStars(starNumber);
        }

        private void Star_Unchecked(object sender, RoutedEventArgs e)
        {
            var uncheckedStar = sender as ToggleButton;
            if (uncheckedStar == null) return;

            int starNumber = int.Parse(uncheckedStar.Name.Replace("Star", ""));
            SetStars(starNumber - 1);
        }

        private void SetStars(int count)
        {
            for (int i = 1; i <= 5; i++)
            {
                var star = this.FindName("Star" + i) as ToggleButton;
                if (star != null)
                {
                    star.Checked -= Star_Checked;
                    star.Unchecked -= Star_Unchecked;

                    star.IsChecked = i <= count;
                    star.Foreground = (i <= count) ? Brushes.Gold : Brushes.Gray;

                    star.Checked += Star_Checked;
                    star.Unchecked += Star_Unchecked;
                }
            }
        }

        private void InviaFeedback_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            int stelleSelezionate = 0;
            for (int i = 1; i <= 5; i++)
            {
                var star = this.FindName("Star" + i) as ToggleButton;
                if (star != null && star.IsChecked == true)
                    stelleSelezionate++;
            }

            string commento = CommentTextBox.Text.Trim();

            try
            {
                var righe = File.ReadAllLines(ordiniPath).ToList();

                for (int i = 0; i < righe.Count; i++)
                {
                    if (righe[i].StartsWith(idPrenotazione + ";"))
                    {
                        righe[i] += $";{stelleSelezionate};\"{commento.Replace("\"", "\"\"")}\"";
                        break;
                    }
                }

                File.WriteAllLines(ordiniPath, righe);

                RisultatoTextBlock.Text = $"Grazie per il tuo feedback! Hai valutato {stelleSelezionate} stelle.";
                RisultatoTextBlock.Visibility = Visibility.Visible;
                timer.Start();
                CommentTextBox.Clear();
                SetStars(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore nel salvataggio del feedback: " + ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

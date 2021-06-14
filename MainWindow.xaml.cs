using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using FolderBrowserForWPF;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Generator_pliku_ddd
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DriversList Drivers = new DriversList();
       

        private Regex OnlyInt = new Regex(@"^[0-9]+");


        public MainWindow()
        {
            // ustawienia początkowe aplikacji
            InitializeComponent();

            CopiesTB.Text = Properties.Settings.Default.LastAmount.ToString();
            SexFreqSlider.Value = Properties.Settings.Default.LastFrequency;

            DateFromDP.SelectedDate = DateTime.Today.AddDays(-20);
            DateToDP.SelectedDate = DateTime.Today.AddDays(20);

            var savePath = Properties.Settings.Default.LastPath;

            if(savePath != "")
            {
                SavePlaceTB.Text = savePath;
            }
            else
            {
                var programPath = Directory.GetCurrentDirectory() + @"\Generated Files";
                if (!Directory.Exists(programPath)) Directory.CreateDirectory(programPath);
                SavePlaceTB.Text = programPath;
            }

            Drivers.OnCountChange += OnDriversCountChange;

            // Wczytanie kierowców z pliku csv jeśli istnieje
            var DriversPath = Directory.GetCurrentDirectory() + @"\Drivers.csv";
            if (File.Exists(DriversPath))
            {
                try
                {
                    using (var reader = new StreamReader(DriversPath))
                    {
                        string line;

                        while((line = reader.ReadLine()) != null)
                        {
                            Drivers.Add(new Driver(line));
                            StatusTB.Text = "Wczytano " + Drivers.Count + " kierowców";
                        }
                    }
                } catch (Exception e)
                {
                    MessageBox.Show("Wystąpił błąd podczas wczytywania kierowców z pliku: \n" + DriversPath + " w " + (Drivers.Count + 1) + " lini", "Wystąpił błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        // Funkcja na zdarzenie zmiany rozmiaru listy kierowców
        private void OnDriversCountChange(DriversList sender, DriverEventArgs args)
        {
            Dispatcher.Invoke(()=> ExportBtn.IsEnabled = sender.Count > 0 );
        }

        // funkcja odpowiadająca za generowanie kierowców
        // potem odpala funkcje odpowiadającą za tworzenie plików
        private async void GnerateBtn_Click(object sender, RoutedEventArgs e)
        {
            var self = sender as Button;
            self.IsEnabled = false;

            var amount = int.Parse(CopiesTB.Text) - Drivers.Count;

            try
            {
                var task = DriverGenerator.Generete(amount > 0 ? (uint)amount : 0, SexFreqSlider.Value);
                StatusTB.Text = "Generowanie kierowców...";

                await task.ContinueWith(d =>
                {
                    var gDrivers = d.Result;

                    Dispatcher.Invoke(() => { StatusTB.Text = "Wygenerowano " + gDrivers.Count + " kierowców"; });

                    Drivers.AddRange(gDrivers);
                });
            } 
            catch(Exception err)
            {
                MessageBox.Show("Nie wygenerowano nowych kierowców", "api.gov.pl", MessageBoxButton.OK, MessageBoxImage.Information);
                StatusTB.Text = "Nie wygenerowano nowych kierowców";
            } 
            finally
            {
                Dispatcher.BeginInvoke((Action)(() => GenerateFile(Drivers)));
            }
        }

        // funkcja odpowiadająca za tworzenie plików
        private async void GenerateFile(DriversList Drivers)
        {
            var rand = RandomSingleton.GetInstance();
            var diff = (int)(DateToDP.SelectedDate - DateFromDP.SelectedDate).Value.TotalSeconds;
            var tasks = new List<Task>();
            int max = int.Parse(CopiesTB.Text);

            for(int i = 0; i< max; i++)
            {
                var driver = Drivers[i];
                var downloadDate = DateFromDP.SelectedDate.Value.AddSeconds(rand.Next(0, diff));
                var bytes = FileStructureConventer.Create(driver, downloadDate).ToArray();
                var fileName = SavePlaceTB.Text + $"\\C_{downloadDate.ToString("yyyyMMdd")}_{downloadDate.ToString("HHmmss")}_" +
                                $"{driver.Personals.FirstName[0]}_{driver.Personals.LastName}_{driver.Personals.ID}.ddd";

                tasks.Add(Task.Run(() => File.WriteAllBytes(fileName, bytes)));

                await tasks.Last().ContinueWith(tt =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        StatusTB.Text = $"Utworzono {i+1}/{max} plików";
                    });
                });
            }

            Task.WaitAll(tasks.ToArray());

            Process.Start("explorer.exe", SavePlaceTB.Text);
            GnerateBtn.IsEnabled = true;
        }

        private async void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            var self = sender as Button;
            self.IsEnabled = false;

            int max = int.Parse(CopiesTB.Text);

            using (var sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\Drivers.csv"))
            {
                for(var i = 0; i< max; i++)
                {
                    await sw.WriteLineAsync(Drivers[i].toCSV());
                    StatusTB.Text = "Eksportowanie kierowców... " + (i + 1) + "/" + max;
                }

            }
            StatusTB.Text = "Weksportowano " + Drivers.Count + " kierowców";
            self.IsEnabled = true;
        }

        private void ChocePlaceBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Dialog();
            dialog.FileName = SavePlaceTB.Text;
            if(dialog.ShowDialog() == true)
            {
                SavePlaceTB.Text = dialog.FileName;
                Properties.Settings.Default.LastPath = dialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void CopiesTB_OnlyInt(object sender, TextCompositionEventArgs e)
        {
            if (!OnlyInt.IsMatch(e.Text)) e.Handled = true;
        }


        private void CopiesTB_TextChange(object sender, TextChangedEventArgs e)
        {
            var self = sender as TextBox;
            if (self.Text.Length == 0) GnerateBtn.IsEnabled = false;
            else GnerateBtn.IsEnabled = true;
            Properties.Settings.Default.LastAmount = int.Parse(self.Text);
            Properties.Settings.Default.Save();

        }

        // zapisywanie wartości SexFrequency
        private void SexFrequencySaveValue(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            Properties.Settings.Default.LastFrequency = SexFreqSlider.Value;
            Properties.Settings.Default.Save();

        }
    }
}

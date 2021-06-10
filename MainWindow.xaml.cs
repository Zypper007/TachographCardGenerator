using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using FolderBrowserForWPF;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Generator_pliku_ddd
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Driver> _drivers = new List<Driver>();
        private List<Driver> Drivers
        {
            get => _drivers;
            set
            {
                _drivers = value;
                ExportBtn.IsEnabled = value.Count > 0;
            }
        } 

        private Regex OnlyInt = new Regex(@"^[0-9]+");


        public MainWindow()
        {
            InitializeComponent();
            DateFromDP.SelectedDate = DateTime.Today.AddDays(-20);
            DateToDP.SelectedDate = DateTime.Today.AddDays(20);
            SavePlaceTB.Text = Directory.GetCurrentDirectory();



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


        private async void GnerateBtn_Click(object sender, RoutedEventArgs e)
        {
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
                MessageBox.Show("Nie udało sie wygenerować nowych kierowców", "api.gov.pl", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusTB.Text = "Błąd api.gov.pl";
            } 
            finally
            {
                 Dispatcher.BeginInvoke((Action)(() => GenerateFile(Drivers)));
            }
        }
        private async void GenerateFile(IEnumerable<Driver> Drivers)
        {
            var rand = RandomSingleton.GetInstance();
            var diff = (int)(DateToDP.SelectedDate - DateFromDP.SelectedDate).Value.TotalSeconds;
            var tasks = new List<Task>();
            int i = 0;

            foreach (var driver in Drivers)
            {
                i++;

                var downloadDate = DateFromDP.SelectedDate.Value.AddSeconds(rand.Next(0, diff));
                var bytes = FileStructureConventer.Create(driver, downloadDate).ToArray();
                var fileName = SavePlaceTB.Text + $"\\C_{downloadDate.ToString("yyyyMMdd")}_{downloadDate.ToString("HHmmss")}_" +
                                $"{driver.Personals.FirstName[0]}_{driver.Personals.LastName}_{driver.Personals.ID}.ddd";

                tasks.Add(Task.Run(() => File.WriteAllBytes(fileName, bytes)));

                await tasks.Last().ContinueWith(tt =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        StatusTB.Text = $"Utworzono {i}/{Drivers.Count()} plików";
                    });
                });
            }

            Task.WaitAll(tasks.ToArray());

            if (i == Drivers.Count()) Process.Start("explorer.exe", SavePlaceTB.Text);
        }

            private void CopiesTB_OnlyInt(object sender, TextCompositionEventArgs e)
        {
            if (!OnlyInt.IsMatch(e.Text)) e.Handled = true;
        }

        private void CopiesTB_TextChange(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 0) GnerateBtn.IsEnabled = false;
            else GnerateBtn.IsEnabled = true;
        }

        private async void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            var self = sender as Button;
            self.IsEnabled = false;

            using (var sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\Drivers.csv"))
            {
                for(var i = 0; i< Drivers.Count; i++)
                {
                    await sw.WriteLineAsync(Drivers[i].toCSV());
                    StatusTB.Text = "Eksportowanie kierowców... " + (i + 1) + "/" + Drivers.Count;
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
            }
        }
    }
}

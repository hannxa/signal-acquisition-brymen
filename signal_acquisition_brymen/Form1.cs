using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using ScottPlot;
using ScottPlot.WinForms;

namespace signal_acquisition_brymen
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private RigolFunction _rigolFunction;
        private StringBuilder _csvData = new StringBuilder();
        private CancellationTokenSource _measurementCts;
        private bool _isMeasuring = false;
        private string _currentMeasurementType = "Voltage DC";

        // Listy do przechowywania danych pomiarowych
        private List<double> measurementValues = new List<double>();
        private List<double> measurementTimes = new List<double>();
        private DateTime startTime;
        private FormsPlot formsPlot1; // Zakładam, że masz ten kontroler na formie

        public Form1()
        {
            InitializeComponent();
            InitializePort();
            InitializeScottPlot();
        }

        private void InitializePort()
        {
            try
            {
                _serialPort = new SerialPort("COM20", 9600, Parity.None, 8, StopBits.One)
                {
                    ReadTimeout = 500,
                    WriteTimeout = 500
                };
                _serialPort.Open();
                _rigolFunction = new RigolFunction(_serialPort);
                feedback_label.Text = "Połączono z urządzeniem.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd otwierania portu COM: " + ex.Message);
                feedback_label.Text = "Błąd połączenia";
            }
        }

        private void InitializeScottPlot()
        {
            // Inicjalizacja wykresu - upewnij się, że formsPlot1 istnieje na formie
            if (formsPlot1 != null)
            {
                formsPlot1.Plot.Title("Wykres pomiarów");
                formsPlot1.Plot.XLabel("Czas (s)");
                formsPlot1.Plot.YLabel("Wartość");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopMeasurement();
            _rigolFunction?.Dispose();
            if (_serialPort?.IsOpen == true)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }
        }

        private void measurementDurationSlider_ValueChanged(object sender, EventArgs e)
        {
            durationLabel.Text = $"Czas pomiaru: {measurementDurationSlider.Value} s";
        }

        // Główna funkcja do rozpoczęcia pomiaru ciągłego
        private async void startMeasurementButton_Click(object sender, EventArgs e)
        {
            if (_isMeasuring)
            {
                StopMeasurement();
                return;
            }

            _isMeasuring = true;
            startMeasurementButton.Text = "Stop";
            feedback_label.Text = "Pomiar w toku...";

            // Wyczyść poprzednie dane
            measurementValues.Clear();
            measurementTimes.Clear();
            _csvData.Clear();

            int durationSeconds = measurementDurationSlider.Value;
            _measurementCts = new CancellationTokenSource();

            try
            {
                await Task.Run(() => PerformContinuousMeasurement(durationSeconds, _measurementCts.Token));
            }
            catch (OperationCanceledException)
            {
                feedback_label.Text = "Pomiar zatrzymany";
            }
            catch (Exception ex)
            {
                feedback_label.Text = $"Błąd: {ex.Message}";
            }
            finally
            {
                _isMeasuring = false;
                startMeasurementButton.Text = "Start";
            }
        }

        private void PerformContinuousMeasurement(int durationSeconds, CancellationToken ct)
        {
            startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(durationSeconds);

            while (DateTime.Now < endTime && !ct.IsCancellationRequested)
            {
                string valueStr = GetMeasurementValue(_currentMeasurementType);
                double elapsedSec = (DateTime.Now - startTime).TotalSeconds;
                string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

                this.Invoke((MethodInvoker)delegate {
                    if (valueStr.StartsWith("Błąd:"))
                    {
                        feedback_label.Text = valueStr;
                        return;
                    }

                    // Dodaj do CSV
                    _csvData.AppendLine($"{_currentMeasurementType};{valueStr};{timestamp}");
                    result_label.Text = $"{_currentMeasurementType}: {valueStr}";

                    // Sparsuj wartość i dodaj do list
                    if (double.TryParse(valueStr.Replace(',', '.'),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double numericValue))
                    {
                        measurementValues.Add(numericValue);
                        measurementTimes.Add(elapsedSec);
                    }
                });

                Thread.Sleep(200); // Odczyt co 200ms
            }

            if (!ct.IsCancellationRequested)
            {
                this.Invoke((MethodInvoker)delegate {
                    feedback_label.Text = $"Pomiar zakończony. Zebrano {measurementValues.Count} próbek.";
                });
            }
        }

        private string GetMeasurementValue(string measurementType)
        {
            try
            {
                return measurementType switch
                {
                    "Voltage DC" => _rigolFunction.GetVoltageDC(),
                    "Voltage AC" => _rigolFunction.GetVoltageAC(),
                    "Current" => _rigolFunction.GetCurrent(),
                    "Resistance" => _rigolFunction.GetResistance(),
                    "Capacitance" => _rigolFunction.GetCapacitance(),
                    _ => _rigolFunction.GetVoltageDC(),
                };
            }
            catch (Exception ex)
            {
                return $"Błąd pomiaru: {ex.Message}";
            }
        }

        private void StopMeasurement()
        {
            _measurementCts?.Cancel();
            _measurementCts?.Dispose();
            _measurementCts = null;
        }

        // Funkcja do wyświetlania wykresu
        private void PlotMeasurements()
        {
            if (measurementValues.Count == 0 || measurementTimes.Count != measurementValues.Count)
            {
                MessageBox.Show("Brak danych do wyświetlenia na wykresie!");
                return;
            }

            if (formsPlot1 == null)
            {
                MessageBox.Show("Kontrolka wykresu nie jest zainicjalizowana!");
                return;
            }

            try
            {
                // Wyczyść poprzedni wykres
                formsPlot1.Plot.Clear();

                // Dodaj dane do wykresu
                formsPlot1.Plot.AddScatter(
                    measurementTimes.ToArray(),
                    measurementValues.ToArray(),
                    label: _currentMeasurementType
                );

                // Ustaw tytuły osi
                formsPlot1.Plot.Title($"Wykres pomiarów - {_currentMeasurementType}");
                formsPlot1.Plot.XLabel("Czas (s)");
                formsPlot1.Plot.YLabel(GetYAxisLabel(_currentMeasurementType));

                // Ustaw zakresy osi
                formsPlot1.Plot.AxisAuto();

                // Odśwież wykres
                formsPlot1.Refresh();

                feedback_label.Text = $"Wykres wyświetlony ({measurementValues.Count} punktów)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd wyświetlania wykresu: {ex.Message}");
            }
        }

        private string GetYAxisLabel(string measurementType)
        {
            return measurementType switch
            {
                "Voltage DC" => "Napięcie DC (V)",
                "Voltage AC" => "Napięcie AC (V)",
                "Current" => "Prąd (A)",
                "Resistance" => "Opór (Ω)",
                "Capacitance" => "Pojemność (F)",
                _ => "Wartość"
            };
        }

        // Przycisk do wyświetlania wykresu
        private void plot_button_Click(object sender, EventArgs e)
        {
            PlotMeasurements();
        }

        // Przyciski wyboru typu pomiaru
        private void v_button_Click_1(object sender, EventArgs e)
        {
            _currentMeasurementType = "Voltage DC";
            if (_isMeasuring) return;
            TakeSingleMeasurement();
        }

        private void ac_button_Click(object sender, EventArgs e)
        {
            _currentMeasurementType = "Voltage AC";
            if (_isMeasuring) return;
            TakeSingleMeasurement();
        }

        private void I_button_Click(object sender, EventArgs e)
        {
            _currentMeasurementType = "Current";
            if (_isMeasuring) return;
            TakeSingleMeasurement();
        }

        private void Ω_button_Click(object sender, EventArgs e)
        {
            _currentMeasurementType = "Resistance";
            if (_isMeasuring) return;
            TakeSingleMeasurement();
        }

        private void capacitance_button_Click(object sender, EventArgs e)
        {
            _currentMeasurementType = "Capacitance";
            if (_isMeasuring) return;
            TakeSingleMeasurement();
        }

        private void TakeSingleMeasurement()
        {
            Task.Run(() =>
            {
                string value = GetMeasurementValue(_currentMeasurementType);
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                this.Invoke((MethodInvoker)delegate {
                    _csvData.AppendLine($"{_currentMeasurementType};{value};{timestamp}");
                    result_label.Text = $"{_currentMeasurementType}: {value}";
                });
            });
        }

        private void csv_button_Click(object sender, EventArgs e)
        {
            SaveToCsv();
        }

        private void SaveToCsv()
        {
            if (_csvData.Length == 0)
            {
                MessageBox.Show("Brak danych do zapisania!");
                return;
            }

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "CSV files (*.csv)|*.csv";
                saveDialog.DefaultExt = "csv";
                saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                saveDialog.FileName = $"rigol_measurements_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(saveDialog.FileName, append: false, Encoding.UTF8))
                        {
                            writer.WriteLine("Typ pomiaru;Wartość;Czas");
                            writer.Write(_csvData.ToString());
                        }
                        MessageBox.Show("Zapisano dane do pliku CSV!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd zapisu: {ex.Message}");
                    }
                }
            }
        }

        // Funkcja do czyszczenia danych (opcjonalna)
        private void clearData_button_Click(object sender, EventArgs e)
        {
            measurementValues.Clear();
            measurementTimes.Clear();
            _csvData.Clear();
            if (formsPlot1 != null)
            {
                formsPlot1.Plot.Clear();
                formsPlot1.Refresh();
            }
            feedback_label.Text = "Dane wyczyszczone";
        }
    }
}
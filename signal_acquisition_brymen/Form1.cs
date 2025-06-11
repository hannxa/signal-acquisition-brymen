using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
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

        private List<double> values = new();
        private List<double> times = new();
        private DateTime startTime;

        public Form1()
        {
            InitializeComponent();
            InitializePort();
            //InitializeScottPlot();
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

        //private void InitializeScottPlot()
        //{
        //    formsPlot1 = new FormsPlot
        //    {
        //        Location = new System.Drawing.Point(50, 300),
        //        Size = new System.Drawing.Size(600, 200)
        //    };
        //    Controls.Add(formsPlot1);
        //    formsPlot1.Plot.Title("Pomiar w czasie");
        //    formsPlot1.Plot.XLabel("Czas (s)");
        //    formsPlot1.Plot.YLabel("Wartość");
        //    formsPlot1.Refresh();
        //}

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
            values.Clear();
            times.Clear();
            //formsPlot1.Plot.Clear();
            startTime = DateTime.Now;

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
            DateTime endTime = DateTime.Now.AddSeconds(durationSeconds);

            while (DateTime.Now < endTime && !ct.IsCancellationRequested)
            {
                string value = GetMeasurementValue(_currentMeasurementType);
                double elapsedSec = (DateTime.Now - startTime).TotalSeconds;
                string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

                this.Invoke((MethodInvoker)delegate {
                    if (value.StartsWith("Błąd:"))
                    {
                        feedback_label.Text = value;
                        return;
                    }

                    _csvData.AppendLine($"{_currentMeasurementType};{value};{timestamp}");
                    result_label.Text = $"{_currentMeasurementType}: {value}";

                    if (double.TryParse(value, out double numericValue))
                    {
                        values.Add(numericValue);
                        times.Add(elapsedSec);

                        //formsPlot1.Plot.Clear();
                        //formsPlot1.Plot.AddScatter(times.ToArray(), values.ToArray());
                        //formsPlot1.Render();
                    }
                });

                Thread.Sleep(200);
            }

            if (!ct.IsCancellationRequested)
            {
                this.Invoke((MethodInvoker)delegate {
                    feedback_label.Text = "Pomiar zakończony";
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
                        _csvData.Clear();
                        MessageBox.Show("Zapisano dane do pliku CSV!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd zapisu: {ex.Message}");
                    }
                }
            }
        }
    }
}

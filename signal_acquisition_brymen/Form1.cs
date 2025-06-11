using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace signal_acquisition_brymen
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private RigolFunction _rigolFunction;
        private StringBuilder _csvData = new();
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
                startMeasurementButton.Text = "Start Measurement";
            }
        }

        private async void singleMeasurementButton_Click(object sender, EventArgs e)
        {
            string value = await Task.Run(() => GetMeasurementValue(_currentMeasurementType));

            this.Invoke((MethodInvoker)delegate {
                if (value.StartsWith("Błąd:"))
                {
                    feedback_label.Text = value;
                }
                else
                {
                    result_label.Text = $"{_currentMeasurementType}: {value}";
                }
            });
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

        private void StopMeasurement()
        {
            _measurementCts?.Cancel();
            _isMeasuring = false;
            startMeasurementButton.Text = "Start Measurement";
            feedback_label.Text = "Pomiar zatrzymany";
        }

        private async void v_button_Click_1(object sender, EventArgs e)
        {
            _currentMeasurementType = "Voltage DC";
            string val = await Task.Run(() => _rigolFunction.GetVoltageDC());

            this.Invoke((MethodInvoker)delegate {
                result_label.Text = $"{_currentMeasurementType}: {val}";
            });
        }

        private async void ac_button_Click(object sender, EventArgs e)
        {
            _currentMeasurementType = "Voltage AC";
            string val = await Task.Run(() => _rigolFunction.GetVoltageAC());

            this.Invoke((MethodInvoker)delegate {
                result_label.Text = $"{_currentMeasurementType}: {val}";
            });
        }

        private async void I_button_Click(object sender, EventArgs e)
        {
            _currentMeasurementType = "Current";
            string val = await Task.Run(() => _rigolFunction.GetCurrent());

            this.Invoke((MethodInvoker)delegate {
                result_label.Text = $"{_currentMeasurementType}: {val}";
            });
        }

        private async void Ω_button_Click(object sender, EventArgs e)
        {
            _currentMeasurementType = "Resistance";
            string val = await Task.Run(() => _rigolFunction.GetResistance());
            if (!val.StartsWith("Błąd:"))
                result_label.Text = $"{_currentMeasurementType}: {val}";
            else
                feedback_label.Text = val;
        }

        private async void capacitance_button_Click(object sender, EventArgs e)
        {
            _currentMeasurementType = "Capacitance";
            string val = await Task.Run(() => _rigolFunction.GetCapacitance());

            this.Invoke((MethodInvoker)delegate {
                result_label.Text = $"{_currentMeasurementType}: {val}";
            });
        }


        private void csv_button_Click(object sender, EventArgs e)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "measurement.csv");
                File.WriteAllText(filePath, _csvData.ToString());
                feedback_label.Text = "Zapisano do CSV";
            }
            catch (Exception ex)
            {
                feedback_label.Text = "Błąd zapisu CSV: " + ex.Message;
            }
        }
    }
}

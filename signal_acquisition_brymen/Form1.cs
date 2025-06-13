using System.IO.Ports;
using System.Text;
using ScottPlot;

namespace signal_acquisition_brymen
{
    public partial class Form1 : Form
    {
        // Serial communication and measurement components
        private SerialPort? _serialPort;
        private RigolFunction? _rigolFunction;

        // Data for CSV export
        private StringBuilder _csvData = new();

        // For async measurement control
        private CancellationTokenSource? _measurementCts;
        private bool _isMeasuring = false;
        private string _currentMeasurementType = "Voltage DC";

        // Data for plotting
        private List<double> values = new();
        private List<double> times = new();
        private DateTime startTime;

        // Plotting components
        private ScottPlot.Plottables.Scatter? plotLine;
        private System.Windows.Forms.Timer? _plotUpdateTimer;

        public Form1()
        {
            InitializeComponent();
            InitializePort();
            InitializePlot();
            InitializeTimer();
        }

        private void InitializePlot()
        {
            // Set up plot with title and labels
            formsPlot1.Plot.Title("Live Measurement Data");
            formsPlot1.Plot.XLabel("Time (seconds)");
            formsPlot1.Plot.YLabel("Value");

            // Initialize empty line
            plotLine = formsPlot1.Plot.Add.Scatter(new double[] { }, new double[] { });
            plotLine.LineWidth = 2;
            plotLine.Color = ScottPlot.Colors.Blue;
            plotLine.LinePattern = LinePattern.Solid;

            formsPlot1.Plot.Axes.AutoScale();
            formsPlot1.Refresh();
        }

        private void InitializeTimer()
        {
            // Timer for periodic plot updates
            _plotUpdateTimer = new System.Windows.Forms.Timer();
            _plotUpdateTimer.Interval = 100;
            _plotUpdateTimer.Tick += PlotUpdateTimer_Tick!;

            _plotUpdateTimer?.Start();
        }

        private void PlotUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_isMeasuring && values.Count > 0)
                UpdatePlot();
        }

        private void UpdatePlot()
        {
            try
            {
                if (values.Count > 0 && times.Count > 0 && plotLine != null)
                {
                    // Refresh plot with new data
                    formsPlot1.Plot.Remove(plotLine);
                    plotLine = formsPlot1.Plot.Add.Scatter(times.ToArray(), values.ToArray());
                    plotLine.LineWidth = 2;
                    plotLine.Color = ScottPlot.Colors.Blue;
                    plotLine.LinePattern = LinePattern.Solid;

                    formsPlot1.Plot.Axes.AutoScale();

                    // Show only last 50 data points
                    if (times.Count > 50)
                    {
                        double minTime = times[Math.Max(0, times.Count - 50)];
                        double maxTime = times[^1];
                        formsPlot1.Plot.Axes.SetLimitsX(minTime - 1, maxTime + 1);
                    }

                    formsPlot1.Refresh();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Plot update error: {ex.Message}");
            }
        }

        private void InitializePort()
        {
            try
            {
                // Setup serial port
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
            // Update label with duration value
            durationLabel.Text = $"Czas pomiaru: {measurementDurationSlider.Value} s";
        }

        private async void startMeasurementButton_Click(object sender, EventArgs e)
        {
            if (_isMeasuring)
            {
                StopMeasurement();
                return;
            }

            StartMeasurement();

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
                StopMeasurement();
            }
        }

        private void StartMeasurement()
        {
            _isMeasuring = true;
            startMeasurementButton.Text = "Stop";
            feedback_label.Text = "Pomiar w toku...";

            // Reset data and plot
            values.Clear();
            times.Clear();
            _csvData.Clear();
            startTime = DateTime.Now;

            formsPlot1.Plot.Clear();
            plotLine = formsPlot1.Plot.Add.Scatter(new double[] { }, new double[] { });
            plotLine.LineWidth = 2;
            plotLine.Color = ScottPlot.Colors.Blue;
            plotLine.LinePattern = LinePattern.Solid;
            formsPlot1.Plot.Title($"Live {_currentMeasurementType} Measurement");
            formsPlot1.Refresh();

            _plotUpdateTimer.Start();
        }

        private void PerformContinuousMeasurement(int durationSeconds, CancellationToken ct)
        {
            DateTime endTime = DateTime.Now.AddSeconds(durationSeconds);

            while (DateTime.Now < endTime && !ct.IsCancellationRequested)
            {
                try
                {
                    string value = GetMeasurementValue(_currentMeasurementType);
                    double elapsedSec = (DateTime.Now - startTime).TotalSeconds;
                    string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

                    this.Invoke((MethodInvoker)delegate {
                        ProcessMeasurementValue(value, elapsedSec, timestamp);
                    });

                    Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate {
                        feedback_label.Text = $"Błąd pomiaru: {ex.Message}";
                    });
                    break;
                }
            }

            if (!ct.IsCancellationRequested)
            {
                this.Invoke((MethodInvoker)delegate {
                    feedback_label.Text = "Pomiar zakończony";
                });
            }
        }

        private void ProcessMeasurementValue(string value, double elapsedSec, string timestamp)
        {
            if (value.StartsWith("Błąd:"))
            {
                feedback_label.Text = value;
                return;
            }

            // Append value to CSV
            _csvData.AppendLine($"{_currentMeasurementType};{value};{timestamp}");

            // Update UI label
            result_label.Text = $"{_currentMeasurementType}: {value}";

            // Add to plot lists
            if (double.TryParse(value, out double numericValue))
            {
                values.Add(numericValue);
                times.Add(elapsedSec);
            }
        }

        private string GetMeasurementValue(string measurementType)
        {
            if (_rigolFunction == null)
                return "Błąd: Brak połączenia z urządzeniem";

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
            _plotUpdateTimer?.Stop();

            if (!feedback_label.Text.StartsWith("Błąd"))
            {
                feedback_label.Text = "Pomiar zatrzymany";
            }
        }

        // Handlers for single-click measurement buttons

        private async void v_button_Click_1(object sender, EventArgs e)
        {
            if (_rigolFunction == null) return;

            _currentMeasurementType = "Voltage DC";
            string val = await Task.Run(() => _rigolFunction.GetVoltageDC());
            result_label.Text = $"{_currentMeasurementType}: {val}";
            UpdatePlotTitle();
        }

        private async void ac_button_Click(object sender, EventArgs e)
        {
            if (_rigolFunction == null) return;

            _currentMeasurementType = "Voltage AC";
            string val = await Task.Run(() => _rigolFunction.GetVoltageAC());
            result_label.Text = $"{_currentMeasurementType}: {val}";
            UpdatePlotTitle();
        }

        private async void I_button_Click(object sender, EventArgs e)
        {
            if (_rigolFunction == null) return;

            _currentMeasurementType = "Current";
            string val = await Task.Run(() => _rigolFunction.GetCurrent());
            result_label.Text = $"{_currentMeasurementType}: {val}";
            UpdatePlotTitle();
        }

        private async void Ω_button_Click(object sender, EventArgs e)
        {
            if (_rigolFunction == null) return;

            _currentMeasurementType = "Resistance";
            string val = await Task.Run(() => _rigolFunction.GetResistance());
            if (!val.StartsWith("Błąd:"))
                result_label.Text = $"{_currentMeasurementType}: {val}";
            else
                feedback_label.Text = val;

            UpdatePlotTitle();
        }

        private async void capacitance_button_Click(object sender, EventArgs e)
        {
            if (_rigolFunction == null) return;

            _currentMeasurementType = "Capacitance";
            string val = await Task.Run(() => _rigolFunction.GetCapacitance());
            result_label.Text = $"{_currentMeasurementType}: {val}";
            UpdatePlotTitle();
        }

        private void UpdatePlotTitle()
        {
            if (!_isMeasuring)
            {
                formsPlot1.Plot.Title($"{_currentMeasurementType} Measurement");
                formsPlot1.Refresh();
            }
        }

        private void csv_button_Click(object sender, EventArgs e)
        {
            try
            {
                // Save measurements to CSV file on desktop
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = $"measurement_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                string filePath = Path.Combine(desktopPath, fileName);

                string csvContent = "Measurement Type;Value;Timestamp\n" + _csvData.ToString();
                File.WriteAllText(filePath, csvContent);

                feedback_label.Text = $"Zapisano do {fileName}";
            }
            catch (Exception ex)
            {
                feedback_label.Text = "Błąd zapisu CSV: " + ex.Message;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _measurementCts?.Cancel();
                _isMeasuring = false;

                _plotUpdateTimer?.Stop();
                _plotUpdateTimer?.Dispose();

                _measurementCts?.Dispose();
                _rigolFunction?.Dispose();

                if (_serialPort != null)
                {
                    if (_serialPort.IsOpen)
                        _serialPort.Close();
                    _serialPort.Dispose();
                }

                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isMeasuring)
                StopMeasurement();
        }
    }
}

using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;

namespace signal_acquisition_brymen
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private RigolFunction _rigolFunction;
        private List<double> _measurementValues = new();
        private LineSeries<double> _series;
        private CartesianChart _cartesianChart;

        public Form1()
        {
            InitializeComponent();

            // Inicjalizacja wykresu
            _series = new LineSeries<double>
            {
                Values = _measurementValues,
                Name = "Wynik",
                Fill = null // linia bez wypełnienia
            };

            _cartesianChart = new CartesianChart
            {
                Series = new ISeries[] { _series },
                Dock = DockStyle.Right,
                Width = 400,
                Height = 300
            };

            Controls.Add(_cartesianChart);

            try
            {
                _serialPort = new SerialPort("COM13", 9600, Parity.None, 8, StopBits.One);
                _serialPort.Open();

                _rigolFunction = new RigolFunction(_serialPort);
                feedback_label.Text = "Połączono z urządzeniem.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd otwierania portu COM: " + ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        private enum MeasurementType
        {
            VoltageDC,
            VoltageAC,
            CurrentDC,
            CurrentAC,
            Resistance
        }

        private MeasurementType _selectedMeasurement = MeasurementType.VoltageDC;


        private void v_button_Click_1(object sender, EventArgs e)
        {
            _selectedMeasurement = MeasurementType.VoltageDC;
            string value = _rigolFunction.GetVoltageDC();
            result_label.Text = $"DC Voltage: {value}";
        }

        private void ac_button_Click(object sender, EventArgs e)
        {
            _selectedMeasurement = MeasurementType.VoltageAC;
            string value = _rigolFunction.GetVoltageAC();
            result_label.Text = $"AC Voltage: {value}";
        }

        private void I_button_Click(object sender, EventArgs e)
        {
            _selectedMeasurement = MeasurementType.CurrentDC;
            string value = _rigolFunction.GetCurrentDC();
            result_label.Text = $"DC current: {value}";
        }

        private void Ω_button_Click(object sender, EventArgs e)
        {
            _selectedMeasurement = MeasurementType.Resistance;
            string value = _rigolFunction.GetResistance();
            result_label.Text = $"Resistance: {value}";
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            feedback_label.Text = $"Czas pomiaru: {trackBar1.Value} s";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            int seconds = trackBar1.Value;
            feedback_label.Text = $"Pomiar przez {seconds} sekund...";
            result_label.Text = "";
            _measurementValues.Clear();
            _series.Values = _measurementValues;
            motionCanvas1.Update();

            var endTime = DateTime.Now.AddSeconds(seconds);
            int t = 0;

            while (DateTime.Now < endTime)
            {
                string valueStr = _selectedMeasurement switch
                {
                    MeasurementType.VoltageDC => _rigolFunction.GetVoltageDC(),
                    MeasurementType.VoltageAC => _rigolFunction.GetVoltageAC(),
                    MeasurementType.CurrentDC => _rigolFunction.GetCurrentDC(),
                    MeasurementType.CurrentAC => _rigolFunction.GetCurrentAC(),
                    MeasurementType.Resistance => _rigolFunction.GetResistance(),
                    _ => "0"
                };

                string label = _selectedMeasurement switch
                {
                    MeasurementType.VoltageDC => "DC Voltage",
                    MeasurementType.VoltageAC => "AC Voltage",
                    MeasurementType.CurrentDC => "DC Current",
                    MeasurementType.CurrentAC => "AC Current",
                    MeasurementType.Resistance => "Resistance",
                    _ => "Unknown"
                };

                // Próbujemy sparsować wartość do double
                if (double.TryParse(valueStr.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value))
                {
                    _measurementValues.Add(value);
                }
                else
                {
                    _measurementValues.Add(0);
                }

                result_label.Text = $"{_selectedMeasurement}: {valueStr}";
                motionCanvas1.Update();
                t++;

                await Task.Delay(1000); // odczyt co 1 sekundę
            }

            feedback_label.Text = $"Pomiar zakończony ({seconds} s)";
            motionCanvas1.Update(); // odśwież wykres po zakończeniu
        }

        private void motionCanvas1_Load(object sender, EventArgs e)
        {

        }
    }
}

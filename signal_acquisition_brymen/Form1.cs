using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace signal_acquisition_brymen
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private RigolFunction _rigolFunction;
        List<string> resultsToFiles = new List<string>();
        StringBuilder csv = new StringBuilder();

        public Form1()
        {
            InitializeComponent();

            try
            {
                _serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
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

        private void v_button_Click_1(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetVoltageDC();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            csv.AppendLine($"Voltage DC;{value};{timestamp}");
            result_label.Text = $"DC Voltage: {value:F6}";
        }

        private void ac_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetVoltageAC();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            csv.AppendLine($"Voltage AC;{value};{timestamp}");
            result_label.Text = $"DC Voltage: {value}";
        }

        private void I_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetCurrent();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            csv.AppendLine($"Current;{value};{timestamp}");
            result_label.Text = $"DC current: {value}";
        }

        private void Ω_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetResistance();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            csv.AppendLine($"Resistance;{value};{timestamp}");
            result_label.Text = $"Resistance: {value}";
        }
        private void capacitance_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetCapacitance();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            csv.AppendLine($"Capacitance;{value};{timestamp}");
            result_label.Text = $"Capacitance: {value}";
        }
        private void csv_button_Click(object sender, EventArgs e)
        {
            saving();
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


        public void saving()
        {

            string path = "C:\\Users\\akabe\\Desktop\\rigolProgram\\text.csv";
            bool fileExists = File.Exists(path);


            DateTime actualdate = DateTime.Now;
            string actualdatestring = actualdate.ToString("d");

            //File.WriteAllLines(path, resultsToFiles);
            using (StreamWriter writer = new StreamWriter(path, append: true, encoding: new UTF8Encoding(true)))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Typ pomiaru;Wartość;Czas");
                }

                writer.Write(csv.ToString());
                MessageBox.Show("Saved to csv!!!! <3333");
            }

            csv.Clear();
        }

        
    }
}

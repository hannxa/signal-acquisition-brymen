using System;
using System.IO.Ports;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace signal_acquisition_brymen
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private RigolFunction _rigolFunction;
        List<string> resultsToFiles = new List<string>();
        
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
            resultsToFiles.Add("Voltage DC " + value);
            result_label.ForeColor = Color.Black;
            result_label.Text = $"DC Voltage: {value:F6}";
        }

        private void ac_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetVoltageAC();
            resultsToFiles.Add("Voltage AC: " + value);
            result_label.Text = $"DC Voltage: {value}";
        }

        private void I_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetCurrent();
            resultsToFiles.Add("Current" + value);
            result_label.Text = $"DC current: {value}";
        }

        private void Ω_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetResistance();
            resultsToFiles.Add("resistance: " + value);
            result_label.Text = $"Resistance: {value}";
        }

        private void csv_button_Click(object sender, EventArgs e)
        {
            saving();
        }

        public void saving()
        {
            DateTime actualdate = DateTime.Now;
            string actualdatestring = actualdate.ToString("d");
           
            string path = "C:\\Users\\akabe\\Desktop\\rigolProgram\\text.txt";
            //File.WriteAllLines(path, resultsToFiles);
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(resultsToFiles.Last());
            }
        }
    }
}

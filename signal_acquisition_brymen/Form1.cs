using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace signal_acquisition_brymen
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        private RigolFunction _rigolFunction;

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

        private void dc_v_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetVoltageDC();
            Console.WriteLine("V:" + value);
            result_label.Text = $"DC Voltage: {value}";
        }

        private void ac_v_button_Click(object sender, EventArgs e)
        {

            string value = _rigolFunction.GetVoltageAC();
            result_label.Text = $"AC Voltage: {value}";
            Console.WriteLine("AC Voltage", value);

        }

        private void dc_i_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetCurrentDC();
            result_label.Text = $"DC Current: {value}";
        }

        private void ac_i_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetCurrentAC();
            result_label.Text = $"AC Current: {value}";
        }

        private void resistance_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetResistance();
            result_label.Text = $"Resistance: {value}";
        }

        private void capacitance_button_Click(object sender, EventArgs e)
        {
            string value = _rigolFunction.GetCapacitance();
            result_label.Text = $"Capacitance: {value}";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }
    }
}

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        private void v_button_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("wejscie w funkcje: "); // debug
            string value = _rigolFunction.GetVoltageDC();
            MessageBox.Show("DC Voltage odpowiedź: " + value); // debug
            result_label.Text = $"DC Voltage: {value}";
        }

        private void ac_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("wejscie w funkcje: "); // debug
            string value = _rigolFunction.GetVoltageAC();
            MessageBox.Show("DC Voltage odpowiedź: " + value); // debug
            result_label.Text = $"DC Voltage: {value}";
        }

        private void I_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("wejscie w funkcje: "); // debug
            string value = _rigolFunction.GetCurrent();
            MessageBox.Show("DC current odpowiedź: " + value); // debug
            result_label.Text = $"DC current: {value}";
        }

        private void Ω_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("wejscie w funkcje: "); // debug
            string value = _rigolFunction.GetResistance();
            MessageBox.Show("Resistance: " + value); // debug
            result_label.Text = $"Resistance: {value}";
        }
    }
}

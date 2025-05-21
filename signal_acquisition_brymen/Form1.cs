using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace signal_acquisition_brymen
{
    public partial class Form1 : Form
    {
        private SerialPortInitializer _portInitializer;
        private RigolFunction _rigolFunction;


        public Form1()
        {
            InitializeComponent();

            InitializeConnection();
        }



        private void v_button_Click(object sender, EventArgs e)
        {
            result_label.Text = _rigolFunction.GetVoltageDC();

        }

        private void ac_button_Click(object sender, EventArgs e)
        {
            result_label.Text = _rigolFunction.GetVoltageAC();

        }

        private void I_button_Click(object sender, EventArgs e)
        {
            result_label.Text = _rigolFunction.GetCurrentDC();
        }

        private void Ω_button_Click(object sender, EventArgs e)
        {
            result_label.Text = _rigolFunction.GetResistance();
        }


        private void InitializeConnection()
        {

            _portInitializer = new SerialPortInitializer("COM3");
            _rigolFunction = new RigolFunction(_portInitializer.Port);

            // Begin communicationstry{

            try
            {
                _portInitializer.Port.Open();
                feedback_label.Text = ("Initialized");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // Enter an application loop to keep this thread alive
            //Application.Run();
        }

    }
}

using System;
using System.IO.Ports;

namespace signal_acquisition_brymen
{
    public class SerialPortInitializer : IDisposable
    {
        public SerialPort Port { get; private set; }

        public SerialPortInitializer(string portName = "COM13")
        {
            Console.WriteLine($"[DEBUG] SerialPortInitializer constructor called for port: {portName}");
            Port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            Port.DataReceived += Port_DataReceived;
            try
            {
                if (!Port.IsOpen)
                {
                    Port.Open();
                    Console.WriteLine($"[INFO]: Port {portName} opened.");
                }
                else
                {
                    Console.WriteLine($"[DEBUG] Port {portName} was already open.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR]: Could not open port {portName}. {ex.Message}");
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine("[DEBUG] DataReceived event triggered.");
            try
            {
                string data = Port.ReadExisting();
                Console.WriteLine($"[DATA RECEIVED]: {data}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR]: Error reading data. {ex.Message}");
            }
        }

        public void Dispose()
        {
            Console.WriteLine("[DEBUG] Dispose called.");
            if (Port != null)
            {
                if (Port.IsOpen)
                {
                    Port.Close();
                    Console.WriteLine("[INFO]: Port closed.");
                }
                else
                {
                    Console.WriteLine("[DEBUG] Port was already closed.");
                }
                Port.Dispose();
                Console.WriteLine("[DEBUG] Port disposed.");
            }
            else
            {
                Console.WriteLine("[DEBUG] Port was null during dispose.");
            }
        }
    }
}

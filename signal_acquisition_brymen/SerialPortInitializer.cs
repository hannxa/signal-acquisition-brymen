using System;
using System.IO.Ports;

namespace signal_acquisition_brymen
{
    public class SerialPortInitializer
    {
        public SerialPort Port { get; private set; }

        public SerialPortInitializer(string portName = "COM1")
        {
            Port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            Port.DataReceived += Port_DataReceived;
            Port.Open();
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = Port.ReadExisting();
            Console.WriteLine($"[DATA RECEIVED]: {data}");
        }
    }
}

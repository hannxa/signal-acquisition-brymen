using System;
using System.IO.Ports;

namespace signal_acquisition_brymen
{
    // A helper class to initialize and configure a SerialPort
    public class SerialPortInitializer
    {
        // Public property to access the configured SerialPort
        public SerialPort Port { get; private set; }

        // Constructor that sets up the SerialPort with given parameters
        public SerialPortInitializer(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            Port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        }
    }
}

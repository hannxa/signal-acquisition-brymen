using System;
using System.IO.Ports;

namespace signal_acquisition_brymen
{
    public class SerialPortInitializer
    {
        public SerialPort Port { get; private set; }

        public SerialPortInitializer(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            Port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        }
    }
}

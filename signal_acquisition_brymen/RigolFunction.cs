using System;
using System.IO.Ports;

namespace signal_acquisition_brymen
{
    public class RigolFunction
    {
        private SerialPort _port;

        public RigolFunction(SerialPort port)
        {
            _port = port;
        }

        public void GetVoltage()
        {
            _port.WriteLine(":MEASure:VOLTage?");
        }

        public void GetCurrent()
        {
            _port.WriteLine(":MEASure:CURRent?");
        }
    }
}

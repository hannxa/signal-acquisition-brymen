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

        public string GetVoltageDC()
        {
            Console.WriteLine("Voltage function");
            _port.DiscardInBuffer(); // czyścimy bufor przed zapytaniem
            _port.WriteLine(":MEASure:VOLTage:DC?");
            return _port.ReadLine().Trim(); // od razu odczytujemy odpowiedź
        }
        public string GetVoltageAC()
        {
            _port.DiscardInBuffer(); // czyścimy bufor przed zapytaniem
            _port.WriteLine(":MEASure:VOLTage:AC?");
            return _port.ReadLine().Trim(); // od razu odczytujemy odpowiedź
        }

        public string GetCurrentDC()
        {
            _port.DiscardInBuffer(); // czyścimy bufor przed zapytaniem
            _port.WriteLine(":MEASure:CURRent:DC?");
            return _port.ReadLine().Trim(); // od razu odczytujemy odpowiedź
        }
        public string GetCurrentAC()
        {
            _port.DiscardInBuffer(); // czyścimy bufor przed zapytaniem
            _port.WriteLine(":MEASure:CURRent:AC?");
            return _port.ReadLine().Trim(); // od razu odczytujemy odpowiedź
        }

        /// bledne
        public string GetResistance()
        {
            _port.DiscardInBuffer(); // czyścimy bufor przed zapytaniem
            _port.WriteLine(":MEASure:RESistance?");
            return _port.ReadLine().Trim(); // od razu odczytujemy odpowiedź
        }
    }
}
